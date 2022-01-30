using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorDead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        MessageSystem.Instance.Notify(new GameplayerMessage(GameplayAction.RESPAWN));
    }
}

public enum GameplayAction
{
    RESPAWN
}
