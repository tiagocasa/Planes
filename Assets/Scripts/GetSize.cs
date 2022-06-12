using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log(width);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
