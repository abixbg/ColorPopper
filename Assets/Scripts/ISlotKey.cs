namespace Popper
{
    public interface ISlotKey<TData>
        where TData : ISlotKeyData
    {
        bool IsMatch(TData other);
    }
}
