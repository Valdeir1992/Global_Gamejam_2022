using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private IEnumerator _coroutine;
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
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

        if (!Object.ReferenceEquals(pM, null) && Object.ReferenceEquals(_coroutine, null))
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
        float timeElapsed = 0;
        float startVolume = _source.volume;
        float finalVolume = 0;
        if(GameplaySystem.Instance.CurrentPersonality == Personality.DEAF)
        {
            finalVolume = 0;
        }
        else
        {
            finalVolume = 1;
        }
        while (true)
        {
            timeElapsed += Time.deltaTime;
            _source.volume = Mathf.Lerp(startVolume, finalVolume, timeElapsed / GameplaySystem.Instance.Data.TranstionTime);
            if(timeElapsed/GameplaySystem.Instance.Data.TranstionTime >= 1)
            {
                break;
            }
            yield return null;
        }
        _coroutine = null;
        yield break;
    }
}
