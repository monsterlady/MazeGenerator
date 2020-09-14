using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMap : MonoBehaviour
{
    public int height, width;
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator mazeGenerator = new MazeGenerator();
        mazeGenerator.GenerateMaze(width,height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
