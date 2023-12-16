using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.CozyGlobeUtils;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] public int buttonIndex = 0;
    private CozyGlobe cozyGlobe;
	public void Start()
	{
        cozyGlobe = GameObject.Find("CozyGlobeTiles").GetComponent<CozyGlobe>();

    }
	public void OnButtonPress()
	{
        Button btn = GetComponent<Button>();
        Debug.Log("Button Pressed " + buttonIndex);
        switch(buttonIndex)
		{
            case 0: cozyGlobe.Build(BuildingType.Shed); break;
            case 1: cozyGlobe.Build(BuildingType.Igloo); break; 
            case 2: cozyGlobe.Build(BuildingType.PretzelStand); break;
            case 3: cozyGlobe.Build(BuildingType.CookieStand); break;
            case 4: cozyGlobe.Build(BuildingType.GingerbreadHouse); break;
            case 5: cozyGlobe.Build(BuildingType.Workshop); break;
            case 6: cozyGlobe.SpawnVillager(VillagerType.Elf); break;
            case 7: cozyGlobe.SpawnVillager(VillagerType.Craftsman); break;
            case 8: cozyGlobe.SpawnVillager(VillagerType.Snowman); break;
            case 9: cozyGlobe.SpawnVillager(VillagerType.Baker); break;
            case 10: cozyGlobe.SpawnVillager(VillagerType.GingerbreadMan); break;
            case 11: cozyGlobe.SpawnVillager(VillagerType.Iceman); break;
        }
	}
}