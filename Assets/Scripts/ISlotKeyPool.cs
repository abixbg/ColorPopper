using Popper;
using System.Collections.Generic;

public interface ISlotKeyPool 
{
    SlotContent GetRandom();
    SlotContent GetRandomNew(SlotContent excluding);
}

