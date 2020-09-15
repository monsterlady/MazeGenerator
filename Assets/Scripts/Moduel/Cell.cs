using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Astar;
using UnityEngine;

public class Cell : AstarNode
{
    private int x, y;
    private  int cost;
    private List<Cell> neighbors;
    private GameObject cube;

    public Cell(int x, int y,int cost,GameObject cube)
    {
        this.x = x;
        this.y = y;
        this.cost = cost;
        this.neighbors = new List<Cell>();
        this.cube = cube;
    }

    public GameObject Cube
    {
        get => cube;
        set => cube = value;
    }

    public int X => x;

    public int Y => y;
    
    public IEnumerable<AstarNode> Neighbors => neighbors;
    public int costTo(AstarNode neighbor)
    {
        return ((Cell) neighbor).Cost;
    }

    public int Cost => cost;

    public int estimateTo(AstarNode destination)
    {
        return Math.Abs(((Cell) destination).X - ((Cell) this).X) + Math.Abs(((Cell) destination).Y - ((Cell) this).Y);
    }

    public void addNeighbor(Cell nei)
    {
        neighbors.Add(nei);
    }

    public override string ToString()
    {
        return "Cell: (" + X + ", " + Y + ")" + (Cost > 0 ? " Path" : "Wall") + " has " +Neighbors.Count() + " neighbor(s)";
    }
}
