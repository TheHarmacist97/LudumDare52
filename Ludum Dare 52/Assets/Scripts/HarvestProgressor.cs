using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestProgressor : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }


    public void ShowProgressor(float fillAmount)
    {
        fillImage.fillAmount = fillAmount;
        _canvasGroup.alpha = 1f;
    }

    public void HideProgressor()
    {
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }
    
}
