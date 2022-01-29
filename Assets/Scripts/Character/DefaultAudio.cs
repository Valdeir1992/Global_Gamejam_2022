using UnityEngine;

public sealed class DefaultAudio : MonoBehaviour, IAudioController
{
    private AudioSource _source;
    private PlayerMediator _player;
    [SerializeField] private float _stepTime;
    private float _elapsedTime;
    [SerializeField] private AudioClip _steps;
    [SerializeField] private AudioClip _jump;

    private void Update()
    {
        if(_elapsedTime <= _stepTime)
        { 
            _elapsedTime += Time.deltaTime;
        } 
    }
    public void Configure(PlayerMediator player)
    {
        _player = player;
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = _steps;
    }

    public void Jump()
    {
        if(GameplaySystem.Instance.CurrentPersonality == Personality.DEAF)
        {
            Handheld.Vibrate(); 
        }
        else
        {
            if (_source.clip != _jump)
            {
                _source.clip = _jump;
            }
            _source.Play();
        }
    }

    public void Steps()
    {
        if (_elapsedTime <= _stepTime) return;
        _elapsedTime = 0;

        if(GameplaySystem.Instance.CurrentPersonality == Personality.DEAF)
        {
            Handheld.Vibrate();
        }
        else
        {
            if(_source.clip != _steps)
            {
                _source.clip = _steps;
            }
            _source.Play();
        }
    }
}
