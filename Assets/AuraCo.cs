using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraCo : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ColumnSprite")
        {
            other.transform.position = new Vector2(15, 25);
            FindObjectOfType<AudioManager>().Play("ExploArvo");
        }
        else if (other.gameObject.name == "ColumnSprite (1)")
        {
            other.transform.position = new Vector2(15, 25);
            FindObjectOfType<AudioManager>().Play("ExploArvo");
        }
    }


}
