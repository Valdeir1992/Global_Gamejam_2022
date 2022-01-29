using UnityEngine;

[CreateAssetMenu(menuName = "Prototipo/Data/Gameplay")]
public sealed class GameplayData : ScriptableObject
{
    [SerializeField] private float _transitionTime;
    [SerializeField] private float _powerTime;
    public float TranstionTime
    {
        get => _transitionTime;
    }
    public float PowerTime { get => _powerTime; }
}