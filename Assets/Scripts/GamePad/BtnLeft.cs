public sealed class BtnLeft : AbstractPadButton
{
    protected override void BtnAction()
    {
        MessageSystem.Instance.Notify(new InputMessage("Left"));
    }
}
