using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandyCane : MonoBehaviour
{
    public CozyGlobe cozyGlobe;
    public BoxCollider2D ccCollider;
    public Transform target;
    public float FadeSpeed;
    public SpriteRenderer Renderer;
    private float timer;
    public bool fadeOut;

    private void Awake()
    {
        ccCollider = GetComponent<BoxCollider2D>();
        cozyGlobe = GameObject.Find("CozyGlobeTiles").GetComponent<CozyGlobe>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-40, 40));
    }

    private void Update()
    {
        if (transform.position.y < -3) fadeOut = true;
        if(fadeOut)
        {
            timer += Time.deltaTime;

            //Get current color
            Color spriteColor = Renderer.material.color;

            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, timer / 0.5f);
            if (alpha <= 0) Destroy(gameObject);

            //Change alpha only
            Renderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
        }
        foreach (Villager v in cozyGlobe.Villagers)
            if (ccCollider.IsTouching(v.vCollider))
			{
                Debug.Log(v.Nickname + " collided with a candy cane!");
                v.EatCandyCane();
                GameObject.Destroy(gameObject);
                //Play sound
			}
                
    }
}
