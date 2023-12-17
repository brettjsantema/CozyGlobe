using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Assets.Scripts.CozyGlobeUtils;

public class CozyGlobe : MonoBehaviour
{
    [SerializeField] private GameObject CandyCane;
    [SerializeField] private Villager Villager;
    [SerializeField] private AudioClip ErrorSound;
    [SerializeField] public GameObject VillagersView;
    [SerializeField] public VillagerCard VillagerCardPrefab;
    private AudioSource audioSource;
    private int currentPageIndex = 0;
    public int Presents;
    public List<Villager> Villagers;
    public List<Building> Buildings;
    public int TotalCapacity;
    public int TotalWidth;
    public Vector2Int BuildingsOrigin;
    public int MaxWidth;

    public Camera MainCamera;
    public enum ButtonNames { Elf, House, PretzelStand, Igloo, GingerbreadHouse, Workshop };
    public TextMeshProUGUI PresentsCount;
    public TextMeshProUGUI VillagersCount;

    private float ccTimer;
    private const float ccDelaySeconds = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        VillagersView.SetActive(false);
        ccTimer = 0;
        MeshRenderer r = GameObject.Find("Background").GetComponent<MeshRenderer>();
        r.sortingLayerName = "Background";
        r.sortingOrder = -99;
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Presents = 60;
        BuildingsOrigin = new Vector2Int(-10, -1);
        MaxWidth = 20;

        PresentsCount = GameObject.Find("Presents Count").GetComponent<TextMeshProUGUI>();
        VillagersCount = GameObject.Find("Villagers Count").GetComponent<TextMeshProUGUI>();
        PresentsCount.outlineWidth = 0.2f;
        VillagersCount.outlineWidth = 0.2f;
        PresentsCount.outlineColor = Color.black;
        VillagersCount.outlineColor = Color.black;

        // Start out with 1 building and an elf
        Villagers = new List<Villager>();
        Buildings = new List<Building>();
        Build(BuildingType.Shed);
        SpawnVillager(VillagerType.Elf);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
		{
            Vector2 worldCoordClickPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(CandyCane, worldCoordClickPosition, Quaternion.identity);
        } else if (Input.GetMouseButton(0) && ccTimer >= ccDelaySeconds) //Click and hold to spam candy canes
		{
            ccTimer = 0;
            Vector2 worldCoordClickPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(CandyCane, worldCoordClickPosition, Quaternion.identity);
        }
        ccTimer += Time.deltaTime;
    }

	private void FixedUpdate()
	{
        
	}

    public void Build(BuildingType type)
	{
        if (Presents >= Building.Price[(int)type])
        {
            if (Building.Tilewidth[(int)type] <= MaxWidth - TotalWidth)
            {
                int width = Building.Tilewidth[(int)type];
                int height = Building.Tileheight[(int)type];
                Vector2Int TopLeft = BuildingsOrigin + new Vector2Int(TotalWidth, height);

                Buildings.Add(new Building(type));
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

    public void SpawnVillager(VillagerType type)
    {
        if (Presents >= Villager.Price[(int)type])
        {
            if (Villagers.Count < TotalCapacity)
            {

                Presents -= Villager.Price[(int)type];
                Villager v = Instantiate(Villager, Vector3.zero, Quaternion.identity);
                v.SetType(type);
                Villagers.Add(v);
                RefreshPresentsCount();
                RefreshVillagersCount();
            }
            else
            {
                Debug.Log("Not enough room!");
                audioSource.PlayOneShot(ErrorSound);
            }
        }
        else
        {
            Debug.Log("Not enough presents!");
            audioSource.PlayOneShot(ErrorSound);
        }
    }

    public void ToggleVillagerView()
	{
        VillagersView.SetActive(!VillagersView.activeInHierarchy);
        if (VillagersView.activeInHierarchy)
        {
            int maxPage = ((Villagers.Count - 1) / 6) + 1;
            VillagersView.transform.Find("PageNumber").GetComponent<TextMeshProUGUI>().text = "Page " + (currentPageIndex + 1) + "/" + maxPage;
            int startIndex = currentPageIndex * 6;
            for(int i = startIndex; i < Villagers.Count && i < startIndex + 6; i++)
            {
                Villager v = Villagers[i];
                VillagerCard vc = Instantiate(VillagerCardPrefab, VillagersView.transform);
                vc.cardIndex = i;
                vc.villager = v;
            }
        }
        else currentPageIndex = 0;
    }

    public void NextPage()
	{
        int maxPage = ((Villagers.Count - 1) / 6) + 1;
        if (maxPage - 1 == currentPageIndex) audioSource.PlayOneShot(ErrorSound);
		else
		{
            foreach(VillagerCard vc in VillagersView.GetComponentsInChildren<VillagerCard>()) Destroy(vc);
            VillagersView.SetActive(false);
            currentPageIndex++;
            ToggleVillagerView();
            VillagersView.transform.Find("PageNumber").GetComponent<TextMeshProUGUI>().text = "Page " + (currentPageIndex + 1) + "/" + maxPage;
        }
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
    public static int[] Capacity = { 2, 4, 8, 12, 20, 50 };
    public static int[] Price = { 50, 100, 200, 400, 500, 1000 };
    public static int[] Tilewidth = { 3, 2, 3, 5, 4, 5 };
    public static int[] Tileheight = { 4, 2, 4, 4, 5, 8 };
    public int TileWidth;
    public Building(BuildingType type)
	{
        Type = type;
	}
}

public class Decoration
{

}

