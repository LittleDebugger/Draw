namespace Draw.Core.CoreInterfaces
{
    public interface IReceiver<out TInputContext>
        where TInputContext : IInputContext
    {
        TInputContext ReceiveInput();
    }
}