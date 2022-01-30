using UnityEngine;

public sealed class DefaultMotion : MonoBehaviour, IMotionController
{ 
    public bool _grounded;
    private bool _walk;
    private bool _jump;
    private LayerMask _layer;
    private PlayerMediator _player;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private Vector3 _direction;

    public bool Grounded { get => _grounded; }

    public bool Walk { get => _walk; }

    private void Awake()
    {
        _layer = LayerMask.GetMask("Player");
    }

    private void OnEnable()
    {
        MessageSystem.Instance.Register(typeof(InputMessage), this.MessageHandler); 
    }
    private void Update()
    { 
        _controller.Move(_playerVelocity * Time.deltaTime);
        _grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, ~_layer);
        if (_grounded)
        {
            _playerVelocity.y = 0;
        }
        else
        { 
            _playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        Move(_direction); 
    }

    private void OnDisable()
    {
        MessageSystem.Instance.UnRegister(typeof(InputMessage), this.MessageHandler); 
    } 
    public void Configure(PlayerMediator player)
    {
        _player = player;
        _controller = GetComponent<CharacterController>(); 
    }
    private void Move(Vector3 direction)
    { 
        _controller.Move(direction * Time.deltaTime * _player.Data.Speed);  
        if (_grounded)
        {
            _player.Walk(); 
        } 
        if(direction.sqrMagnitude > 0)
        { 
            _controller.transform.forward = direction;
            _walk = true;
        }
        else
        {
            _walk = false;
        }
    } 

    private void Jump()
    {
        if (_grounded && !_jump)
        {  
            _playerVelocity.y += Mathf.Sqrt(_player.Data.Height * -3.0f * Physics.gravity.y);
            _jump = true;
            _player.Jump();
            Invoke("TriggerJump", 0.1f);
        } 
    }

    private void TriggerJump()
    {
        _jump = false;
    }

    private bool MessageHandler(Message message)
    {
        InputMessage iM = message as InputMessage;
        if (!Object.ReferenceEquals(iM, null))
        {
            switch (iM.Input)
            {
                case "Left_Press":
                    _direction += Vector3.left;
                    break;
                case "Left_Free":
                    _direction -= Vector3.left;
                    break;
                case "Right_Press":
                    _direction += Vector3.right;
                    break;
                case "Right_Free":
                    _direction -= Vector3.right;
                    break;
                case "Jump":
                    Jump();
                    break;
            }
        }
        return false;
    }
}