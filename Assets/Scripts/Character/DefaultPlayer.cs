public sealed class DefaultPlayer : PlayerMediator
{
    private IMotionController _motion;
    private IAudioController _audio;
    private IAnimationController _animation;
    private void Awake()
    {
        _motion = GetComponent<IMotionController>();
        _motion.Configure(this);

        _audio = GetComponent<IAudioController>();
        _audio.Configure(this);

        _animation = GetComponent<IAnimationController>();
        _animation.Configure(this);
    }
    private void Update()
    {
        _animation.SetGrounded(_motion.Grounded);
    }
    public override void Jump()
    {
        _audio.Jump();
        _animation.Jump();
    }

    public override void Walk()
    {
        _audio.Steps();
        _animation.Walk(_motion.Walk);
    } 
}

public interface IAudioController : IController
{
    void Jump();

    void Steps();
} 

public interface IAnimationController : IController
{
    void Jump();
    void SetGrounded(bool grounded);
    void Walk(bool walk);
}
