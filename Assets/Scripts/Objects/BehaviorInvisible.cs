using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorInvisible : MonoBehaviour
{
    private MeshRenderer _meshRender;
    private bool _isInvisible = true;
    private void OnEnable()
    {
        _meshRender = GetComponent<MeshRenderer>();
        _meshRender.enabled = false;
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
                _isInvisible = !_isInvisible;
                if (_isInvisible)
                {
                    _meshRender.enabled = false;
                }
                else
                {
                    _meshRender.enabled = true;
                }
            }
        }
        return false;
    }
}
