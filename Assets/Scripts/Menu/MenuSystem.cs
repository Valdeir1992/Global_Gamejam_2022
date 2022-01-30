using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private Button _btnCredits;
    [SerializeField] private Button _btnQuit;
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnReturnToMenu;
    [SerializeField] private Button _btnCloseAlert;
    [SerializeField] private Button _btnQuitAlert;
    [SerializeField] private CanvasGroup[] _canvas;

    private void Awake()
    {
        _btnPlay.onClick.AddListener(Play);
        _btnCredits.onClick.AddListener(ShowCredits);
        _btnQuit.onClick.AddListener(ShowAlert);
        _btnReturnToMenu.onClick.AddListener(ShowMenu);
        _btnCloseAlert.onClick.AddListener(ShowMenu);
        _btnQuitAlert.onClick.AddListener(Application.Quit); 
    }

    private void Start()
    {
        OpenCanvas(0);
    }

    private void Play()
    {
        MessageSystem.Instance.Notify(new SceneLoadMessage("Level_01"));
    }

    private void ShowCredits()
    {
        OpenCanvas(1);
    }

    private void ShowAlert()
    {
        OpenCanvas(2);
    }

    private void ShowMenu()
    {
        OpenCanvas(0);
    }

    private void OpenCanvas(int value)
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            if(value == i)
            {
                CanvasGroup canva = _canvas[i];
                canva.alpha = 1;
                canva.blocksRaycasts = true;
                canva.interactable = true;
            }
            else
            {
                CanvasGroup canva = _canvas[i];
                canva.alpha = 0;
                canva.blocksRaycasts = false;
                canva.interactable = false;
            }
        }
    }
}

public sealed class SceneLoadMessage : Message
{
    public readonly string LevelName; 
    public SceneLoadMessage(string levelName)
    {
        LevelName = levelName;
    }
} 
