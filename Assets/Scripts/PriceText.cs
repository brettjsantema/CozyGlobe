using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriceText : MonoBehaviour
{
    [SerializeField] public int buttonIndex;
    [SerializeField] public bool isRightOfScreen = false;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        if(isRightOfScreen) tmp.text = "-" + Building.Price[buttonIndex];
        else tmp.text = "-" + Villager.Price[buttonIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
