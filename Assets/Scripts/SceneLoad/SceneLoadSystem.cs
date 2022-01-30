using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadSystem : MonoBehaviour
{
    private static SceneLoadSystem _instance;
    [SerializeField] private Image _fade;
    [SerializeField] private float _fadeTime;

    private void Awake()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            StartCoroutine(Coroutine_FadeIn());
        };
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        MessageSystem.Instance.Register(typeof(SceneLoadMessage), this.MessageHandler);
    }

    private void OnDisable()
    {
        MessageSystem.Instance.UnRegister(typeof(SceneLoadMessage), this.MessageHandler); 
    }

    private bool MessageHandler(Message message)
    {
        SceneLoadMessage sM = message as SceneLoadMessage;
        if (!Object.ReferenceEquals(sM, null))
        {
            StartCoroutine(Coroutine_LoadScene(sM.LevelName));
        }
        return false;
    }

    private IEnumerator Coroutine_LoadScene(string level)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(level);
        load.allowSceneActivation = false;

        yield return Coroutine_FadeOut();

        while (!load.isDone)
        {
            if(load.progress > 0.85)
            {
                load.allowSceneActivation = true;
            }
            yield return null;
        } 
        yield break;
    }

    private IEnumerator Coroutine_FadeOut()
    {
        float elapsedTime = 0;
        Color startColor = _fade.color;
        Color finalColor = new Color(0, 0, 0, 1);
        while (true)
        {
            elapsedTime += Time.deltaTime;
            _fade.color = Color.Lerp(startColor, finalColor, (elapsedTime / (_fadeTime / 2)));
            if (elapsedTime / (_fadeTime / 2) >= 1)
            { 
                break;
            }
            yield return null;
        }
    }

    private IEnumerator Coroutine_FadeIn()
    {
        float elapsedTime = 0;
        Color startColor = _fade.color;
        Color finalColor = new Color(0, 0, 0, 0);
        while (true)
        {
            elapsedTime += Time.deltaTime;
            _fade.color = Color.Lerp(startColor, finalColor, (elapsedTime / (_fadeTime / 2)));
            if (elapsedTime / (_fadeTime / 2) >= 1)
            { 
                break;
            }
            yield return null;
        }
    }
}
