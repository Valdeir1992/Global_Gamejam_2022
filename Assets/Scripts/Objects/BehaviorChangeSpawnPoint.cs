using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorChangeSpawnPoint : MonoBehaviour
{
    private StartPosition _spwanPoint;
    [SerializeField] private int _point;

    private void Awake()
    {
        _spwanPoint = FindObjectOfType<StartPosition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _spwanPoint.ChangeSpawnPoint(_point);
    }
}
