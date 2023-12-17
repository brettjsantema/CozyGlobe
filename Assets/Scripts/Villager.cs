using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.CozyGlobeUtils;

public class Villager : MonoBehaviour
{
    public VillagerType Type { get; set; }
    public string Name { get; set; }
    public int Exp { get; set; }
    public int Level { get; set; }
    public int TotalPresentsEarned { get; set; }
    public static int[] Price = { 5, 10, 20, 100, 185, 350 };
    public static int[] PresentsPerMinute = { 5, 10, 20, 100, 185, 350 };
    public Villager(VillagerType type)
    {
        Type = type;
        Level = 1;
    }
	void Start()
	{
        transform.position = new Vector3(Random.Range(-1f, 1.75f), 8, 0);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        rb.AddForce(new Vector3(Random.Range(-200, 200), -5, 0));
        rb.AddTorque(Random.Range(-10, 10));
    }
	void Update()
	{
		
	}

    public void SetType(VillagerType type)
	{
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        string assetName = type.ToString();
        Sprite sprite = Resources.Load<Sprite>(assetName);
        sr.sprite = sprite;
        Name = type.ToString();
    }

}