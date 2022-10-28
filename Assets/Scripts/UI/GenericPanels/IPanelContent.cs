using System.ComponentModel;
using UnityEngine;


namespace AGK.UI.Panels
{
    public interface IPanelContent 
    {
        RectTransform Rect { get; }
        void AttachContent(ISingleContentBox box);
    }

    public interface IPanelContent<TData> : IPanelContent
    {

        void SetInitialState();
        void UpdateData(TData data);

    }
}
