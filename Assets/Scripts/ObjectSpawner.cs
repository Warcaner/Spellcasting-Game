using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objects;

    public void Spawn(string ObjectName)
    {
        foreach (var item in objects)
        {
            item.SetActive(ObjectName == item.name);
        }
    }
}
