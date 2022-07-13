using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasolinaScript : MonoBehaviour
{

    public Slider slider;

    void Update()
    {
        if (!GameControl.instance.gameOver)
        {
            slider.value = GameControl.instance.gasoline;
        }
    }
}
