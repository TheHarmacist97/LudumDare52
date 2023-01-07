using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipWarning : MonoBehaviour
{
    private static TooltipWarning instance;
    
    private TextMeshProUGUI tooltipText;
    
    private RectTransform rectTransform;
    private RectTransform backgroundRectTransform;
    private Image backgroundImage;
    private RectTransform parentTransform;
    private RectTransform canvasRectTransform;
    
    private Func<string> getTooltipStringFunc;
    private float showTimer;
    private float flashTimer;
    private int flashState;

    private void Awake()
    {
        instance = this;

        rectTransform = transform.GetComponent<RectTransform>();
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        backgroundImage = backgroundRectTransform.GetComponent<Image>();
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

        flashTimer += Time.deltaTime;
        float flashTimerMax = 0.033f;
        if (flashTimer > flashTimerMax)
        {
            flashState++;
            switch (flashState)
            {
                case 1:
                case 3:
                case 5:
                    tooltipText.color = Color.white;
                    backgroundImage.color = Color.red;
                    break;
                case 2:
                case 4:
                    tooltipText.color = Color.red;
                    backgroundImage.color = Color.white;
                    break;
            }
        }
        showTimer -= Time.deltaTime;
        if (showTimer <= 0f)
        {
            HideTooltip();
        }
    }

    private void ShowTooltip(string text, float showTime = 2f)
    {
        ShowTooltip(() => text, showTime);
    }

    private void ShowTooltip(Func<string> getTooltipStringFunc, float showTime = 2f)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        this.getTooltipStringFunc = getTooltipStringFunc;
        showTimer = showTime;
        flashTimer = 0f;
        flashState = 0;
        Update();
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
