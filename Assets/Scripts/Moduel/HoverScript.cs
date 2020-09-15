using System;
using UnityEngine;

namespace Moduel
{
    public class HoverScript:MonoBehaviour
    {
        //public int x,y;
        public MazeGenerator mazeGenerator;

        void OnMouseOver()
        {
            Debug.Log(gameObject.transform.position.x + " " + gameObject.transform.position.y);
            mazeGenerator.GeneratePath((int)gameObject.transform.position.x,(int)gameObject.transform.position.y);
        }

        void OnMouseExit()
        {
            Debug.Log("鼠标移开！");
            mazeGenerator.resetPath();
        }
        
    }
}