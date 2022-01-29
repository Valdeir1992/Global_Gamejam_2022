using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudPersonalitySystem : MonoBehaviour
{
    private Vector2 _deafStartPoisiton;
    private Vector2 _listenerStartPosition; 
    private IEnumerator _coroutine;
    [SerializeField] private float _totalTime;
    [SerializeField] private Image _deafImage;
    [SerializeField] private Image _listenerImage;

    private void Awake()
    {
        _deafStartPoisiton = _deafImage.rectTransform.anchoredPosition;
        _listenerStartPosition = _listenerImage.rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        MessageSystem.Instance.Register(typeof(PersonalityMessage), this.MessageHandler);
    }

    private void OnDisable()
    {
        MessageSystem.Instance.UnRegister(typeof(PersonalityMessage), this.MessageHandler);
    }




    private bool MessageHandler(Message message)
    {
        PersonalityMessage pM = message as PersonalityMessage;

        if (!Object.ReferenceEquals(pM, null) && Object.ReferenceEquals(_coroutine,null))
        {
             if (pM.Personality == Personality.CHANGE)
            { 
                _coroutine = Coroutine_Change_Personality();
                StartCoroutine(_coroutine);
            }
        }
        return false;
    }

    private IEnumerator Coroutine_Change_Personality()
    {
        Vector2 deaftStartPosition = _deafImage.rectTransform.anchoredPosition;
        Vector2 listenerStartPosition = _listenerImage.rectTransform.anchoredPosition;
        Vector2 deafFinalPosition = Vector2.zero;
        Vector2 listenerFinalPosition = Vector2.zero;
        if(GameplaySystem.Instance.CurrentPersonality != Personality.DEAF)
        {
            deafFinalPosition = _deafStartPoisiton;
            listenerFinalPosition = _listenerStartPosition;
        }
        else
        {
            deafFinalPosition = _listenerStartPosition;
            listenerFinalPosition = _deafStartPoisiton;
        }
        float timeElaped = 0;
        while (true)
        {
            timeElaped += Time.deltaTime;
            _listenerImage.rectTransform.anchoredPosition = Vector2.Lerp(listenerStartPosition, listenerFinalPosition, timeElaped / _totalTime);
            _deafImage.rectTransform.anchoredPosition = Vector2.Lerp(deaftStartPosition, deafFinalPosition, timeElaped / GameplaySystem.Instance.Data.TranstionTime);
            if(timeElaped/ GameplaySystem.Instance.Data.TranstionTime >= 1)
            {
                if(GameplaySystem.Instance.CurrentPersonality != Personality.DEAF)
                {
                    _deafImage.transform.SetAsFirstSibling();
                }
                else
                {
                    _listenerImage.transform.SetAsFirstSibling();
                }
                break;
            }
            yield return null;
        }
        _coroutine = null;
        yield break;
    }
}

public sealed class PersonalityMessage : Message
{
    public readonly Personality Personality;

    public PersonalityMessage(Personality personality)
    {
        Personality = personality;
    }

    public PersonalityMessage()
    {
        Personality = Personality.CHANGE;
    }
}
