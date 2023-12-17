using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusPresent : MonoBehaviour
{
    private float timer;
    private float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        // Play sound
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        if(timer > 0.25f) Destroy(gameObject);
    }
}
