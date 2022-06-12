using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MyPooler : MonoBehaviour
{
    [System.Serializable]
    public class Spawnaveis
    {
        public string Name;
        public int PoolSize;
        public GameObject Prefab;
        public float spawnRate;
    }

    private float timeSince;
    private string[] ToSpawn;

    public List<Spawnaveis> ListaPool;

    void Start()
    {
        
        foreach (Spawnaveis pool in ListaPool)
        {
            ToSpawn = new string[pool.PoolSize];
            for (int i = 0; i<pool.PoolSize; i++)
            {
                ToSpawn[i] = pool.Name;
            }
        }

        Array.ForEach(ToSpawn, UnityEngine.Debug.Log);

        //UnityEngine.Debug.Log(ToSpawn[]);


        //ToSpawn = new GameObject[ListaPool.Count, 3];

        //for (int i = 0; i< ToSpawn[])
        //colums = new GameObject[columPoolSize];
        //for (int i = 0; i < columPoolSize; i++)
        //{
        //    colums[i] = (GameObject)Instantiate(columPrefab, objectPoolPosition, Quaternion.identity);
        //}

        //foreach (Spawnaveis pool in ListaPool)
        //{

        //}
    }


    void update()
    {

        //timeSince += Time.deltaTime;

        //foreach(Spawnaveis pool in ListaPool)
        //{
        //    if (GameControl.instance.gameOver == false && timeSince >= spawnRate)
        //    {
        //        timeSince = 0;
        //        float spawnYPosition = UnityEngine.Random.Range(-1, 1);
        //        pool.Prefab.transform.position = new Vector2(spawnXPosition, spawnYPosition);

        //    currentColum++;
        //    if (currentColum >= columPoolSize)
        //    {
        //        currentColum = 0;
        //    }
        //}
}





    // PPRIMEIRO TESTE NÃO DEU

    //private float timeSince;

    //[System.Serializable]
    //public class SpawnPro
    //{
    //    public string Name;
    //    public int PoolSize;
    //    public GameObject Prefab;

    //}

    //public List<SpawnPro> pools;
    //public List<pools> Objetos;
    ////public Dictionary<string, Queue<GameObject>> poolDictionary;

    //void Start()
    //{
    //    //poolDictionary = new Dictionary<string, Queue<GameObject>>();

    //    //foreach (SpawnPro pool in pools)
    //    //{
    //    //    Queue<GameObject> objectPool = new Queue<GameObject>();

    //    //    for (int i = 0; i < pool.PoolSize; i++)
    //    //    {
    //    //        GameObject obj = Instantiate(pool.Prefab);
    //    //        obj.SetActive(false);
    //    //        objectPool.Enqueue(obj);
    //    //    }

    //    //    poolDictionary.Add(pool.Name, objectPool);
    //    //}
    //}

    //void Update()
    //{     
      
    //}
}
