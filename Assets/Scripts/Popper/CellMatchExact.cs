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

        public bool IsAccepted(ICellContentMatch data)
        {
            return data.IsMatch(accepted);
        }
    }
}