using System;
using UnityEngine;

public sealed class StartPosition : MonoBehaviour
{
    private int _currentPoint = 0;
    [SerializeField] private Vector3[] _spawnPosions;
    public Vector3 SpawnPoint { get => GetLastSavePosition(); }


    private Vector3 GetLastSavePosition()
    {
        return _spawnPosions[_currentPoint];
    }

    public void ChangeSpawnPoint(int point)
    {
        _currentPoint = point;
    }
}
