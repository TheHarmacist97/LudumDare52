using CodeMonkey.Utils;
using UnityEngine;

public class BuildingHelper : MonoBehaviour
{
    enum States
    {
        INACTIVE,
        SAMPLING
    }
    [SerializeField] GameObject buildingPrefab;
    private States state = States.INACTIVE;

    private GameObject currentBuildingPrefab;
    private Building currentBuildingComponent;
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
            currentBuildingPrefab = Instantiate(buildingPrefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
            currentBuildingComponent = currentBuildingPrefab.GetComponent<Building>();
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
            currentBuildingPrefab.transform.position = currentMousePos;
            currentCellObject = grid.GetValue(currentMousePos);

            if (currentCellObject.occupied)
            {
                currentBuildingComponent.ChangeConstructionState(ConstructionState.COLLIDING);
                return;
            }
            else
            {
                currentBuildingComponent.ChangeConstructionState(ConstructionState.BUILDABLE);
            }


            if (Input.GetMouseButtonDown(0) && !currentCellObject.occupied)
            {
                AstarPath.active.Scan();
                currentBuildingPrefab.transform.position = currentCellObject.transform.position;
                currentBuildingComponent.ChangeConstructionState(ConstructionState.BUILT);
                currentCellObject.occupied = true;
                state = States.INACTIVE;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(currentBuildingPrefab);
                state = States.INACTIVE;
            }
        }

    }
}
