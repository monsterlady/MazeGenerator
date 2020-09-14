using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaivor : MonoBehaviour
{
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        float height = map.GetComponent<NormalMap>().height;
        float width = map.GetComponent<NormalMap>().width;
        float z = (float) (-10) * (float) Math.Pow(width, 0.5);
        gameObject.transform.position = new Vector3(height / 2,width / 2,z);
        float size = height > width ? height / 2 + 5: width / 2 + 5;
        gameObject.GetComponent<Camera>().orthographicSize = size;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
