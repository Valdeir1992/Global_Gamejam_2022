public sealed class DefaultPlayer : PlayerMediator
{
    private IMotionController _motion;
    private IAudioController _audio;
    private void Awake()
    {
        _motion = GetComponent<IMotionController>();
        _motion.Configure(this);

        _audio = GetComponent<IAudioController>();
        _audio.Configure(this);
    }
    public override void Jump()
    {
        _audio.Jump();
    }

    public override void Walk()
    {
        _audio.Steps();
    } 
}

public interface IAudioController : IController
{
    void Jump();

    void Steps();
}
