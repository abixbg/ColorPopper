namespace Popper
{
    public interface ISlotKey<TData>
        where TData : ISlotKeyData

    {
        bool Match(TData other);
    }
}
