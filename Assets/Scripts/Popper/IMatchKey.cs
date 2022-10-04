namespace Popper
{
    public interface IMatchKey<TData>
    {
        bool IsMatch(TData other);
    }
}
