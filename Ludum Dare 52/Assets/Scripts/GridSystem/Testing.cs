using CodeMonkey.Utils;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] private int width, height;
    [SerializeField] private float cellSize;
    GridSystem grid;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(-(width / 2) * cellSize, -(height * cellSize / 2));
        grid = new GridSystem(width, height, cellSize, transform.position, InstantiateCell);
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
        GameObject cell = Instantiate(cellPrefab, pos + new Vector2(cellSize, cellSize)/2f, Quaternion.identity); ;
        return cell.GetComponent<CellObject>();
    }
}
