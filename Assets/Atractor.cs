using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Atractor : MonoBehaviour
{
    public float AtracSpeed;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameControl.instance.isIma)
        {
            if (other.gameObject.name == "Bird")
            {
                FindObjectOfType<AudioManager>().Play("sugada");
                gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (GameControl.instance.isIma)
        {
            if (other.gameObject.name == "Bird")
            {
                gameObject.transform.parent.position = Vector2.MoveTowards(transform.parent.position, other.transform.position, AtracSpeed * Time.deltaTime);
            }
        }
    }
}
