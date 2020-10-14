using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class director : MonoBehaviour
{
    GameObject roulette;
    float angle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        roulette = GameObject.Find("roulette");

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            angle = 1.0f;
        }
        roulette.transform.Rotate(new Vector3(0, 0, angle));
        angle *= 0.95f;

    }
}
