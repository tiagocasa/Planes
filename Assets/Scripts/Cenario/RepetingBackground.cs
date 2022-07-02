using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetingBackground : MonoBehaviour
{
   
    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;
    public float timermap = 0;
    private bool transitionGround = false;

    [SerializeField] private Sprite[] spriteList;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = groundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -groundHorizontalLength)
        {
            RepositionBackground();
        }
        timermap += Time.deltaTime;

    }

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;

        if (transitionGround)
        {
            transitionGround = false;
            GameControl.instance.groundIndex++;
            timermap = 0;
        }
        if (timermap > 6)
        {
            GameControl.instance.groundIndex++;
            transitionGround = true;
        }

        if(GameControl.instance.groundIndex >2)
        {
            GameControl.instance.groundIndex = 0;
        }

        spriteRenderer.sprite = spriteList[GameControl.instance.groundIndex];
    }

   

   

}
