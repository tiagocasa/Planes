using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float timeSince=0;

    private Vector3 posicaoDash;
    private Vector3 posicaoMoeda;
    private Vector3 posicaoGasolina;
    private Vector3 posicaoIma;

    public float ChanceDash;
    private float ChanceMoeda;
    public float ChanceGasolina;
    public float ChanceIma;

    public float Ymax;
    public float Ymin;
    public float Xmax;
    public float Xmin;


    void start()
    {
        posicaoDash = new Vector3(22, 0, 0);
        posicaoMoeda = new Vector3(22, 0, 0);
        posicaoGasolina = new Vector3(22, 0, 0);
        posicaoIma = new Vector3(22, 0, 0);
        timeSince = 0;
        
    }

    void Update()
    {
        timeSince += Time.deltaTime;

        if (GameControl.instance.gameOver == false && timeSince >= 3)
        {
            timeSince = 0;
            ObjectPooler.Instance.SpawnPool("colunas", transform.position, Quaternion.identity);

            ChanceMoeda = PlayerPrefs.GetInt("APARECER MOEDAS", 0) * 10f;
            if (UnityEngine.Random.Range(0, 100) < ChanceMoeda)
            {
                float Ypos = UnityEngine.Random.Range(Ymin, Ymax);
                float Xpos = UnityEngine.Random.Range(Xmin, Xmax);
                posicaoMoeda = new Vector3(Xpos, Ypos, 0);
                ObjectPooler.Instance.SpawnPool("Coin", posicaoMoeda, Quaternion.identity);
            }



            if (PlayerPrefs.GetInt("TEMPO DO TURBO", 0) == 0)
            {

            }
            else
            {
                if (UnityEngine.Random.Range(0, 100) < ChanceDash)
                {
                    float Ypos = UnityEngine.Random.Range(Ymin, Ymax);
                    float Xpos = UnityEngine.Random.Range(Xmin, Xmax);
                    posicaoDash = new Vector3(Xpos, Ypos, 0);
                    ObjectPooler.Instance.SpawnPool("Dash", posicaoDash, Quaternion.identity);
                }
            }




            if (PlayerPrefs.GetInt("RECUPERAR GASOLINA", 0) == 0)
            {

            }
            else
            {
                if (UnityEngine.Random.Range(0, 100) < ChanceGasolina)
                {
                    float Ypos = UnityEngine.Random.Range(Ymin, Ymax);
                    float Xpos = UnityEngine.Random.Range(Xmin, Xmax);
                    posicaoGasolina = new Vector3(Xpos, Ypos, 0);
                    ObjectPooler.Instance.SpawnPool("Gasolina", posicaoGasolina, Quaternion.identity);
                }
            }


            if (PlayerPrefs.GetInt("TEMPO DO IMÃ", 0) == 0)
            {

            }
            else
            {
                if (UnityEngine.Random.Range(0, 100) < ChanceIma)
                {
                    float Ypos = UnityEngine.Random.Range(Ymin, Ymax);
                    float Xpos = UnityEngine.Random.Range(Xmin, Xmax);
                    posicaoIma = new Vector3(Xpos, Ypos, 0);
                    ObjectPooler.Instance.SpawnPool("Ima", posicaoIma, Quaternion.identity);
                }
            }
        }

    }
}
