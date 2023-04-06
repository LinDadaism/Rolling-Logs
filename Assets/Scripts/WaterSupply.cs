using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSupply : MonoBehaviour
{
    public GameObject deathExplosion;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        // Debug.Log("Bucket dead");

        /* all of Shuriken's particle effects by default use the convention of Z being upwards, 
and XY being the horizontal plane. as a result, since we are looking down the Y axis, we rotate 
the particle system so that it flys in the right way.
 */     Instantiate(deathExplosion, gameObject.transform.position, Quaternion.identity);

        GameObject obj = GameObject.Find("GlobalObject");
        GlobalManager m = obj.GetComponent<GlobalManager>();
        m.waterLevel += pointValue;
        m.numProPack++;

        // play sound effect
        AudioSource[] sounds = obj.GetComponents<AudioSource>();
        foreach (AudioSource sound in sounds)
        {
            if (sound.clip.name == "magic")
            {
                sound.Play();
            }
        }

        // Destroy removes the gameObject from the scene and
        // marks it for garbage collection
        Destroy(gameObject);
    }
}
