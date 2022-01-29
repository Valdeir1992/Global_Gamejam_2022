public sealed class BtnRight : AbstractPadButton
{
    protected override void BtnAction()
    {
        MessageSystem.Instance.Notify(new InputMessage("Right"));
    }
}
