using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingGround : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public bool dash = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0); 
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameControl.instance.gameOver == true)
        {
            rb2d.velocity = Vector2.zero;
        }

    }

    public void ChangeVelocity()
    {
            dash = true;
            rb2d.velocity *= 10;
    }

   
}
