using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FORCE : MonoBehaviour


{

    [SerializeField] private float forceAmount = 10f;

    // [SerializeField] private Transform ShootFromThis;
    
    private Rigidbody _rigidbody;

    public bool homo = true;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        homo = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (homo == true)
        {
           
            _rigidbody.AddForce(Vector3.forward * forceAmount);

        }


    }
}
