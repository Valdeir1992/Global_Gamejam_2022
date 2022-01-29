using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMediator : MonoBehaviour
{
    [SerializeField] private PlayerData _data;

    public PlayerData Data { get => _data; } 
    public abstract void Jump(); 
    public abstract void Walk();
}

public interface IMotionController: IController
{
    
}

public interface IController
{
    void Configure(PlayerMediator player);
}
