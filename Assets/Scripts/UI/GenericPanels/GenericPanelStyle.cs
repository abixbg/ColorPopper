using Unity.Mathematics;
using UnityEngine;

namespace AGK.UI.Panels
{
    [CreateAssetMenu(fileName = "style-", menuName = "AGK/UI/Panel Style")]
    public class GenericPanelStyle : ScriptableObject
    {
        [SerializeField] private float2 rectSize;
        [SerializeField] private Sprite background;
        [SerializeField] private Sprite icon;

        public float2 RectSize { get => rectSize; }
        public Sprite SpriteBackground { get => background; }
        public Sprite SpriteIcon { get => icon; }
    }
}