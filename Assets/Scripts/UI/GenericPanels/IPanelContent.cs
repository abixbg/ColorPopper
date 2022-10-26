using UnityEngine;


namespace AGK.UI.Panels
{
    public interface IPanelContent<TData>
    {
        void SetInitialState();
        void UpdateData(TData data);
    }
}
