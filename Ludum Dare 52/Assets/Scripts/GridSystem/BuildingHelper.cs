using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using JetBrains.Annotations;
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
    public List<Transform> turretPositions;
    private Building currentBuilding;
    private CraftableItemSO _currentCraftableItem;
    private CellObject currentCellObject;
    
    private GridSystem grid;
    private Vector2 currentMousePos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        turretPositions= new List<Transform>();
        grid = GridCreator.grid;
    }

    
    void Update()
    {
        currentMousePos = UtilsClass.GetMouseWorldPosition();
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
        
        if (state == States.SAMPLING)
        {
            currentCellObject = grid.GetValue(currentMousePos);
            if (currentCellObject == null) return;
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
                AstarPath.active.Scan();
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
                turretPositions.Add(currentBuilding.transform);
                currentBuilding.ChangeConstructionState(ConstructionState.BUILT);
                currentCellObject.occupied = true;
                state = States.INACTIVE;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _currentCraftableItem = null;
                Destroy(currentBuilding.gameObject);
                currentBuilding = null;
                state = States.INACTIVE;
            }
        }
    }
    public Transform GetRandomTurret()
    {
        if (turretPositions.Count == 0)
            return Ship.instance.transform;
        
        return turretPositions[UnityEngine.Random.Range(0, turretPositions.Count)];
    }
}
