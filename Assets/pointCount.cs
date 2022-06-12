using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointCount : MonoBehaviour
{
   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Bird")
        {
            GameControl.instance.CoinPickUp();
            gameObject.transform.parent.position = new Vector2(15, 25);
            FindObjectOfType<AudioManager>().Play("Moeda");
            //gameObject.SetActive(false);
        }
    }
}
