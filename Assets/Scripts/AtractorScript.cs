using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtractorScript : MonoBehaviour
{
    public float AtracSpeed;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Bird")
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, AtracSpeed * Time.deltaTime);
        }
    }
}
