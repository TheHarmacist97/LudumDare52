using CodeMonkey.Utils;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public static GridCreator instance;
    [SerializeField] GameObject cellPrefab;
    [SerializeField] private int width, height;
    [SerializeField] private float cellSize;
    public GridSystem grid;

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

        transform.position = new Vector2(-(width / 2) * cellSize, -(height * cellSize / 2));
        grid = new GridSystem(width, height, cellSize, transform.position, InstantiateCell, false);
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
        GameObject cell = Instantiate(cellPrefab, pos + new Vector2(cellSize, cellSize)/2f, Quaternion.identity);
        cell.transform.parent = transform;
        return cell.GetComponent<CellObject>();
    }
}
