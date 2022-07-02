using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPredioLonge : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float scrollVelocity;

    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;

    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = groundCollider.size.x;
        rb2d = GetComponent<Rigidbody2D>();

        UpdateSpeed();
    }



    // Update is called once per frame
    void Update()
    {
        UpdateSpeed();


        if (transform.position.x < -groundHorizontalLength)
        {
            RepositionBackground();
        }
    }

    private void UpdateSpeed()
    {
        if (GameControl.instance.gameOver == true)
        {
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            scrollVelocity = 0.15f * GameControl.instance.scrollSpeed;
            rb2d.velocity = new Vector2(scrollVelocity, 0);
        }
    }

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}