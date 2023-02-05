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
    [SerializeField] AudioSource correctSound;
    public bool isPassed;

    [Header("LineDraw Script")]
    [SerializeField]
    Draw_Beam lineDrawer;

    [Header("LaserAudio")]
    [SerializeField] AudioSource laserStart, laserStay;

    // Start is called before the first frame update
    void Start()
    {
        coneCollider = GetComponent<Collider>();
        timer = 0;
        isPassed = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log("Collided Timer: " + timer);
        

        if (timer > 15)
        {
           if(isPassed == false)
           {
                correctSound.Play();
                Debug.Log("Next Level");
                isPassed = true;
           }
           else
           {
                timer = 0;
                isPassed = false;
           }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        timer =+ 1 * Time.deltaTime;
        lineDrawer.drawLine();
        laserStart.Play();
        lineDrawer.particleTrail.GetComponent<ParticleSystem>().Play();
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log (other.name);
        timer += 1 * Time.deltaTime;
        lineDrawer.drawLine();
        //laserStay.Play();
        
    }

    void OnTriggerExit(Collider other)
    {
        timer = 0;
        isPassed=false;
        lineDrawer.endLine();
        laserStay.Stop();
        lineDrawer.particleTrail.GetComponent<ParticleSystem>().Stop() ;
    }
}
