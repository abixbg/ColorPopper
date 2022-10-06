using AGK.GameGrids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotContent : ICellContentMatch
{
    public abstract bool IsMatch(ICellContentMatch other);
}
