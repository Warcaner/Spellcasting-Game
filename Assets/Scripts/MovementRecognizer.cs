using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.IO;
using UnityEngine.Events;
using System.Linq;

public class MovementRecognizer : MonoBehaviour
{

    Moves moves;



    // experimental, remove if doesnt work
    //public GameObject recognizedGestObj;

    // Start is called before the first frame update
    void Start()
    {

        moves = GetComponent<Moves>();
        ;
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (var item in gestureFiles)
        {
            moves.trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
        // experimental forloop that replaces combo.add to avoid the out of bounds error



    }

    // Update is called once per frame
    void Update()
    {


        moves.Movementscan();
        moves.SetTimer(0);



     }

 }

