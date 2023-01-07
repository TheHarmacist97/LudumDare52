using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    int width;
    int height;
    float cellSize;
    int[,] cellValues;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width/2;
        this.height = height/2;
        this.cellSize = cellSize;
        cellValues = new int[width, height];

        for (int i = -width; i < width; i++)
        {
            for (int j = -height; j < height; j++)
            {
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j+1), Color.red, 100f);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i+1, j), Color.cyan, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(-width, height), GetWorldPosition(width, height), Color.red, 100f);
        Debug.DrawLine(GetWorldPosition(width, -height), GetWorldPosition(width, height), Color.cyan, 100f);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y)* cellSize;
    }
    public Vector2Int GetCellIndice(Vector2 worldPos)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPos.x/cellSize), Mathf.FloorToInt(worldPos.y/cellSize)); 
    }

    public void SetValue(int x, int y, int value)
    {
        if (x < 0 || y < 0) return;
        if (x > width || y > height) return;

        cellValues[x,y] = value;
    }

    public void SetValue(Vector2 worldPos, int value)
    {
        Vector2Int cellIndex = GetCellIndice(worldPos);
        SetValue(cellIndex.x, cellIndex.y, value);
    }

    public int GetValue(Vector2 worldPos)
    {
        Vector2Int cellIndex = GetCellIndice(worldPos);
        return GetValue(cellIndex.x, cellIndex.y);
    }
    
    public int GetValue(int x, int y)
    {
        if (x < 0 || y < 0) return 0;
        if (x > width || y > height) return 0;

        return cellValues[x, y];
    }
}
