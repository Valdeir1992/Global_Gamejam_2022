using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PosProcessingSystem : MonoBehaviour
{
    [SerializeField] private PostProcessProfile _file; 

    private void OnEnable()
    {
        _file.GetSetting<ColorGrading>().saturation.value = -50;
        MessageSystem.Instance.Register(typeof(PersonalityMessage), this.MessageHandler);

    }

    private void OnDisable()
    {
        MessageSystem.Instance.UnRegister(typeof(PersonalityMessage), this.MessageHandler);
    }
    private bool MessageHandler(Message message)
    {
        PersonalityMessage pM = message as PersonalityMessage;

        if (!Object.ReferenceEquals(pM, null))
        {
            if (pM.Personality == Personality.CHANGE)
            {
                StartCoroutine(Coroutine_Change_Color());
            }
        }
        return false;
    }

    private IEnumerator Coroutine_Change_Color()
    {
        ColorGrading color = _file.GetSetting<ColorGrading>();
        float timeElapsed = 0;
        float startValue = _file.GetSetting<ColorGrading>().saturation.value;
        float finalValue = 0;
        if(GameplaySystem.Instance.CurrentPersonality == Personality.DEAF)
        {
            finalValue = 0;
        }
        else
        {
            finalValue = -50;
        }
        while (true)
        {
            timeElapsed += Time.deltaTime;
            color.saturation.value = Mathf.Lerp(startValue, finalValue, timeElapsed/GameplaySystem.Instance.Data.TranstionTime);
            if(timeElapsed/GameplaySystem.Instance.Data.TranstionTime >= 1)
            {
                break;
            }
            yield return null;
        }
        yield break;
    }
}
