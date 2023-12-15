using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CozyGlobeUtils;

public class CozyGlobe : MonoBehaviour
{
    public int Presents;
    public List<Villager> Villagers;
    public List<Building> Buildings;
    public Vector2Int BuildingsOrigin;
    public int MaxWidth;

    public Camera MainCamera;
    public BoxCollider2D[] Buttons;
    public enum ButtonNames { Elf, House };

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Buttons = new BoxCollider2D[2];
        Buttons[0] = GameObject.Find("Buy Elf Button").GetComponent<BoxCollider2D>();
        Buttons[1] = GameObject.Find("Buy House Button").GetComponent<BoxCollider2D>();
        Presents = 10;
        Villagers = new List<Villager>();
        Buildings = new List<Building>();
        Buildings.Add(new Building(BuildingType.Shack));
        BuildingsOrigin = new Vector2Int(-10, -4);
        MaxWidth = 20;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Villager villager in Villagers) if (Time.frameCount % (3600 / villager.PresentsPerMinute) == 0) Presents++;
        if(Input.GetMouseButtonDown(0))
		{
            Vector2 worldCoordClickPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            for(int i = 0; i < Buttons.Length; i++)
			{
                BoxCollider2D button = Buttons[i];
                if(button.OverlapPoint(worldCoordClickPosition))
				{
                    switch((ButtonNames) i)
					{
                        case ButtonNames.Elf: SpawnElf();  break;
                        case ButtonNames.House: BuildHouse(); break;
                        default: break;
					}
				}
			}
		}
    }

    public void BuildHouse()
	{
        Debug.Log("CLICKED HOUSE BUTTON");
	}

    public void SpawnElf()
    {
        Debug.Log("!!CLICKED ELF BUTTON!!");
    }
}

public static class CozyGlobeUtils
{
    public enum VillagerType { Elf, Santa, GingerbreadMan, SnowMan };
    public enum BuildingType { Shack, Igloo, Workshop, PretzelStand };
    public enum DecorationType { DeadTree, ElmTree, BlueFlowers, RedFlowers};
    public static int[] ExpToNextLvl = new int[] { 0, 100, 500, 2000, 5000, 10000 }; // 1-indexed
}

public class Building
{
    public BuildingType Type { get; set; }
    public int Capacity;
    public int TileWidth;
    public Building(BuildingType type)
	{
        Type = type;
        switch(Type)
		{
            case BuildingType.Shack: Capacity = 4; TileWidth = 3; break;
            case BuildingType.Igloo: Capacity = 2; break;
            case BuildingType.Workshop: Capacity = 8; break;
            case BuildingType.PretzelStand: Capacity = 1; break;
            default: Capacity = 0; break;
        }
	}
}

public class Decoration
{

}
public class Villager
{
    public VillagerType Type { get; set; }
    public string Name { get; set; }
    public int PresentsPerMinute { get; set; }
    public int Exp { get; set; }
    public int Level { get; set; } 
    public int TotalPresentsEarned { get; set; }
    public Villager(VillagerType type)
	{
        Type = type;
        Name = nameof(Type);
        Level = 1;
        switch(Type)
		{
            case VillagerType.Elf: PresentsPerMinute = 1; break;
            default: PresentsPerMinute = 0; break;
		}
	}

}
