using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour, IPooledObject
{
    public float colunaInferiorMax;
    public float colunaInferiorMin;
    public float GapMax;
    public float GapMin;

    public void Start()
    {
        float spawnYPosition = UnityEngine.Random.Range(colunaInferiorMin, colunaInferiorMax);
        float spawnGapPosition = UnityEngine.Random.Range(GapMax, GapMin);
        transform.GetChild(0).position = new Vector2(22, spawnYPosition);
        transform.GetChild(1).position = new Vector2(22, spawnYPosition+spawnGapPosition);
        GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
    }

    public void OnObjectSpawn()
    {
        float spawnYPosition = UnityEngine.Random.Range(colunaInferiorMin, colunaInferiorMax);
        float spawnGapPosition = UnityEngine.Random.Range(GapMax, GapMin);
        transform.GetChild(0).position = new Vector2(22, spawnYPosition);
        transform.GetChild(1).position = new Vector2(22, spawnYPosition + spawnGapPosition);
        GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
    }

    void Update()
    {
        if (GameControl.instance.gameOver == true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.GetComponent<bird>() != null)
        {
            GameControl.instance.BirdScored();
        }
    }

}
