using UnityEngine;

public sealed class StartPosition : MonoBehaviour
{
    [SerializeField] private Vector3[] _spawnPosions;
    public Vector3 SpawnPoint { get => GetLastSavePosition(); }


    private Vector3 GetLastSavePosition()
    {
        return _spawnPosions[0];
    }
}
