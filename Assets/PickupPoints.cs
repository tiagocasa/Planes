using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoints : MonoBehaviour, IPooledObject
{
    private Rigidbody2D rb2d;
    private float scrollVelocity;
    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scrollVelocity = GameControl.instance.scrollSpeed;
        rb2d.velocity = new Vector2(scrollVelocity, 0);
    }

    public void OnObjectSpawn()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scrollVelocity = GameControl.instance.scrollSpeed;
        rb2d.velocity = new Vector2(scrollVelocity, 0);
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //        if (other.gameObject.name == "Bird")
    //        {

    //            GameControl.instance.CoinPickUp();
    //            gameObject.transform.position = new Vector2(15, 25);
    //            FindObjectOfType<AudioManager>().Play("Moeda");
    //            //gameObject.SetActive(false);
    //        }
    //}

    void Update()
    {
        if (GameControl.instance.gameOver == true)
        {
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            scrollVelocity = GameControl.instance.scrollSpeed;
            rb2d.velocity = new Vector2(scrollVelocity, 0);
        }
    }

    public void StopCoin() { }

}
