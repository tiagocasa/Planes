using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaPickup : MonoBehaviour
{
    public float velocity;

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
            GameControl.instance.ImaPickUp();
            gameObject.transform.position = new Vector2(15, 25);
            FindObjectOfType<AudioManager>().Play("MagnetField");
            FindObjectOfType<AudioManager>().Play("itens");
            //gameObject.SetActive(false);
        }
    }

    void Update()
    {

        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        if (GameControl.instance.gameOver == true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            velocity = GameControl.instance.scrollSpeed;
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
            if (transform.position.x < -16)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
