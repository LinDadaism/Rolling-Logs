using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogWrapper : MonoBehaviour
{
    public float turnSpeed;
    public float thrustSpeed;

    private GameObject globalObj;
    private GameObject childObj;
    private Material mat;

    // Use this for initialization
    void Start()
    {
        turnSpeed = .5f; thrustSpeed = 0.2f;
        globalObj = GameObject.Find("GlobalObject");
        childObj = gameObject.transform.GetChild(0).gameObject;
        mat = childObj.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (globalObj.GetComponent<GlobalManager>().logSelected == gameObject.name)
        {
            // highlight object in yellow
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.yellow);

            // allow log to move through other objs to speed up
            childObj.GetComponent<Collider>().enabled = false;
            childObj.GetComponent<Rigidbody>().useGravity = false;

            if (Input.GetAxisRaw("Roll") < 0)
            {
                gameObject.transform.Translate(-thrustSpeed, 0, 0);
            }
            else if (Input.GetAxisRaw("Roll") > 0)
            {
                gameObject.transform.Translate(thrustSpeed, 0, 0);
            }

            if (Input.GetAxisRaw("Shift") < 0)
            {
                gameObject.transform.Translate(0, 0, -thrustSpeed);
            }
            else if (Input.GetAxisRaw("Shift") > 0)
            {
                gameObject.transform.Translate(0, 0, thrustSpeed);
            }

            if (Input.GetAxisRaw("Rotate") > 0)
            {
                // turn on collider and gravity to avoid log tilting into the ground
                childObj.GetComponent<Collider>().enabled = true;
                //childObj.GetComponent<Rigidbody>().useGravity = true;

                Vector3 position = childObj.GetComponent<Renderer>().bounds.center; // can't use transform.position b/c it's the transform's pivot position, not center of mass
                gameObject.transform.RotateAround(position, Vector3.up, turnSpeed);
            }
        }
        else
        {
            childObj.GetComponent<Collider>().enabled = true;
            childObj.GetComponent<Rigidbody>().useGravity = true;
            mat.DisableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f, 0.0f));
        }
    }
}
