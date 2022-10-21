public interface ISlotKeyPool 
{
    SlotContent GetRandom();
    SlotContent GetRandomNew(SlotContent excluding);
}

