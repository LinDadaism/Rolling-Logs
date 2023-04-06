using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogScript : MonoBehaviour
{
/*    public Vector3 thrust;
    public Quaternion heading;*/
/*    public float turnSpeed;
    public float thrustSpeed;*/

    private GameObject globalObj;
    private GameObject parentObj;
    private Material mat;

    // Use this for initialization
    void Start()
    {
//        turnSpeed = .5f; thrustSpeed = 0.2f;
        globalObj = GameObject.Find("GlobalObject");
        parentObj = gameObject.transform.parent.gameObject;
/*        mat = gameObject.GetComponent<Renderer>().material;
*/
        /*        // travel straight in the X-axis
                thrust.z = 40.0f;
                // do not passively decelerate
                GetComponent<Rigidbody>().drag = 0;
                // set the direction it will travel in
                GetComponent<Rigidbody>().MoveRotation(heading);
                // apply thrust once, no need to apply it again since
                // it will not decelerate
                GetComponent<Rigidbody>().AddRelativeForce(thrust);*/
    }

/*    void FixedUpdate()
    {
        // force thruster
        if (global.GetComponent<GlobalManager>().logSelected == gameObject.name)
        {
            // highlight object in yellow
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.yellow);

            gameObject.GetComponent<Collider>().enabled = false; // to be able to move through objects
            if (Input.GetAxisRaw("Roll") < 0)
            {
                //gameObject.transform.Translate(0, 0, thrustSpeed); // relative to local coord
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().AddRelativeForce(0, 0, thrustSpeed);
                GetComponent<Rigidbody>().drag = thrustSpeed/2;
                GetComponent<Rigidbody>().useGravity = true;
            }
            else if (Input.GetAxisRaw("Roll") > 0)
            {
                //gameObject.transform.Translate(0, 0, -thrustSpeed);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().AddRelativeForce(0, 0, -thrustSpeed);
                GetComponent<Rigidbody>().drag = -thrustSpeed / 2;
                GetComponent<Rigidbody>().useGravity = true;
            }

            if (Input.GetAxisRaw("Shift") < 0)
            {
                //gameObject.transform.Translate(0, thrustSpeed, 0);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().AddRelativeForce(0, thrustSpeed, 0);
                GetComponent<Rigidbody>().drag = thrustSpeed / 2;
                GetComponent<Rigidbody>().useGravity = true;
            }
            else if (Input.GetAxisRaw("Shift") > 0)
            {
                //gameObject.transform.Translate(0, -thrustSpeed, 0);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().AddRelativeForce(0, -thrustSpeed, 0);
                GetComponent<Rigidbody>().drag = -thrustSpeed / 2;
                GetComponent<Rigidbody>().useGravity = true;
            }

            if (Input.GetAxisRaw("Rotate") > 0)
            {
                gameObject.transform.Rotate(turnSpeed, 0, 0);

            }
        }
        else
        {
            gameObject.GetComponent<Collider>().enabled = true;
            mat.DisableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f, 0.0f));
        }
    }
*/
    // Update is called once per frame
 /*   void Update()
    {
        if (globalObj.GetComponent<GlobalManager>().logSelected == parentObj.name)
        {
            // highlight object in yellow
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.yellow);

            gameObject.GetComponent<Collider>().enabled = false; // to be able to move through objects
            gameObject.GetComponent<Rigidbody>().useGravity = false;

            if (Input.GetAxisRaw("Roll") < 0)
            {
                gameObject.transform.parent.Translate(-thrustSpeed, 0, 0);
            }
            else if (Input.GetAxisRaw("Roll") > 0)
            {
                gameObject.transform.parent.Translate(thrustSpeed, 0, 0);
            }

            if (Input.GetAxisRaw("Shift") < 0)
            {
                gameObject.transform.parent.Translate(0, 0, -thrustSpeed);
            }
            else if (Input.GetAxisRaw("Shift") > 0)
            {
                gameObject.transform.parent.Translate(0, 0, thrustSpeed);
            }

            if (Input.GetAxisRaw("Rotate") > 0)
            {
                gameObject.transform.parent.Rotate(0, turnSpeed, 0);
            }
        }
        else
        {
            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            mat.DisableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", new Color(1.0f, 1.0f, 1.0f, 0.0f));
        }
    }*/

    void OnMouseDown()
    {
        // update GlobalObject's logSelected tracker
        GlobalManager m = globalObj.GetComponent<GlobalManager>();
        m.logSelected = parentObj.name;
    }

    void OnCollisionEnter(Collision collision)
    {
        // the Collision contains a lot of info, but it’s the colliding
        // object we’re most interested in.
        Collider collider = collision.collider;
        if (collider.CompareTag("Animal"))
        {
            Animal animal = collider.gameObject.GetComponent<Animal>();
            // let the other object handle its own death throes
            animal.Die();
            // Destroy the Bullet which collided with the Asteroid
            // Destroy(gameObject);
        }
        else if (collider.CompareTag("Supply"))
        {
            WaterSupply bucket = collider.gameObject.GetComponent<WaterSupply>();
            bucket.Die();
        }
    }
}
