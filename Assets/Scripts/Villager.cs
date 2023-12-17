
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.CozyGlobeUtils;

public class Villager : MonoBehaviour
{
    [SerializeField] public GameObject PlusPresent;
    [SerializeField] public AudioClip SpawnSound;
    [SerializeField] public AudioClip PresentSound;
    [SerializeField] public AudioClip LevelUpSound;

    private CozyGlobe cozyGlobe;
    private AudioSource audioSource;
    public BoxCollider2D vCollider;
    public VillagerType Type { get; set; }
    public string Nickname;
    public int Exp { get; set; }
    public int Level { get; set; }
    public int TotalPresentsEarned { get; set; }

    public static int[] ExpToNextLvl = new int[] { 0, 25, 50, 100, 200, 400, 1000 }; // 1-indexed
    public static int[] Price = { 5, 10, 20, 100, 200, 350 };
    public static float[] SecondsPerPresent = { 8, 6, 4, 2, 1, 0.5f };
    private float timer = 0;
	private void Awake()
	{
		cozyGlobe = GameObject.Find("CozyGlobeTiles").GetComponent<CozyGlobe>();
        vCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(SpawnSound);
    }
	void Start()
	{
        Level = 1;
        timer = 0;
        
        transform.position = new Vector3(UnityEngine.Random.Range(-1f, 1.75f), 8, 0);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(UnityEngine.Random.Range(-200, 200), -5, 0));
        rb.AddTorque(UnityEngine.Random.Range(-10, 10));
    }
	void Update()
	{
        timer += Time.deltaTime;
        if (timer >= Level * SecondsPerPresent[(int)Type])
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
        Nickname = GenerateNickName();
    }

    public void EatCandyCane()
	{
        audioSource.PlayOneShot(PresentSound);
        Exp++;
        int expToNextLevel = ExpToNextLvl[(int)Level] * ((int)Type + 1);
        if (Exp > expToNextLevel)
		{
            Level++;
            Exp = 0;
            Debug.Log(Nickname + " leveled up to level " + Level);
            audioSource.PlayOneShot(LevelUpSound);
		}

	}

    public string GenerateNickName()
	{
		System.Random random = new System.Random();
        int idx = 0;
        switch (Type)
		{
            case VillagerType.Elf:
                idx = random.Next(elfPrefixes.Length);
				string elfPrefix = elfPrefixes[idx] + " ";
                idx = random.Next(elfFirstNames.Length);
                string elfFirst = elfFirstNames[idx] + " ";
                idx = random.Next(elfLastNames.Length);
                string elfLast = elfLastNames[idx];
                return elfPrefix + elfFirst + elfLast;
            case VillagerType.Craftsman:
                idx = random.Next(craftsmanPrefixes.Length);
                string craftsmanPrefix = craftsmanPrefixes[idx] + " ";
                idx = random.Next(craftsmanFirstNames.Length);
                string craftsmanFirst = craftsmanFirstNames[idx] + " ";
                idx = random.Next(craftsmanLastNames.Length);
                string craftsmanLast = craftsmanLastNames[idx];
                return craftsmanPrefix + craftsmanFirst + craftsmanLast;
            case VillagerType.Snowman:
                idx = random.Next(snowmanPrefixes.Length);
                string snowmanPrefix = snowmanPrefixes[idx] + " ";
                idx = random.Next(snowmanFirstNames.Length);
                string snowmanFirst = snowmanFirstNames[idx] + " ";
                idx = random.Next(snowmanLastNames.Length);
                string snowmanLast = snowmanLastNames[idx];
                return snowmanPrefix + snowmanFirst + snowmanLast;
            case VillagerType.Baker:
                idx = random.Next(bakerPrefixes.Length);
                string bakerPrefix = bakerPrefixes[idx] + " ";
                idx = random.Next(bakerFirstNames.Length);
                string bakerFirst = bakerFirstNames[idx] + " ";
                idx = random.Next(bakerLastNames.Length);
                string bakerLast =bakerLastNames[idx];
                return bakerPrefix + bakerFirst + bakerLast;
            case VillagerType.GingerbreadMan:
                idx = random.Next(gingerbreadPrefixes.Length);
                string gbPrefix = gingerbreadPrefixes[idx] + " ";
                idx = random.Next(gingerbreadFirstNames.Length);
                string gbFirst = gingerbreadFirstNames[idx] + " ";
                idx = random.Next(gingerbreadLastNames.Length);
                string gbLast = gingerbreadLastNames[idx];
                return gbPrefix + gbFirst + gbLast;
            case VillagerType.Iceman:
                idx = random.Next(jackFrostPrefixes.Length);
                string jfPrefix = jackFrostPrefixes[idx] + " ";
                idx = random.Next(jackFrostFirstNames.Length);
                string jfFirst = jackFrostFirstNames[idx] + " ";
                idx = random.Next(jackFrostLastNames.Length);
                string jfLast = jackFrostLastNames[idx];
                return jfPrefix + jfFirst + jfLast;
        }
        return "";
    }
}