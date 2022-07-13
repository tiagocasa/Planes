using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSpawn : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(-3, 0,0 );
    }
}
