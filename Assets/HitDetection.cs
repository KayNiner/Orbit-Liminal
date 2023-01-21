using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [SerializeField]
    GameObject coneDetection;
    Collider coneCollider;
    float timer;
    AudioSource correctSound;
    bool isPassed;
    // Start is called before the first frame update
    void Start()
    {
        correctSound = GetComponent<AudioSource>();
        coneDetection = GetComponentInChildren<GameObject>();
        coneCollider = GetComponentInChildren<Collider>();
        correctSound = GetComponent<AudioSource>();
        timer = 0;
        isPassed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Collided Timer: " + timer);

        if (timer > 5 && isPassed == false)
        {
            correctSound.Play();
            Debug.Log("Next Level");
            isPassed = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        timer =+ 1*Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        timer += 1 * Time.deltaTime;
    }

    void OnTriggerExit(Collider other)
    {
        timer = 0;
        isPassed=false;
    }
}
