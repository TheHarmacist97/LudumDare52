using CodeMonkey.Utils;
using UnityEngine;

public class BuildingHelper : MonoBehaviour
{
    enum States
    {
        INACTIVE,
        SAMPLING
    }
    [SerializeField] Building buildingPrefab;
    private States state = States.INACTIVE;

    private Building currentBuilding;
    private CellObject currentCellObject;
    GridSystem grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = GridCreator.instance.grid;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentBuilding = Instantiate(buildingPrefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            state = States.SAMPLING;
        }
        StateLogic();
    }

    private void StateLogic()
    {
        if (state == States.INACTIVE) return;
        Vector2 currentMousePos = UtilsClass.GetMouseWorldPosition();
        if (state == States.SAMPLING)
        {
            currentBuilding.transform.position = currentMousePos;
            currentCellObject = grid.GetValue(currentMousePos);

            if (currentCellObject.occupied)
            {
                currentBuilding.ChangeConstructionState(ConstructionState.COLLIDING);
                return;
            }
            else
            {
                currentBuilding.ChangeConstructionState(ConstructionState.BUILDABLE);
            }


            if (Input.GetMouseButtonDown(0) && !currentCellObject.occupied)
            {
                AstarPath.active.Scan();
                currentBuilding.transform.position = currentCellObject.transform.position;
                currentBuilding.ChangeConstructionState(ConstructionState.BUILT);
                currentCellObject.occupied = true;
                state = States.INACTIVE;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(currentBuilding.gameObject);
                state = States.INACTIVE;
            }
        }

    }
}
