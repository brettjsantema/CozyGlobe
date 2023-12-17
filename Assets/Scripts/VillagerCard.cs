using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillagerCard : MonoBehaviour
{
    public Villager villager;
    public int cardIndex = 0;

    private float xOffset = -7f;
    private float yOffset = 3.5f;
    private float colSpacing = 8.0f;
    private float rowSpacing = 2.5f;

    void Awake()
	{

    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 offset = new Vector2(xOffset, yOffset);
        transform.position = offset;
        switch (cardIndex % 6)
		{
            case 0: break;
            case 1: transform.position = offset + (new Vector2(colSpacing, 0)); break;
            case 2: transform.position = offset + (new Vector2(0, -rowSpacing)); break;
            case 3: transform.position = offset + (new Vector2(colSpacing, -rowSpacing)); break;
            case 4: transform.position = offset + (new Vector2(0, -rowSpacing * 2)); break;
            case 5: transform.position = offset + (new Vector2(colSpacing, -rowSpacing * 2)); break;
        }

        transform.Find("VillagerIcon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(villager.Type.ToString());
        transform.Find("VillagerName").GetComponent<TextMeshProUGUI>().text = villager.Nickname;
        transform.Find("VillagerExpText").GetComponent<TextMeshProUGUI>().text = "Level " + villager.Level + " ~ " + villager.Exp + "/" + (Villager.ExpToNextLvl[villager.Level] * ((int)villager.Type + 1));
        transform.Find("VillagerPresentsText").GetComponent<TextMeshProUGUI>().text = "Every " + (villager.Level * Villager.SecondsPerPresent[(int)villager.Type]) + " Seconds";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
	{
		Destroy(gameObject);
	}
}
