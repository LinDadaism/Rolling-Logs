using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartBoard : MonoBehaviour
{
    // some public variables that can be used to tune the Board’s movement 
    public Vector3 forceVector;
    public float rotationSpeed;
    public float rotation;
    public GameObject log; // the GameObject to spawn
    public int pointValue;
    public int winWaterLevel;

    private GameObject globalObj;
    private GameObject uiWin;
    private GameObject uiLose;
    private GameObject uiButtons;
    private GameObject uiNext;

    // Start is called before the first frame update
    void Start()
    {
        forceVector.z = 10.0f;
        rotationSpeed = 2.0f;
        rotation = -90; // sync with initial board orientation
        pointValue = 5;
        winWaterLevel = 80;

        uiWin = GameObject.Find("WinTextBg");
        uiLose = GameObject.Find("LoseTextBg");
        uiButtons = GameObject.Find("GameOverOp");
        uiNext = GameObject.Find("AdvanceButton");
        uiWin.SetActive(false);
        uiLose.SetActive(false);
        uiButtons.SetActive(false);
        uiNext.SetActive(false);

        globalObj = GameObject.Find("GlobalObject");
    }

    /* forced changes to rigid body physics parameters should be done through the FixedUpdate() 
        method, not the Update() method 
     */
    void FixedUpdate()
    {
        // force thruster 
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().AddRelativeForce(forceVector);
            GetComponent<Rigidbody>().drag = 1.0f;
            GetComponent<Rigidbody>().useGravity = true;

        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().AddRelativeForce(-forceVector);
            GetComponent<Rigidbody>().drag = 1.0f;
            GetComponent<Rigidbody>().useGravity = true;
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            rotation += rotationSpeed;
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            GetComponent<Rigidbody>().MoveRotation(rot);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            rotation -= rotationSpeed;
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            GetComponent<Rigidbody>().MoveRotation(rot);
            //GetComponent<Rigidbody>().Rotate(0, -2.0f, 0.0f ); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        int gameState = globalObj.GetComponent<GlobalManager>().gameState;
        // check for lose state when the cart is entirely on the ground
        if (gameObject.transform.position.y < -0.439f && gameState != 2)
        {
            // pause the game scene
            Time.timeScale = 0;
            globalObj.GetComponent<GlobalManager>().gameState = 2;

            uiLose.SetActive(true);
            uiWin.SetActive(false);
            uiButtons.SetActive(true);

            // play sound effect
            AudioSource[] sounds = globalObj.GetComponents<AudioSource>();
            foreach (AudioSource sound in sounds)
            {
                if (sound.clip.name == "sad_fail")
                {
                    sound.Play();
                }
            }
        }

        /*        if (Input.GetButtonDown("Fire1"))
                {
                    // Debug.Log("Fire! " + rotation);
        *//*            we don’t want to spawn a Bullet inside our ship, so some
                            Simple trigonometry is done here to spawn the bullet
                            at the tip of where the ship is pointed.*//*

                    Vector3 spawnPos = gameObject.transform.position;
                    // spawnPos.x += 1.5f * Mathf.Cos(rotation * Mathf.PI / 180);
                    // spawnPos.z += 1.ff * Mathf.Sin(rotation * Mathf.PI / 180);
                    // instantiate the Bullet
                    GameObject obj = Instantiate(log, spawnPos, Quaternion.identity) as GameObject;
                    // get the Bullet Script Component of the new Bullet instance
                    WoodLogScript w = obj.GetComponent<WoodLogScript>();
                    // set the direction the Bullet will travel in
                    Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
                    w.heading = rot;
                }*/

    }

    void OnMouseDown()
    {
        GlobalManager m = globalObj.GetComponent<GlobalManager>();
        m.logSelected = "empty";
    }

    void OnCollisionEnter(Collision collision)
    {
        GlobalManager m = globalObj.GetComponent<GlobalManager>();
        Collider collider = collision.collider;
        // win state
        if (collider.CompareTag("Destination") && m.waterLevel >= winWaterLevel)
        {
            Debug.Log("win");

            uiWin.SetActive(true);
            uiLose.SetActive(false);
            uiButtons.SetActive(true);
            uiNext.SetActive(true);

            // play sound effect
            AudioSource[] sounds = globalObj.GetComponents<AudioSource>();
            foreach (AudioSource sound in sounds)
            {
                if (sound.clip.name == "win")
                {
                    sound.Play();
                }
            }

            // pause the game scene
            Time.timeScale = 0;
        }
        // penalty state
        else if (collider.CompareTag("Ground"))
        {
            m.waterLevel -= pointValue;
        }
        // scoring by knocking out animals
        else if (collider.CompareTag("Animal"))
        {
            Animal animal = collider.gameObject.GetComponent<Animal>();
            // let the other object handle its own death throes
            animal.Die();
        }
        // filling in water by collecting water buckets
        else if (collider.CompareTag("Supply"))
        {
            WaterSupply bucket = collider.gameObject.GetComponent<WaterSupply>();
            bucket.Die();
        }
    }
}
