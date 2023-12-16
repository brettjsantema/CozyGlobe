using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Assets.Scripts.CozyGlobeUtils;

public class CozyGlobe : MonoBehaviour
{
    [SerializeField] private GameObject CandyCane;

    public int Presents;
    public List<Villager> Villagers;
    public List<Building> Buildings;
    public int TotalCapacity;
    public int TotalWidth;
    public Vector2Int BuildingsOrigin;
    public int MaxWidth;

    public Camera MainCamera;
    public BoxCollider2D[] Buttons;
    public enum ButtonNames { Elf, House, PretzelStand, Igloo, GingerbreadHouse, Workshop };
    public TextMeshProUGUI PresentsCount;
    public TextMeshProUGUI VillagersCount;

    private float ccTimer;
    private const float ccDelaySeconds = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        ccTimer = 0;
        MeshRenderer r = GameObject.Find("Background").GetComponent<MeshRenderer>();
        r.sortingLayerName = "Background";
        r.sortingOrder = -99;

        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Buttons = new BoxCollider2D[6];
        Buttons[0] = GameObject.Find("Buy Elf Button").GetComponent<BoxCollider2D>();
        Buttons[1] = GameObject.Find("Buy House Button").GetComponent<BoxCollider2D>();
        Buttons[2] = GameObject.Find("Buy Pretzel Stand Button").GetComponent<BoxCollider2D>();
        Buttons[3] = GameObject.Find("Buy Igloo Button").GetComponent<BoxCollider2D>();
        Buttons[4] = GameObject.Find("Buy Gingerbread House Button").GetComponent<BoxCollider2D>();
        Buttons[5] = GameObject.Find("Buy Workshop Button").GetComponent<BoxCollider2D>();

        Presents = 1000;

        Villagers = new List<Villager>();
        Buildings = new List<Building>();
        Buildings.Add(new Building(BuildingType.House));
        TotalCapacity = Building.Capacity[(int) BuildingType.House];
        TotalWidth = Building.Tilewidth[(int) BuildingType.House];
        BuildingsOrigin = new Vector2Int(-10, -1);
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
            bool buttonPressed = false;
            Vector2 worldCoordClickPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            for (int i = 0; i < Buttons.Length; i++)
			{
                BoxCollider2D button = Buttons[i];
                if(button.OverlapPoint(worldCoordClickPosition))
				{
                    buttonPressed = true;
                    switch ((ButtonNames) i)
					{
                        case ButtonNames.Elf: SpawnElf();  break;
                        case ButtonNames.House: Build(BuildingType.House); break;
                        case ButtonNames.PretzelStand: Build(BuildingType.PretzelStand); break;
                        case ButtonNames.Igloo: Build(BuildingType.Igloo); break;
                        case ButtonNames.GingerbreadHouse: Build(BuildingType.GingerbreadHouse); break;
                        case ButtonNames.Workshop: Build(BuildingType.Workshop); break;
                        default: break;
					}
				}
			}
            if(!buttonPressed)
			{
                SpawnCandyCane(worldCoordClickPosition);
			}
		} else if (Input.GetMouseButton(0) && ccTimer >= ccDelaySeconds) //Click and hold to spam candy canes
		{
            ccTimer = 0;
            Vector2 worldCoordClickPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            SpawnCandyCane(worldCoordClickPosition);
        }
        ccTimer += Time.deltaTime;
    }

	private void FixedUpdate()
	{
        
	}
	public void SpawnCandyCane(Vector3 worldPos)
	{
        GameObject candyCane = Instantiate(CandyCane, worldPos, Quaternion.identity);
        candyCane.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-40f, 40f));
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
                Vector2Int TopLeft = BuildingsOrigin + new Vector2Int(TotalWidth, height);

                Buildings.Add(new Building(BuildingType.House));
                Presents -= Building.Price[(int)type];
                TotalWidth += width;
                TotalCapacity += Building.Capacity[(int)type];
                RefreshPresentsCount();
                RefreshVillagersCount();

                Tilemap tilemap = transform.GetComponent<Tilemap>();
                for(int y = TopLeft.y; y > TopLeft.y - height - 1; y--)
                    for(int x = TopLeft.x; x < TopLeft.x + width; x++)
					{
                        string name = GetTileAssetName(type, x - TopLeft.x, y - TopLeft.y);
                        if (name != null)
                        {
                            TileBase tile = Resources.Load<Tile>(name);
                            tilemap.SetTile(new Vector3Int(x, y, 0), tile);
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
    public static int[] Capacity = { 4, 2, 6, 12, 20 };
    public static int[] Price = { 5, 10, 20, 200, 800 };
    public static int[] Tilewidth = { 3, 3, 2, 4, 5 };
    public static int[] Tileheight = { 4, 4, 2, 5, 8 };
    public int TileWidth;
    public Building(BuildingType type)
	{
        Type = type;
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
