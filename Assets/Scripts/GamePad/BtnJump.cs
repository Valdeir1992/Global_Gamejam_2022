public sealed class BtnJump : AbstractPadButton
{
    protected override void BtnAction()
    {
        MessageSystem.Instance.Notify(new InputMessage("Jump"));
    }
}
