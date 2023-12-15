using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Assets.Scripts.CozyGlobeUtils;
using static CozyGlobeUtils;

public class CozyGlobe : MonoBehaviour
{
    public int Presents;
    public List<Villager> Villagers;
    public List<Building> Buildings;
    public int TotalCapacity;
    public int TotalWidth;
    public Vector2Int BuildingsOrigin;
    public int MaxWidth;

    public Camera MainCamera;
    public BoxCollider2D[] Buttons;
    public enum ButtonNames { Elf, House };
    public TextMeshProUGUI PresentsCount;
    public TextMeshProUGUI VillagersCount;

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
        Buildings.Add(new Building(BuildingType.House));
        TotalCapacity = Building.Capacity[(int) BuildingType.House];
        TotalWidth = Building.Tilewidth[(int) BuildingType.House];
        BuildingsOrigin = new Vector2Int(-10, -4);
        MaxWidth = 20;

        PresentsCount = GameObject.Find("Presents Count").GetComponent<TextMeshProUGUI>();
        VillagersCount = GameObject.Find("Villagers Count").GetComponent<TextMeshProUGUI>();
        PresentsCount.outlineWidth = 0.2f;
        VillagersCount.outlineWidth = 0.2f;
        PresentsCount.outlineColor = Color.black;
        VillagersCount.outlineColor = Color.black;
        RefreshVillagersCount();
        RefreshPresentsCount();
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
                        case ButtonNames.House: Build(BuildingType.House); break;
                        default: break;
					}
				}
			}
		}
    }

    public void Build(BuildingType type)
	{
        Debug.Log("CLICKED " + nameof(type) + " BUTTON");
        if (Presents >= Building.Price[(int)type])
        {
            if (Building.Tilewidth[(int)type] <= MaxWidth - TotalWidth)
            {
                int width = Building.Tilewidth[(int)type];
                int height = Building.Tileheight[(int)type];
                Buildings.Add(new Building(BuildingType.House));
                Presents -= Building.Price[(int)type];
                TotalWidth += width;
                TotalCapacity += Building.Capacity[(int)type];

                Tilemap tilemap = transform.GetComponent<Tilemap>();
                Vector2Int TopLeft = BuildingsOrigin + new Vector2Int(TotalWidth, height);
                for(int y = TopLeft.y; y > TopLeft.y - height; y--)
				{
                    for(int x = TopLeft.x; x < TopLeft.x + width; x++)
					{
                        tilemap.SetTile(new Vector3Int(x, y, 0), )
					}
				}
            }
            else Debug.Log("Not enough room!");
        }
        else Debug.Log("Not enough presents!");
	}

    public void SpawnElf()
    {
        Debug.Log("!!CLICKED ELF BUTTON!!");
    }

    public void RefreshVillagersCount()
	{
        VillagersCount.text = "*" + Villagers.Count + "/" + TotalCapacity + "*";
    }

    public void RefreshPresentsCount()
    {
        PresentsCount.text = "" + Presents;
    }
}

public class Building
{
    public BuildingType Type { get; set; }
    public static int[] Capacity = { 2, 2, 4, 8};
    public static int[] Price = { 5, 20, 50, 800 };
    public static int[] Tilewidth = { 3, 3, 3, 3 };
    public static int[] Tileheight = { 4, 4, 4, 4 };
    public int TileWidth;
    public Building(BuildingType type)
	{
        Type = type;
        switch(Type)
		{
            case BuildingType.House: break;
            case BuildingType.Igloo: break;
            case BuildingType.Workshop: break;
            case BuildingType.PretzelStand: break;
            default: break;
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
