using AGK.GameGrids;
using UnityEngine;

namespace Popper
{
    [System.Serializable]
    public class ContentPicture : SlotContent
    {
        [SerializeField] private int group;

        public int Group => group;

        public ContentPicture(int group)
        {
            this.group = group;
        }

        public override bool IsMatch(ICellContentMatch other)
        {
            if (other is ContentPicture)
            {
                return ((ContentPicture)other).Group == Group;
            }
            else
            {
                Debug.LogAssertion("Wrong Cast!");
                return false;
            }
        }
    }
}