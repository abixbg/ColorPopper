using Popper;
using System.Collections.Generic;

public interface ISlotKeyPool<TKey>
    where TKey : ISlotKeyData
{
    List<TKey> Pool { get; }

    TKey GetRandom();
    void Replace(List<TKey> keys);

    bool Contains(TKey key);
}
