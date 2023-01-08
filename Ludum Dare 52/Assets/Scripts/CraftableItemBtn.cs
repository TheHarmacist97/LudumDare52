using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering;

[RequireComponent(typeof(Button), typeof(EventTrigger))]
public class CraftableItemBtn : MonoBehaviour
{
    private EventTrigger _eventTrigger;
    private CraftableItemSO _craftableItem;
    private Button _button;
    private Image _buttonImage;
    private TextMeshProUGUI _buttonText;
    private string _tooltipText;

    public void Init(CraftableItemSO craftableItem)
    {
        _button = GetComponent<Button>();
        _buttonImage = _button.GetComponent<Image>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        
        _craftableItem = craftableItem;
        _tooltipText = "Required resources: \n";
        foreach (var requiredResource in craftableItem.requiredResources)
        {
            _tooltipText += "<color=\"yellow\">" + requiredResource.requiredAmount + "</color> " + requiredResource.resource.ItemName + "\n";
        }

        _buttonText.text = craftableItem.itemName;
        _buttonImage.sprite = craftableItem.itemSprite;
        _button.onClick.AddListener(CraftItem);
    }
    private void Start()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry pointerEnterEvent = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        pointerEnterEvent.callback.AddListener((eventData) => { OnPointerEnter(); });
        EventTrigger.Entry pointerExitEvent = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        pointerExitEvent.callback.AddListener((eventData) => { OnPointerExit(); });
        // add the events
        _eventTrigger.triggers.Add(pointerEnterEvent);
        _eventTrigger.triggers.Add(pointerExitEvent);
    }

    private void CraftItem()
    {
        BuildingHelper.instance.InitiateBuilding(_craftableItem);
    }
    
    private string GetTooltipString()
    {
        return _tooltipText;
    }

    private void OnPointerEnter()
    {
        Tooltip.ShowTooltip_Static(GetTooltipString);
    }
    
    private void OnPointerExit()
    {
        Debug.Log("pointer exit");
        Tooltip.HideTooltip_Static();
    }
}
