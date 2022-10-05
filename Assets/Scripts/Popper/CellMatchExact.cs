using AGK.GameGrids;

namespace Popper
{
    public class CellMatchExact<TData>
        where TData : ICellContentMatch
    {
        protected TData accepted;

        public TData Current => accepted;

        public CellMatchExact(TData data)
        {
            accepted = data;
        }

        public bool IsAccepted(IMatchKey<TData> data)
        {
            return data.IsMatch(accepted);
        }
    }

    public interface IAccepted<TData>
    {
        bool IsAccepted(TData other);
    }
}