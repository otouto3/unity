using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roulette : MonoBehaviour
{
    float angle = 0.0f;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            angle = 1.0f;
        }
        transform.Rotate(new Vector3(0, 0, angle));
        angle *= 0.95f;

    }
}
