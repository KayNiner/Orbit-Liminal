using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [SerializeField]
    GameObject coneDetection;
    [SerializeField]
    Collider coneCollider;
    public float timer;
    AudioSource correctSound;
    public bool isPassed;
    // Start is called before the first frame update
    void Start()
    {
        coneCollider = GetComponent<Collider>();
        correctSound = GetComponent<AudioSource>();
        timer = 0;
        isPassed = false;
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log("Collided Timer: " + timer);
        

        if (timer > 4 && isPassed == false)
        {
            correctSound.Play();
            Debug.Log("Next Level");
            isPassed = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        timer =+ 1*Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log (other.name);
        timer += 1 * Time.deltaTime;
    }

    void OnTriggerExit(Collider other)
    {
        timer = 0;
        isPassed=false;
    }
}
