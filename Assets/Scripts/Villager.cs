
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.CozyGlobeUtils;

public class Villager : MonoBehaviour
{
    [SerializeField] public GameObject PlusPresent;
    private CozyGlobe cozyGlobe;
    public BoxCollider2D vCollider;
    public VillagerType Type { get; set; }
    public string Nickname;
    public int Exp { get; set; }
    public int Level { get; set; }
    public int TotalPresentsEarned { get; set; }
    public static int[] Price = { 12, 10, 20, 100, 185, 350 };
    public static float[] SecondsPerPresent = { 12, 8, 6, 4, 2, 1 };
    private float timer = 0;
	private void Awake()
	{
		cozyGlobe = GameObject.Find("CozyGlobeTiles").GetComponent<CozyGlobe>();
        vCollider = GetComponent<BoxCollider2D>();
	}
	void Start()
	{
        Level = 1;
        timer = 0;
        
        transform.position = new Vector3(Random.Range(-1f, 1.75f), 8, 0);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(Random.Range(-200, 200), -5, 0));
        rb.AddTorque(Random.Range(-10, 10));
    }
	void Update()
	{
        timer += Time.deltaTime;
        if (timer >= SecondsPerPresent[(int)Type])
        {
            Instantiate(PlusPresent, transform);
            TotalPresentsEarned++;
            cozyGlobe.Presents++;
            cozyGlobe.RefreshPresentsCount();
            timer = 0;
        }
    }

    public void SetType(VillagerType type)
	{
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        string assetName = type.ToString();
        Sprite sprite = Resources.Load<Sprite>(assetName);
        sr.sprite = sprite;
        Type = type;
        Nickname = type.ToString();
    }

}