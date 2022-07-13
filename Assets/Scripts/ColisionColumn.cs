using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionColumn : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameControl.instance.isDash)
        {
            if (other.gameObject.name == "Bird")
            {
                gameObject.SetActive(false);
                
            }
        }
    }

}
