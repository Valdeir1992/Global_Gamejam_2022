using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class BtnChangePersonality : MonoBehaviour
{
    private Button _btnChangePersonality;

    private void Awake()
    {
        _btnChangePersonality = GetComponent<Button>();
        _btnChangePersonality.onClick.AddListener(() =>
        {
            MessageSystem.Instance.Notify(new GameplayerMessage(Personality.CHANGE));
        });
    }
}

public sealed class GameplayerMessage : Message
{
    public readonly Personality Personality;

    public GameplayerMessage(Personality personality)
    {
        Personality = personality;
    }
}
