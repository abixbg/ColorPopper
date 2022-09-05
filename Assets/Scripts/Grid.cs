using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System;

public class Grid : MonoBehaviour
{
    public Slot slotPrefab;
    public Dot dotPrefab;
    public Loot lootPrefab;
    public SpriteRenderer BoardVisual;
    public BoardCellGenerator generator;

    public CameraScreenFit gameView;

    [SerializeField] private ColorPalette colorPalette;

    public float CellSize;

    public int countX;
    public int countY;
    [SerializeField] private float2 _boardSize;
    public float2 BoardSize { get => _boardSize; }

    private BubblePoolColors bubblePool;

    public List<Slot> gridSlots;
    public List<Color> dotColors;

    public Action OnBoardChanged;

    private void Awake()
    {
        bubblePool = new BubblePoolColors(colorPalette);

        float halfcell = CellSize * 0.5f;

        generator.Construct(new int2(countX, countY), CellSize, slotPrefab, gameObject.transform);
        generator.GenerateCells();
        gridSlots = generator.Slots;

        _boardSize = new float2(countX * CellSize + halfcell, countY * CellSize + halfcell);
    }

    void Start()
    {

        LockWithDots();
        FillWithLoot();

        UpdateColorList();

        BoardVisual.size = new Vector2(BoardSize.x, BoardSize.y);
        OnBoardChanged?.Invoke();
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
        if (dotColors.Contains(GameManager.current.Level.AcceptedColor) == false)
        {
            Debug.Log("nothing");
            GameManager.current.Level.SwitchAcceptedColor();
        }
    }


    public Color GetRandomColor()
    {
        if (dotColors.Count > 0 )
        {
            int index = UnityEngine.Random.Range(0, dotColors.Count);
            ///Debug.Log(dotColors.Count);
            return dotColors[index];
        }
        return new Color();
    }

    //// instantiates slot objects in grid
    //public void GenerateCells()
    //{
    //    int count = countX * countY;
    //    gridSlots = new List<Slot>();

    //    int index = 0;

    //    for (int i = 0; i < countX; i++)
    //    {
    //        for (int j = 0; j < countY; j++)
    //        {
    //            Vector3 pos = new Vector3(i * CellSize, j * CellSize, 0) + transform.position;

    //            //instantiating dots in grid
    //            gridSlots.Add(Instantiate(slotPrefab, pos, Quaternion.identity));

    //            //make slots gameobjects parent of grid
    //            gridSlots[index].transform.parent = gameObject.transform;

    //            index++;
    //        }
    //    }
    //}

    // fills the grid with dot gameobjects
    public void LockWithDots()
    {
        for (int i = 0; i < gridSlots.Count; i++)
        {
            //instantiating dots in grid
            gridSlots[i].keyhole = Instantiate(dotPrefab, gridSlots[i].transform.position, Quaternion.identity) as Dot;

            //make dot gameobjects parent of slot
            gridSlots[i].keyhole.transform.parent = gridSlots[i].transform;

            //assigning colors from the palette
            gridSlots[i].keyhole.SetColor(bubblePool.GetRandomColor());
        }
    }


    public void FillWithLoot()
    { 
        for (int i = 0; i < gridSlots.Count; i++)
        {
            bool hasLoot = UnityEngine.Random.value >= 0.6f;

            if (hasLoot)
            {
                //instantiating dots in grid
                gridSlots[i].Loot = Instantiate(lootPrefab, gridSlots[i].transform.position, Quaternion.identity) as Loot;

                //make dot gameobjects parent of slot
                gridSlots[i].Loot.transform.parent = gridSlots[i].transform;
            }
        }
    }

}
