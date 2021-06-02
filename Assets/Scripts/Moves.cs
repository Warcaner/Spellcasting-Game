using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using UnityEngine.XR;
using System.IO;
using UnityEngine.Events;
using System.Linq;

public class Moves : MonoBehaviour
{

    // test bool for double casting
    public bool steroid;
    public System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
    public System.TimeSpan ts;

    public bool isCreated;
    Spellcast spellcast;
    


    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public InputHelpers.Button inputButton2;
    public InputHelpers.Button inputButton3;
    public float inputThreshold = 0.1f;
    public Transform movementSource;
    public float newPositionThresholdDistance = 0.05f;

    public GameObject DebugCubePrefab;
    //public GameObject Testolone;
    //public GameObject MZ;

    //public GameObject projectile;
    //public float shootForce;

    //[SerializeField]
    // public Transform FirePoint;



    public bool creationMode = true;
    public string newGestureName;

    public float recognitionThreshold = 0.7f;

    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }

    public UnityStringEvent OnRecognized;

    public List<Gesture> trainingSet = new List<Gesture>();


    private bool isMoving = false;


    private List<Vector3> positionsList = new List<Vector3>();
    List<string> combo = new List<string>();
    // Start is called before the first frame update
    public void Movementscan()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton2, out bool isClicked, inputThreshold);


        spellcast = GetComponent<Spellcast>();
        




        // prevent problem with out of bound index by adding a space at the first index
        if (combo?.Count == 0)
        {


            isCreated = false;
            combo.Add(" ");

        }


        // start of movement
        if (!isMoving && isPressed)
        {

            Debug.Log("Start movement");
            isCreated = false;
            isMoving = true;
            positionsList.Clear();


            if (DebugCubePrefab)
                Destroy(Instantiate(DebugCubePrefab, movementSource.position, Quaternion.identity), 3);
            positionsList.Add(movementSource.position);
        }

        //end
        else if (isMoving && !isPressed)
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

            if (creationMode == true)
            {
                newGesture.Name = newGestureName;
                trainingSet.Add(newGesture);

                string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
                GestureIO.WriteGesture(pointArray, newGestureName, fileName);
            }
            else
            {
                Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
                //Debug.Log(result.GestureClass + result.Score);

                if (result.Score > recognitionThreshold)
                {





                    string gesture1 = result.GestureClass;
                    GameObject payload;
                    payload = GameObject.Find(gesture1);
                    payload.name = gesture1;
                    combo.Add(gesture1); // experimental forloop that replaces combo.add to avoid the out of bounds error





                    string comboliststring = string.Join(",", combo.ToArray());
                    Debug.Log(comboliststring);




                    // else
                    // {
                    //    Debug.Log("Couldn't Recognize a gesture.");
                    //}




                    // updat


                }

            }
        }

        else if (isMoving && isPressed)
        {

            //Debug.Log("update movement");
            Vector3 lastPosition = positionsList[positionsList.Count - 1];
            if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance)
                positionsList.Add(movementSource.position);
            if (DebugCubePrefab)
                Destroy(Instantiate(DebugCubePrefab, movementSource.position, Quaternion.identity), 3);

        }











        for (int i = 0; i < combo.Count - 1; i++)

        {
            

            if (isCreated == false)
            {


                if (isClicked)
                {
                    SetTimer(1);


                    if (combo[1].Contains("Vline"))
                    {

                        SetTimer(2);

                        if (combo.Count == 2)

                        {
                            combo.Clear();
                            isCreated = true;
                            spellcast.Fire();                           
                            SetTimer(3);
                        }
                     
                        if (combo.Contains("Hline"))
                        {

                            combo.Clear();
                            isCreated = true;
                            spellcast.FireThunder();

                        }

                        if (combo.Contains("Z"))
                        {

                            combo.Clear();
                            isCreated = true;
                            spellcast.FireIce();

                        }

                    }



                    if (combo[1].Contains("Hline"))
                    {

                        SetTimer(2);

                        if (combo.Count == 2)

                        {
                            combo.Clear();
                            isCreated = true;
                            spellcast.Thunder();
                            SetTimer(3);
                        }

                        if (combo.Contains("Vline"))
                        {

                            combo.Clear();
                            isCreated = true;
                            spellcast.ThunderFire();

                        }

                        if (combo.Contains("Z"))
                        {

                            combo.Clear();
                            isCreated = true;
                            spellcast.ThunderIce();

                        }

                    }



                    if (combo[1].Contains("Z"))
                    {

                        SetTimer(2);

                        if (combo.Count == 2)

                        {
                            combo.Clear();
                            isCreated = true;
                            spellcast.Ice();
                            SetTimer(3);
                        }

                        if (combo.Contains("Hline"))
                        {

                            combo.Clear();
                            isCreated = true;
                            spellcast.IceThunder();

                        }

                        if (combo.Contains("Vline"))
                        {

                            combo.Clear();
                            isCreated = true;
                            spellcast.IceFire();

                        }

                    }



                }

                if (combo.Count > 3)
                {
                    combo.Clear();
                    SetTimer(3);
                }
            }
        }






    }

    public void SetTimer(int mode) {
        if (mode == 0)
        {
            if (timer.IsRunning && timer.Elapsed.Seconds >= 5)
            {
                //combo.Clear();
                timer.Stop();
                Debug.Log("Time is up!");
            }
        } else if (mode == 1)
        {
            timer.Start();
            Debug.Log("Timer activated.");
        } else if (mode == 2)
        {
            timer.Restart();
            Debug.Log("Timer reset.");
        } else if (mode == 3)
        {
            timer.Stop();
            //combo.Clear();
           // Debug.Log("Timer stopped.");
        }
    }
}
    

