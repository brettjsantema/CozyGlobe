using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCanes : MonoBehaviour
{
    [SerializeField] private GameObject candyCane;

    void Update()
    {
        DropCandyCane();
    }

    void DropCandyCane()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(candyCane, cursorPos, Quaternion.identity);
        }
    }
}
