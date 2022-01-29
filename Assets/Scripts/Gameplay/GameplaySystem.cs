using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : Singleton<GameplaySystem>
{
    private Personality _currentPersonality = Personality.LISTENER;
    private bool _transitionPersonality;
    private PlayerMediator _currentPlayer;
    [SerializeField] private GameplayData _data;
    [SerializeField] private PlayerMediator _deafPrefab;
    [SerializeField] private PlayerMediator _listenerPrefab;

    public GameplayData Data { get => _data; }
    public Personality CurrentPersonality { get => _currentPersonality; }

    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _currentPlayer = Instantiate(_listenerPrefab,
            FindObjectOfType<StartPosition>().SpawnPoint,
            Quaternion.identity);

        SetCinemachineCamera();
    }

    private void SetCinemachineCamera()
    {
        Cinemachine.CinemachineVirtualCamera cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        cam.Follow = _currentPlayer.transform;
    }

    private void OnEnable()
    {
        MessageSystem.Instance.Register(typeof(GameplayerMessage), this.MessageHandler);
    }
    private void OnDisable()
    {
        MessageSystem.Instance.UnRegister(typeof(GameplayerMessage), this.MessageHandler); 
    }
    private bool MessageHandler(Message message)
    {
        GameplayerMessage gM = message as GameplayerMessage;

        if (!Object.ReferenceEquals(gM, null))
        {
            if(gM.Personality == Personality.CHANGE  && !_transitionPersonality)
            {
                Change();
                Invoke("Change", _data.PowerTime);
            }
        }
        return false;
    }

    private void Change()
    {
        if (_currentPersonality == Personality.DEAF)
        {
            _currentPersonality = Personality.LISTENER;
        }
        else
        {
            _currentPersonality = Personality.DEAF;
        }
        MessageSystem.Instance.Notify(new PersonalityMessage(Personality.CHANGE));
        ChangeCharacter();
        _transitionPersonality = true;
        Invoke("TriggerTransition", _data.TranstionTime + _data.PowerTime);
    }

    private void ChangeCharacter()
    {
        Vector3 playerWorldPosition = _currentPlayer.transform.position;
        Destroy(_currentPlayer.gameObject);
        if(_currentPersonality == Personality.DEAF)
        {
            _currentPlayer = Instantiate(_deafPrefab, playerWorldPosition, Quaternion.identity);
        }
        else
        {
            _currentPlayer = Instantiate(_listenerPrefab, playerWorldPosition, Quaternion.identity); 
        }
        SetCinemachineCamera();
    }

    private void TriggerTransition()
    {
        _transitionPersonality = false;
    }
}

public enum Personality
{
    DEAF,
    LISTENER,
    CHANGE
}
