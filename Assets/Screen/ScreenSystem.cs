using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSystem : MonoBehaviour
{
    private float _previuosTimeScale;
    [SerializeField] private CanvasGroup _pauseScreen;
    [SerializeField] private Button _btnPause;
    [SerializeField] private Button _btnReturnToGame;


    private void Awake()
    {
        _btnPause.onClick.AddListener(() =>
        {
            Show(_pauseScreen);
            _previuosTimeScale = Time.timeScale;
            Time.timeScale = 0;
        });

        _btnReturnToGame.onClick.AddListener(ReturnToGame);
    }

    private void Start()
    {
        Hidden(_pauseScreen);
    }

    private void ReturnToGame()
    {
        Hidden(_pauseScreen);
        Time.timeScale = _previuosTimeScale;
    }

    private void Show(CanvasGroup canvas)
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    private void Hidden(CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

}
