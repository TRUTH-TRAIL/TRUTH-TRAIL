using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float F { get { return G + H; } }
    public float G { get; private set; } = 0;
    public float H { get; private set; } = 0;

    public Coord coord;

    bool[] isWall = new bool[4];

    public void SetCoord(int col, int row)
    {
        coord = new Coord(col, row);
    }
}

public struct Coord
{
    private int col;
    private int row;

    public Coord(int col, int row)
    {
        this.col = col;
        this.row = row;
    }
    public Vector3 GetCoord()
    {
        return new Vector3(col, row, 0);
    }
}
