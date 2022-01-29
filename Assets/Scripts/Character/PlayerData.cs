using UnityEngine;

[CreateAssetMenu(menuName ="Prototipo/Data/Player")]
public sealed class PlayerData : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    public float Speed { get => _speed; }
    public float Height { get => _jumpHeight; }
}
