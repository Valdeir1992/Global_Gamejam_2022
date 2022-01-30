public sealed class DefaultAnimation : UnityEngine.MonoBehaviour, IAnimationController
{
    private PlayerMediator _player;
    private UnityEngine.Animator _anim;
    public void Configure(PlayerMediator player)
    {
        _player = player;
        _anim = GetComponent<UnityEngine.Animator>();
    }

    public void Jump()
    {
        _anim.SetTrigger("Jump");
    }

    public void SetGrounded(bool grounded)
    {
        _anim.SetBool("Grounded", grounded);
    }

    public void Walk(bool walk)
    {
        _anim.SetBool("Walk", walk);
    }
}
