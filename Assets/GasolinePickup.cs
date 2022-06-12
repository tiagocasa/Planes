using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasolinePickup : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
    }

    public void OnObjectSpawn()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Bird")
        {
            GameControl.instance.GasolinaPickUp();
            gameObject.transform.position = new Vector2(15, 25);
            FindObjectOfType<AudioManager>().Play("itens");
            //gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (GameControl.instance.gameOver == true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
