using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendSystem : MonoBehaviour
{
    private IEnumerator _coroutine;
    [SerializeField] private TMPro.TextMeshProUGUI _legend;

    private void OnEnable()
    {
        MessageSystem.Instance.Register(typeof(PersonalityMessage), this.MessageHandler);
    }

    private void OnDisable()
    {
        MessageSystem.Instance.UnRegister(typeof(PersonalityMessage), this.MessageHandler);
    }

    private void Start()
    {
        _coroutine = Coroutine_Change_Personality();
        StartCoroutine(_coroutine);
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
        }else if(!Object.ReferenceEquals(pM, null) && !Object.ReferenceEquals(_coroutine, null))
        {
            StopCoroutine(_coroutine);
            _coroutine = Coroutine_Change_Personality();
            StartCoroutine(_coroutine);
        }
        return false;
    }

    private IEnumerator Coroutine_Change_Personality()
    {
        if(GameplaySystem.Instance.CurrentPersonality == Personality.DEAF)
        {
            _legend.text = "Music: off";
        }
        else
        {
            _legend.text = "Music: on"; 
        }
        yield return new WaitForSeconds(2);
        _legend.text = "";
        _coroutine = null;
        yield break;
    }
}
