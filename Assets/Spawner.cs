using System;
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

    public float timeLimit;

    private float[] lista = {0.5f, 1f, 1.5f, 2f};
    private int index;

    private float limitCoin;
    private float timerCoin;

    private float limitDash;
    private float timerDash;

    private float limitMagnet;
    private float timerMagnet;

    private float limitGas;
    private float timerGas;

    private void Start()
    {
        posicaoDash = new Vector3(22, 0, 0);
        posicaoMoeda = new Vector3(22, 0, 0);
        posicaoGasolina = new Vector3(22, 0, 0);
        posicaoIma = new Vector3(22, 0, 0);
        AtualizarTimers();
    }

    void Update()
    {
        if (!GameControl.instance.gameOver)
        {
            timeSince += Time.deltaTime;
            timerCoin += Time.deltaTime;
            timerDash += Time.deltaTime;
            timerMagnet += Time.deltaTime;
            timerGas += Time.deltaTime;

            if (timerCoin > limitCoin)
            {
                SpawnMoeda();
            }
            if (timerDash > limitDash)
            {
                SpawnDash();
            }
            if (timerMagnet > limitMagnet)
            {
                SpawnMagnet();
            }
            if (timerGas > limitGas)
            {
                SpawnGas();
            }
            if (timeSince >= timeLimit)
            {
                SpawnColumn();
            }
        }
       
    }

    public void SpawnMoeda()
    {
        ChanceMoeda = 100;
        if (UnityEngine.Random.Range(0, 100) <= ChanceMoeda)
        {
            float Ypos = UnityEngine.Random.Range(Ymin, Ymax);
            //float Xpos = UnityEngine.Random.Range(Xmin, Xmax);
            float Xpos = 22;
            if (Xpos < Xmin) { Xpos = Xmin; };
            if (Xpos > Xmax) { Xpos = Xmax; };
            posicaoMoeda = new Vector3(Xpos, Ypos, 0);
            ObjectPooler.Instance.SpawnPool("Coin", posicaoMoeda, Quaternion.identity);
        }
        timerCoin = -10;
    }

    public void SpawnDash()
    {
        if (!GameControl.instance.isDash)
        {
            if (UnityEngine.Random.Range(0, 100) < ChanceDash)
            {
                float Ypos = UnityEngine.Random.Range(0, Ymax);
                float Xpos = 22;
                if (Xpos < Xmin) { Xpos = Xmin; };
                if (Xpos > Xmax) { Xpos = Xmax; };
                posicaoDash = new Vector3(Xpos, Ypos, 0);
                ObjectPooler.Instance.SpawnPool("Dash", posicaoDash, Quaternion.identity);
            }
        }
        timerDash = -10;
    }
    public void SpawnMagnet()
    {
        if (UnityEngine.Random.Range(0, 100) < ChanceIma)
        {
            float Ypos = UnityEngine.Random.Range(Ymin, Ymax);
            float Xpos = 22;
            if (Xpos < Xmin) { Xpos = Xmin; };
            if (Xpos > Xmax) { Xpos = Xmax; };
            posicaoIma = new Vector3(Xpos, Ypos, 0);
            ObjectPooler.Instance.SpawnPool("Ima", posicaoIma, Quaternion.identity);
        }
        timerMagnet = -10;
    }

    public void SpawnGas()
    {
        if (UnityEngine.Random.Range(0, 100) < ChanceGasolina)
        {
            float Ypos = UnityEngine.Random.Range(Ymin, Ymax);
            float Xpos = 22;
            if (Xpos < Xmin) { Xpos = Xmin; };
            if (Xpos > Xmax) { Xpos = Xmax; };
            posicaoGasolina = new Vector3(Xpos, Ypos, 0);
            ObjectPooler.Instance.SpawnPool("Gasolina", posicaoGasolina, Quaternion.identity);
        }
        timerGas = -10;
    }

    public void SpawnColumn()
    {




        if (GameControl.instance.timedash <= 4)
        {
            if (GameControl.instance.orderMap == 0)
            {
                ObjectPooler.Instance.SpawnPool("postes", transform.position, Quaternion.identity);
            }
            else if (GameControl.instance.orderMap == 1)
            {
                ObjectPooler.Instance.SpawnPool("moinho", transform.position, Quaternion.identity);
            }
            else if (GameControl.instance.orderMap == 2)
            {
                ObjectPooler.Instance.SpawnPool("torre", transform.position, Quaternion.identity);
            }
            else if (GameControl.instance.orderMap == 3)
            {
                ObjectPooler.Instance.SpawnPool("arvore", transform.position, Quaternion.identity);
            }
        }

        AtualizarTimers();

    }
    public void RemoveElement<T>(ref T[] arr, int index)
    {
        for (int i = index; i<arr.Length -1; i++)
        {
            arr[i] = arr[i + 1];
        }
        Array.Resize(ref arr, arr.Length - 1);
    }
    
    public void AtualizarTimers()
    {
        timeSince = 0;
        timerGas = 0;
        timerDash = 0;
        timerMagnet = 0;
        timerCoin = 0;
        Array.Resize(ref lista, 4);
        lista[0] = 0.8f;
        lista[1] = 1.2f;
        lista[2] = 1.6f;
        lista[3] = 2f;

        if (GameControl.instance.isDash)
        {
            timeLimit = UnityEngine.Random.Range(2.5f, 4.5f) / 5;
        }
        else
        {
            timeLimit = UnityEngine.Random.Range(2.5f, 4.5f) ;
        }
        index = UnityEngine.Random.Range(0, 3);
        limitCoin = timeLimit - (lista[index]*(timeLimit/2.5f));
        RemoveElement(ref lista, index);
        index = UnityEngine.Random.Range(0, 2);
        limitDash = timeLimit - (lista[index] * (timeLimit / 2.5f));
        RemoveElement(ref lista, index);
        index = UnityEngine.Random.Range(0, 1);
        limitMagnet = timeLimit - (lista[index] * (timeLimit / 2.5f));
        RemoveElement(ref lista, index);
        limitGas = timeLimit - (lista[0] * (timeLimit / 2.5f));

    }
}
