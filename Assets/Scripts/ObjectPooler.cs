using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion


    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    
    public GameObject SpawnPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            //Print("Pool com a tag " + tag + "não existe.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        //objectToSpawn.GetComponent<Rigidbody2D>().velocity = new Vector3(-1,0,0);

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
            
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        objectToSpawn.SetActive(true);
        if (tag == "postes" || tag =="arvore" || tag =="torre" || tag=="moinho")
        {
            objectToSpawn.transform.GetChild(0).gameObject.SetActive(true);
            objectToSpawn.transform.GetChild(1).gameObject.SetActive(true);
        }
        return objectToSpawn;
    }

}
