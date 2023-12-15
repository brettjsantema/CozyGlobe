using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CozyGlobeUtils;

public class CozyGlobe : MonoBehaviour
{
    public int Presents;
    public List<Villager> Villagers;
    public List<Building> Buildings;

    // Start is called before the first frame update
    void Start()
    {
        Presents = 10;
        Villagers = new List<Villager>();
        Buildings = new List<Building>();
        Buildings.Add(new Building(BuildingType.Shack));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Villager villager in Villagers)
		{
            int framesPerPresent =  3600 / villager.PresentsPerMinute;
            if (Time.frameCount % framesPerPresent == 0) Presents++;
		}
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
    public Building(BuildingType type)
	{
        Type = type;
        switch(Type)
		{
            case BuildingType.Shack: Capacity = 4; break;
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
