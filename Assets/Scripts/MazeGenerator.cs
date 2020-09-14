using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Moduel;
using UnityEngine;
public enum BlockDirection
{
    Up,
    Down,
    Right,
    Left
}

public class MazeBlock
{
    public int x, y;
    public BlockDirection direction;

    public MazeBlock(int x, int y,BlockDirection blockDirection)
    {
        this.x = x;
        this.y = y;
        this.direction = blockDirection;
    }
}

public class MazeGenerator : Maze
{
    public int[,] Map => _map;
    public Cell[,] _cells;

    public override void GenerateMaze(int width, int height)
    {
        // 设置迷宫数据
        _map = new int[height, width];
        _cells = new Cell[width,height];
        _hight = height;
        _width = width;
        
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                _map[i, j] = 0;
            }
        }

        // 设置起点为目标格
        var targetX = 0;
        var targetY = 0;
        _map[targetX, targetY] = 1;

        // 记录邻墙
        var rpBlockPos = new List<MazeBlock>();
        // 如果上方有临墙，将临墙加入列表
        if (targetX > 1)
        {
            var block = new MazeBlock(targetX - 1, targetY, BlockDirection.Up);
            rpBlockPos.Add(block);
        }

        // 如果右方有临墙，将临墙加入列表
        if (targetY < width - 2)
        {
            var block = new MazeBlock(targetX, targetY + 1, BlockDirection.Right);
            rpBlockPos.Add(block);
        }

        // 如果下方有临墙，将临墙加入列表
        if (targetX < height - 2)
        {
            var block = new MazeBlock(targetX + 1, targetY, BlockDirection.Down);
            rpBlockPos.Add(block);
        }

        // 如果左方有临墙，将临墙加入列表
        if (targetY > 1)
        {
            var block = new MazeBlock(targetX, targetY - 1, BlockDirection.Left);
            rpBlockPos.Add(block);
        }

        while (rpBlockPos.Count > 0)
        {
            // 随机选一面墙
            long tick = System.DateTime.Now.Ticks;
            System.Random ran = new System.Random((int) (tick & 0xffffffffL) | (int) (tick >> 32));
            int blockIndex = ran.Next(0, rpBlockPos.Count);

            switch (rpBlockPos[blockIndex].direction)
            {
                case BlockDirection.Up:
                    targetX = rpBlockPos[blockIndex].x - 1;
                    targetY = rpBlockPos[blockIndex].y;
                    break;
                case BlockDirection.Down:
                    targetX = rpBlockPos[blockIndex].x + 1;
                    targetY = rpBlockPos[blockIndex].y;
                    break;
                case BlockDirection.Left:
                    targetX = rpBlockPos[blockIndex].x;
                    targetY = rpBlockPos[blockIndex].y - 1;
                    break;
                case BlockDirection.Right:
                    targetX = rpBlockPos[blockIndex].x;
                    targetY = rpBlockPos[blockIndex].y + 1;
                    break;
            }

            //如果目标墙尚未料筒
            if (_map[targetX, targetY] == 0)
            {
                // 连通目标墙
                _map[rpBlockPos[blockIndex].x, rpBlockPos[blockIndex].y] = 1;
                _map[targetX, targetY] = 1;

                // 添加目标墙的临墙
                if (rpBlockPos[blockIndex].direction != BlockDirection.Down &&
                    targetX > 1 && _map[targetX - 1, targetY] == 0 && _map[targetX - 2, targetY] == 0)
                {
                    var block = new MazeBlock(targetX - 1, targetY, BlockDirection.Up);
                    rpBlockPos.Add(block);
                }

                if (rpBlockPos[blockIndex].direction != BlockDirection.Left &&
                    targetY < width - 2 && _map[targetX, targetY + 1] == 0 && _map[targetX, targetY + 2] == 0)
                {
                    var block = new MazeBlock(targetX, targetY + 1, BlockDirection.Right);
                    rpBlockPos.Add(block);
                }

                if (rpBlockPos[blockIndex].direction != BlockDirection.Up &&
                    targetX < height - 2 && _map[targetX + 1, targetY] == 0 && _map[targetX + 2, targetY] == 0)
                {
                    var block = new MazeBlock(targetX + 1, targetY, BlockDirection.Down);
                    rpBlockPos.Add(block);
                }

                if (rpBlockPos[blockIndex].direction != BlockDirection.Right &&
                    targetY > 1 && _map[targetX, targetY - 1] == 0 && _map[targetX, targetY - 2] == 0)
                {
                    var block = new MazeBlock(targetX, targetY - 1, BlockDirection.Left);
                    rpBlockPos.Add(block);
                }
            }

            // 移除目标墙
            rpBlockPos.RemoveAt(blockIndex);


            // var rbDirection = new List<BlockDirection>();
            // var blockStack = new List<MazeBlock>();
            // blockStack.Add(new MazeBlock(targetX,targetY));
            // System.Random random = new System.Random();
            // while(blockStack.Count != 0)
            // {
            //     // int ri = random.Next(blockStack.Count);
            //     // MazeBlock current = blockStack[ri];
            //     // targetX = current.x;
            //     // targetY = current.y;
            //     // _map[targetX, targetY] = 1;
            //     // blockStack.RemoveAt(ri);
            //     // 检测周围有没有未连通的格子
            //     rbDirection.Clear();
            //     // 检测上方
            //     if (targetX >= 0 && targetX < width && targetY < height && targetY >= 0 && targetY + 1 < height)
            //     {
            //         if (_map[targetX, targetY + 1] == 0)
            //         {
            //             rbDirection.Add(BlockDirection.Up);
            //         }
            //     }
            //     // 检测右方
            //     if (targetX >= 0 && targetX < width && targetY < height && targetY >= 0 && targetX + 1 < width)
            //     {
            //         if (_map[targetX + 1, targetY] == 0)
            //         {
            //             rbDirection.Add(BlockDirection.Right);
            //         }
            //     }
            //     // 检测下方
            //     if (targetX >= 0 && targetX < width && targetY < height && targetY >= 0 && targetY - 1 >= 0)
            //     {
            //         if (_map[targetX, targetY - 1] == 0)
            //         {
            //             rbDirection.Add(BlockDirection.Down);
            //         }
            //     }
            //     // 检测左方
            //     if (targetX >= 0 && targetX < width && targetY < height && targetY >= 0 && targetX - 1 >= 0)
            //     {
            //         if ( _map[targetX - 1, targetY] == 0)
            //         {
            //            rbDirection.Add(BlockDirection.Left);
            //         }
            //     }
            //     
            //     // 选出下一个当前格
            //     if(rbDirection.Count != 0) 
            //     {
            //         // 标记当前格
            //         if(targetX >= 0 && targetX < height && targetY >= 0 && targetY < width)
            //             _map[targetX, targetY] = 1;
            //         long tick = System.DateTime.Now.Ticks;
            //         System.Random ran = new System.Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            //         int blockIndex = ran.Next(0, rbDirection.Count);
            //         // 将当前格入栈
            //         // 连通邻格，并将邻格指定为当前格
            //         switch(rbDirection[blockIndex]) 
            //         {
            //             case BlockDirection.Up:
            //                 _map[targetX, targetY + 1] = 1;
            //                 targetY ++;
            //                 break;
            //             case BlockDirection.Down:
            //                 _map[targetX, targetY - 1] = 1;
            //                 targetY --;
            //                 break;
            //             case BlockDirection.Left:
            //                 _map[targetX - 1, targetY] = 1;
            //                 targetX --;
            //                 break;
            //             case BlockDirection.Right:
            //                 _map[targetX + 1, targetY] = 1;
            //                 targetX ++;
            //                 break;
            //         }
            //         var block = new MazeBlock(targetX, targetY);
            //         Debug.Log("下一个节点: " + block.x + " " + block.y);
            //         blockStack.Add(block);
            //     }
            //} 
        }
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {   
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(i,j,0); 
                _cells[i,j] = new Cell(i,j,10);
                if (_map[i, j] == 0)
                {
                    obj.GetComponent<Renderer>().material.color = Color.red;
                    _cells[i,j] = new Cell(i,j,-1);
                }
                else if(i== 0 & j == 0)
                {
                    obj.GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    obj.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
        addNeighbors();
        foreach (var eachCell in _cells)
        {
            Debug.Log(eachCell.ToString());
        }
    }

    private void addNeighbors()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j = 0 ; j < height; j++){
              //UP
              if (j + 1 < height)
              {
                  if (_cells[i, j + 1].Cost > 0)
                  {
                      _cells[i,j].addNeighbor(_cells[i,j+1]);
                  }
              }
              //DOWN
              if (j - 1 >= 0)
              {
                  //is path
                  if (_cells[i, j - 1].Cost > 0)
                  {
                      _cells[i,j].addNeighbor(_cells[i,j-1]);
                  }
              }
              //RIGHT
              if (i + 1 < width)
              {
                  //is path
                  if (_cells[i + 1, j ].Cost > 0)
                  {
                      _cells[i,j].addNeighbor(_cells[i + 1,j]);
                  }
              }
              //LEFT
              if (i - 1 >= 0)
              {
                  //is path
                  if (_cells[i - 1, j ].Cost > 0)
                  {
                      _cells[i,j].addNeighbor(_cells[i - 1, j ]);
                  }
              }
            }
        }
    }
}
