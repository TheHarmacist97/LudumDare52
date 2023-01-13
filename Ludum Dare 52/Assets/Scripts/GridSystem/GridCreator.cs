using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public static GridCreator instance;
    [Header("Prefabs")]
    [SerializeField] CellObject cellPrefab;
    [SerializeField] private int width, height;
    [SerializeField] private float cellSize;
    [SerializeField] private int buildingSize;
    public static GridSystem grid;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;   
        }
        else if(instance!= this&&instance != null)
        {
            Destroy(gameObject);
        }

        CellObject.cellSize = cellSize;
        transform.position = new Vector2(-(width / 2) * cellSize, -(height * cellSize / 2));
        grid = new GridSystem(width, height, CellObject.cellSize, transform.position, InstantiateCell, true, buildingSize);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition());
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
    public CellObject InstantiateCell(Vector2 pos)
    {
        Vector2 offset = new Vector2(cellSize, cellSize) / 2f;
        CellObject cell = Instantiate(cellPrefab, pos + offset, Quaternion.identity);
        cell.transform.parent = transform;
        return cell;
    }
}
