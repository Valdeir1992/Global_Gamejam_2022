using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadSystem : MonoBehaviour
{
    private static GamepadSystem _instance;
    private PlayerInputs _inputs; 

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _inputs = new PlayerInputs();
        _inputs.Player.Left.performed += ctx =>
        {
            MessageSystem.Instance.Notify(new InputMessage("Left_Press"));
        };
        _inputs.Player.Left.canceled += ctx =>
        {
            MessageSystem.Instance.Notify(new InputMessage("Left_Free"));
        };
        _inputs.Player.Right.performed += ctx =>
        {
            MessageSystem.Instance.Notify(new InputMessage("Right_Press"));
        };
        _inputs.Player.Right.canceled += ctx =>
        {
            MessageSystem.Instance.Notify(new InputMessage("Right_Free"));
        };
        _inputs.Player.Jump.performed += ctx =>
        {
            MessageSystem.Instance.Notify(new InputMessage("Jump"));
        };
    }

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            _inputs.Enable();
        }
    }

    private void OnDisable()
    {
        if (gameObject.activeInHierarchy)
        {
            _inputs.Disable();
        }
    }


}
