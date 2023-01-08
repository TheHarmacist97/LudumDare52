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
    private CraftableItemSO _currentCraftableItem;
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

    public void InitiateBuilding(CraftableItemSO craftableItem)
    {
        _currentCraftableItem = craftableItem;
        currentBuilding = Instantiate(craftableItem.itemPrefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
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
                // Deduct resources from inventory
                bool hasEnoughResources = true;
                foreach (var requiredResource in _currentCraftableItem.requiredResources)
                {
                    if (!InventoryManager.instance.InventoryContains(requiredResource.resource,
                            requiredResource.requiredAmount))
                    {
                        TooltipWarning.ShowTooltip_Static(() => "Not Enough Resources!");
                        hasEnoughResources = false;
                        return;
                    }
                }

                foreach (var requiredResource in _currentCraftableItem.requiredResources)
                {
                    InventoryManager.instance.RemoveFromInventory(requiredResource.resource, requiredResource.requiredAmount);
                }
                currentBuilding.transform.position = currentCellObject.transform.position;
                currentBuilding.ChangeConstructionState(ConstructionState.BUILT);
                currentCellObject.occupied = true;
                state = States.INACTIVE;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                _currentCraftableItem = null;
                Destroy(currentBuilding.gameObject);
                currentBuilding = null;
                state = States.INACTIVE;
            }
        }

    }
}
