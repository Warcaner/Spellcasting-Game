using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.IO;
using UnityEngine.Events;

public class MovementRecognizer : MonoBehaviour {


    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public InputHelpers.Button inputButton2;

    public float inputThreshold = 0.1f;
    public Transform movementSource;

    public float newPositionThresholdDistance = 0.05f;
    public GameObject DebugCubePrefab;

    public GameObject projectile;
    public float shootForce;

    [SerializeField]
    public Transform FirePoint;



    public bool creationMode = true;
    public string newGestureName;

    public float recognitionThreshold = 0.7f;

    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string>
    {

    }
    public UnityStringEvent OnRecognized;

    private List<Gesture> trainingSet = new List<Gesture>();
    

    private bool isMoving = false;

    private List<Vector3> positionsList = new List<Vector3>();

    // experimental, remove if doesnt work
    public GameObject recognizedGestObj;

    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (var item in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }

    // Update is called once per frame
    void Update()
    {


        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton2, out bool isClicked, inputThreshold);

        //start
        if (!isMoving && isPressed)
        {

            startMovement();
            positionsList.Add(movementSource.position);
        }

        //end
        else if(isMoving && isClicked)
        {

            endMovement();

        }


        // update
        else if (isMoving && isPressed)
        {

            updateMovement();

        }





    }

    void startMovement()
    {


        Debug.Log("Start movement");

        isMoving = true;
        positionsList.Clear();



        if(DebugCubePrefab)
        Destroy(Instantiate(DebugCubePrefab, movementSource.position, Quaternion.identity),3);
    }


    void endMovement()
    {


        Debug.Log("End movement");

        isMoving = false;

        Point[] pointArray = new Point[positionsList.Count];
        for (int i = 0; i < positionsList.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);

        }

        Gesture newGesture = new Gesture(pointArray);

        if (creationMode == true) {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        } else {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + result.Score);
            if (result.Score > recognitionThreshold)
            {
                OnRecognized.Invoke(result.GestureClass);
                Debug.Log("Spawned :" + result.GestureClass);
                /* experimental
                GameObject shot = GameObject.Instantiate(projectile, transform.position, transform.rotation);
                shot.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);

                Destroy(shot, 3);
                */

            } else
            {
                Debug.Log("Couldn't Recognize a gesture.");
            }
        }




    }

    void updateMovement()
    {
        Debug.Log("update movement");
        Vector3 lastPosition = positionsList[positionsList.Count - 1];
        if(Vector3.Distance(movementSource.position,lastPosition) > newPositionThresholdDistance)
        positionsList.Add(movementSource.position);
        if (DebugCubePrefab)
            Destroy(Instantiate(DebugCubePrefab, movementSource.position, Quaternion.identity), 3);
    }

}
