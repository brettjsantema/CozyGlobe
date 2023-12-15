using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CandyCane : MonoBehaviour
{
    public GameObject FadeOutLine;
    public float FadeSpeed;

    private void Start()
    {
        FadeOutLine = GameObject.Find("Candy Canes Fade Line");
    }

    private void Update()
    {
        Color ObjectColor = this.GetComponent<SpriteRenderer>().material.color;
        float FadeAmount = ObjectColor.a - (FadeSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }

    IEnumerator FadeOut()
    {
        Debug.Log("Coroutine started");
        Color ObjectColor = this.GetComponent<SpriteRenderer>().material.color;
        float FadeAmount = ObjectColor.a - (FadeSpeed * Time.deltaTime);
        switch (ObjectColor.a)
        {
            case 0: Debug.Log("Aplah @ 0"); break;
        }
        yield return null;
    }
}
