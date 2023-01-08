using System;
using CodeMonkey.Utils;
using UnityEngine;

public class BuildingHelper : MonoBehaviour
{
    public static BuildingHelper instance;
    enum States
    {
        INACTIVE,
        SAMPLING
    }
    private States state = States.INACTIVE;

    private Building currentBuilding;
    private CellObject currentCellObject;
    GridSystem grid;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        grid = GridCreator.instance.grid;
    }

    
    void Update()
    {
        StateLogic();
    }

    public void InitiateBuilding(Building building)
    {
        currentBuilding = Instantiate(building, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
        state = States.SAMPLING;
    }

    private void StateLogic()
    {
        if (state == States.INACTIVE) return;
        Vector2 currentMousePos = UtilsClass.GetMouseWorldPosition();
        if (state == States.SAMPLING)
        {
            currentCellObject = grid.GetValue(currentMousePos);
            currentBuilding.transform.position = currentCellObject.transform.position;

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
                //AstarPath.active.Scan();
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
