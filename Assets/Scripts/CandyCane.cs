using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandyCane : MonoBehaviour
{
    public Transform target;
    public float FadeSpeed;
    public SpriteRenderer Renderer;

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        target = GameObject.Find("CandyCaneLine").GetComponent<Transform>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y <= target.position.y)
        {
            StartCoroutine(fadeOut(Renderer, FadeSpeed));
        }
    }


    IEnumerator fadeOut(SpriteRenderer MyRenderer, float duration)
    {
        Debug.Log("started");
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            if(alpha <= 0)
            {
                GameObject.Destroy(gameObject);
            }

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }
}
