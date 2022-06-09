using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public Slot slotPrefab;
    public Dot dotPrefab;
    public Loot lootPrefab;
    

    public float offset;

    public int countX;
    public int countY;

    public ColorPalette colorPalette;

    public Slot[] gridSlots;
    public List<Color> dotColors;

    void Start()
    {
        GenerateSlots();
        LockWithDots();
        FillWithLoot();

        UpdateColorList();
        //test
        GameManager.current.currentDotCollector.SwitchAcceptedColor();

    }

    void UpdateColorList()
    {
        dotColors.Clear();
        for (int i = 0; i < countX * countY; i++)
        {
            if (dotColors.Contains(gridSlots[i].keyhole.color) == false)
            {
                if (gridSlots[i].isActive == true)
                {
                    dotColors.Add(gridSlots[i].keyhole.color);
                }               
            }
        }
    }

    public void ValidateGrid()
    {
        // update avaiable colors in grid
        UpdateColorList();

        // check if grid have unopened slots with collector color and switch it if not
        if (dotColors.Contains(GameManager.current.currentDotCollector.acceptedColor) == false)
        {
            Debug.Log("nothing");         
            GameManager.current.currentDotCollector.SwitchAcceptedColor();
        }
    }


    public Color GetRandomColor()
    {
        if (dotColors.Count > 0 )
        {
            int index = Random.Range(0, dotColors.Count);
            ///Debug.Log(dotColors.Count);
            return dotColors[index];
        }
        return new Color();
    }

    // instantiates slot objects in grid
    public void GenerateSlots()
    {
        int count = countX * countY;
        gridSlots = new Slot[count];

        int index = 0;

        for (int i = 0; i < countX; i++)
        {
            for (int j = 0; j < countY; j++)
            {
                Vector3 pos = new Vector3(i * offset, j * offset, 0) + transform.position;

                //instantiating dots in grid
                gridSlots[index] = Instantiate(slotPrefab, pos, Quaternion.identity) as Slot;

                //make slots gameobjects parent of grid
                gridSlots[index].transform.parent = gameObject.transform;

                index++;
            }
        }
    }

    // fills the grid with dot gameobjects
    public void LockWithDots()
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            //instantiating dots in grid
            gridSlots[i].keyhole = Instantiate(dotPrefab, gridSlots[i].transform.position, Quaternion.identity) as Dot;

            //make dot gameobjects parent of slot
            gridSlots[i].keyhole.transform.parent = gridSlots[i].transform;

            //assigning colors from the palette
            gridSlots[i].keyhole.SetColor(colorPalette.GetRandomColor());
        }
    }


    public void FillWithLoot()
    { 
        for (int i = 0; i < gridSlots.Length; i++)
        {
            //instantiating dots in grid
            gridSlots[i].loot = Instantiate(lootPrefab, gridSlots[i].transform.position, Quaternion.identity) as Loot;

            //make dot gameobjects parent of slot
            gridSlots[i].loot.transform.parent = gridSlots[i].transform;
        }
    }

}
