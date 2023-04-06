using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelUI : MonoBehaviour
{
    GlobalManager globalObj;
    Text waterText;

    // Use this for initialization
    void Start()
    {
        globalObj = GameObject.Find("GlobalObject").GetComponent<GlobalManager>();
        waterText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        waterText.text = "Water: " + globalObj.waterLevel.ToString() + "%";
    }
}
