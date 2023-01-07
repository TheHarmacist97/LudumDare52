using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public enum ScenesEnum
    {
        Space,
        Planet
    }
    public static SceneSwitcher instance;
    
    [SerializeField] private Button backToSpaceBtn;
    [SerializeField] private CanvasGroup faderImage;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private GameObject[] spaceSceneDependencies;
    [SerializeField] private GameObject[] planetSceneDependencies;

    public ScenesEnum ActiveScene { get; private set; } = ScenesEnum.Space;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        faderImage.alpha = 0f;
        // default scene is space scene
        EnableGameObjects(spaceSceneDependencies);
        DisableGameObjects(planetSceneDependencies);
        backToSpaceBtn.onClick.AddListener((() =>
        {
            SwitchScene(ScenesEnum.Space);
        }));
    }

    public void SwitchScene(ScenesEnum scene)
    {
        ActiveScene = scene;
        StartCoroutine(SwitchToActiveScene());
    }

    IEnumerator SwitchToActiveScene()
    {
        faderImage.transform.SetAsLastSibling();
        faderImage.DOFade(1f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration + 0.3f);
        switch (ActiveScene)
        {
            case ScenesEnum.Planet:
                EnableGameObjects(planetSceneDependencies);
                DisableGameObjects(spaceSceneDependencies);
                break;
            case ScenesEnum.Space:
                EnableGameObjects(spaceSceneDependencies);
                DisableGameObjects(planetSceneDependencies);
                break;
        }
        faderImage.DOFade(0f, fadeDuration);
    }

    void DisableGameObjects(GameObject[] gameObjects)
    {
        foreach (var gameObj in gameObjects)
        {
            gameObj.SetActive(false);
        }
    }
    void EnableGameObjects(GameObject[] gameObjects)
    {
        foreach (var gameObj in gameObjects)
        {
            gameObj.SetActive(true);
        }
    }
}
