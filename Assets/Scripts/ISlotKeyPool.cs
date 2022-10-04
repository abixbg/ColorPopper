using Popper;
using System.Collections.Generic;

public interface ISlotKeyPool<TKey>
{
    List<TKey> Pool { get; }

    TKey GetRandom();
    void Replace(List<TKey> keys);

    bool Contains(TKey key);
}
