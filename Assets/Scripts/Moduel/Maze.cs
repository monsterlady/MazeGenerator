using UnityEngine;

namespace Moduel
{
    public abstract class Maze 
    {
        internal int[,] _map;
        internal Vector2 _originPos;
        internal int _hight;
        internal int _width;

        public int[,] map 
        {
            get
            { 
                return _map;
            }
        }
        
        public int height 
        {
            get
            {
                return _hight;
            }
        }

        public int width
        {
            get 
            {
                return _width;
            }
        }

        public abstract void GenerateMaze(int width, int height);
    }
}