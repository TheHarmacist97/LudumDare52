using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;
    
    private TextMeshProUGUI tooltipText;
    
    private RectTransform rectTransform;
    private RectTransform backgroundRectTransform;
    private RectTransform parentTransform;
    private RectTransform canvasRectTransform;
    
    private Func<string> getTooltipStringFunc;

    private void Awake()
    {
        instance = this;

        rectTransform = transform.GetComponent<RectTransform>();
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<TextMeshProUGUI>();
        parentTransform = transform.parent.GetComponent<RectTransform>();
        
        HideTooltip();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentTransform, Input.mousePosition, null,
            out localPoint);

        transform.localPosition = localPoint;
        
        // update text
        if(getTooltipStringFunc != null)
            SetText(getTooltipStringFunc());
        
        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void ShowTooltip(string text)
    {
        ShowTooltip(() => text);
    }

    private void ShowTooltip(Func<string> getTooltipStringFunc)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        this.getTooltipStringFunc = getTooltipStringFunc;
        SetText(getTooltipStringFunc());
    }

    private void SetText(string text)
    {
        tooltipText.text = tooltipText.text = text;
        float textPadding = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPadding * 2f, tooltipText.preferredHeight + textPadding * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(Func<string> getTooltipStringFunc)
    {
        instance.ShowTooltip(getTooltipStringFunc);
    }
    
    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
