using System;
using UnityEngine;

public class GridSystem
{
    public int width;
    public int height;
    public int centralOccupance;
    public float cellSize;
    public bool debug;
    public CellObject[,] cellValues;
    public Vector2 origPos;

    public GridSystem(int width, int height, float cellSize, Vector2 originPosition, Func<Vector2, CellObject> CreateCell, bool debug, int centralOccupance)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.debug = debug;
        origPos = originPosition;
        cellValues = new CellObject[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cellValues[i, j] = CreateCell(GetWorldPosition(i, j));
                if ((i >= width / 2 - centralOccupance && i < width / 2 + centralOccupance) && (j >= height / 2 - centralOccupance && j < height / 2 + centralOccupance))
                {
                    cellValues[i,j].occupied = true;
                }
            }
        }
        if (debug)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.red, 100f);
                    Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.cyan, 100f);

                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.cyan, 100f);
        }

        this.centralOccupance = centralOccupance;
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize + origPos;
    }
    public Vector2Int GetCellIndex(Vector2 worldPos)
    {
        return new Vector2Int(Mathf.FloorToInt((worldPos - origPos).x / cellSize), Mathf.FloorToInt((worldPos - origPos).y / cellSize));
    }

    public void SetValue(int x, int y)
    {
        if (x < 0 || y < 0) return;
        if (x > width || y > height) return;

        //cellValues[x, y].OnClick();
    }

    public void SetValue(Vector2 worldPos)
    {
        Vector2Int cellIndex = GetCellIndex(worldPos);
        SetValue(cellIndex.x, cellIndex.y);
    }

    public CellObject GetValue(Vector2 worldPos)
    {
        Vector2Int cellIndex = GetCellIndex(worldPos);
        return GetValue(cellIndex.x, cellIndex.y);
    }

    public CellObject GetValue(int x, int y)
    {
        if (x < 0 || y < 0) return default;
        if (x > width || y > height) return default;

        return cellValues[x, y];
    }
}
