using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button exitButton;

    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            OnStartClick();
        });
        exitButton.onClick.AddListener(() =>
        {
            OnExitClick();
        });
    }

    private void OnStartClick()
    {
        SceneManager.LoadSceneAsync(1);
    }

    private void OnExitClick()
    {
        Application.Quit();
    }
}
