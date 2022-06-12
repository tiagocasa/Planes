    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collumpool : MonoBehaviour
{
    public int columPoolSize = 1;
    public GameObject columPrefab;
    public float spawnRate = 2f;
    public float ColumMin = -1.5f;
    public float ColumMax = 0f;

    private GameObject[] colums;
    private Vector2 objectPoolPosition = new Vector2(10f, 0f);
    private float timeSince;
    private float spawnXPosition = 10f;
    private int currentColum = 0;

    public GameObject coinPrefab;
    public int coinPoolSize = 1;
    public float coinSpawRate = 2f;
    public float coinMin = 1.5f;
    public float coinMax = 0f;
    private GameObject[] coins;
    private Vector2 coinPoolPosition = new Vector2(18f, 30f);
    private int currentCoin = 0;
    public float randomCoin;

    public GameObject dashPrefab;
    public int dashPoolSize = 1;
    public float dashSpawRate = 2f;
    public float dashMin = 1.5f;
    public float dashMax = 0f;
    private GameObject[] dash;
    private Vector2 dashPoolPosition = new Vector2(18f, 30f);
    private int currentDash = 0;
    public float randomDash;



    // Start is called before the first frame update
    void Start()
    {
        // Iniciar Colunas
        colums = new GameObject[columPoolSize];
        for (int i = 0; i < columPoolSize; i++)
        {
            colums[i] = (GameObject)Instantiate(columPrefab, objectPoolPosition, Quaternion.identity);
        }

        // Iniciar Moedas
        coins = new GameObject[coinPoolSize];
        for (int i = 0; i < coinPoolSize; i++)
        {
            coins[i] = (GameObject)Instantiate(coinPrefab, coinPoolPosition, Quaternion.identity);
        }

        dash = new GameObject[dashPoolSize];
        for (int i = 0; i < dashPoolSize; i++)
        {
            dash[i] = (GameObject)Instantiate(dashPrefab, dashPoolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSince += Time.deltaTime;

        if(GameControl.instance.gameOver == false && timeSince >= spawnRate)
        {
            timeSince = 0;
            float spawnYPosition = UnityEngine.Random.Range(ColumMin, ColumMax);
  
            colums[currentColum].transform.position = new Vector2(spawnXPosition, spawnYPosition);
 
            currentColum++;
            if (currentColum>= columPoolSize)
            {
                currentColum = 0;
            }


            // Spawn das moedas no time das colunas
            if (UnityEngine.Random.Range(0, 100) < randomCoin) 
            { 
                spawnYPosition = UnityEngine.Random.Range(coinMin, coinMax);
                coins[currentCoin].transform.position = new Vector2(spawnXPosition+(UnityEngine.Random.Range(4,10)), spawnYPosition);
                currentCoin++;
                if (currentCoin >= coinPoolSize)
                {
                    currentCoin = 0;
                }
            }

            // Spawn dos dash no time das colunas
            if (UnityEngine.Random.Range(0, 100) < randomDash)
            {
                spawnYPosition = UnityEngine.Random.Range(dashMin, dashMax);
                dash[currentDash].transform.position = new Vector2(spawnXPosition + (UnityEngine.Random.Range(4, 10)), spawnYPosition);
                currentDash++;
                if (currentDash >= dashPoolSize)
                {
                    currentDash = 0;
                }
            }







        }

       
    }
}
