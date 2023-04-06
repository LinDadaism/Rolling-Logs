using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProPackUI : MonoBehaviour
{
    GlobalManager globalObj;
    Text numPackText;

    // Use this for initialization
    void Start()
    {
        globalObj = GameObject.Find("GlobalObject").GetComponent<GlobalManager>();
        numPackText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        numPackText.text = "Buckets: " + globalObj.numProPack.ToString();
    }
}
