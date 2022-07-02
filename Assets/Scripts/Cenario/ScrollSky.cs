using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSky : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float scrollVelocity;

    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;


    [SerializeField] private Sprite[] spriteList;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            scrollVelocity = 0.1f * GameControl.instance.scrollSpeed;
            rb2d.velocity = new Vector2(scrollVelocity, 0);
        }
    }

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;

        spriteRenderer.sprite = spriteList[GameControl.instance.skyIndex];

        if (GameControl.instance.transitionSky)
        {
            GameControl.instance.transitionSky = false;
            GameControl.instance.skyIndex++;

        }
        if (GameControl.instance.skyIndex == spriteList.Length)
        {
            GameControl.instance.skyIndex = 0;
        }

       
    }
}

