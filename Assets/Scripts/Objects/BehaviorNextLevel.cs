using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorNextLevel : MonoBehaviour
{
    [SerializeField] private string _nextLevelName;

    private void OnTriggerEnter(Collider other)
    {  
        MessageSystem.Instance.Notify(new SceneLoadMessage(_nextLevelName));
    }
}
