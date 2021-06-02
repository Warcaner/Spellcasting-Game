using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.IO;
using UnityEngine.Events;

public class Spells : MonoBehaviour
{


    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.05f;
    public float shootForce = 100.0f;
    public bool isCreated;

    [SerializeField] 
    private GameObject bulletPrefab;
    
    [SerializeField]
    private Transform bulletSpawn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Bulletman();
    }

    void Bulletman()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        if (isPressed = true && isCreated == false)
        {
            isCreated = true;
        }

        if(isPressed = false && isCreated == true) 
            {
            GameObject projectileInstance = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);

            isCreated = false;

            }



        /*if (bulletPrefab)
            Destroy(Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity), 3); */
    }

}
