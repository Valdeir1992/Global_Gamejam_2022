using UnityEngine;

public sealed class DefaultMotion : MonoBehaviour, IMotionController
{
    public bool _grounded;
    private bool _jump;
    private LayerMask _layer;
    private PlayerMediator _player;
    private CharacterController _controller;
    private Vector3 _playerVelocity;

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
        _grounded = Physics.Raycast(transform.position, Vector3.down, 1.25f, ~_layer);
        if (_grounded)
        {
            _playerVelocity.y = 0;
        }
        else
        { 
            _playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        } 

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector3.down *1.25f);
    }

    private void OnDisable()
    {
        MessageSystem.Instance.UnRegister(typeof(InputMessage), this.MessageHandler);
    }
    public void Configure(PlayerMediator player)
    {
        _player = player;
        _controller = gameObject.AddComponent<CharacterController>();
    }

    private void MoveLeft()
    {
        _controller.Move(Vector3.left * Time.deltaTime * _player.Data.Speed);
        if (_grounded)
        { 
            _player.Walk();
        } 
    }

    private void MoveRight()
    {
        _controller.Move(Vector3.right * Time.deltaTime * _player.Data.Speed);
        _player.Walk();
        if (_grounded)
        {
            _player.Walk();
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
                case "Left":
                    MoveLeft();
                    break;
                case "Right":
                    MoveRight();
                    break;
                case "Jump":
                    Jump();
                    break;
            }
        }
        return false;
    }
}