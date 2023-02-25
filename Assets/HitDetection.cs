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
    public float requiredTime;
    [SerializeField] AudioSource correctSound;
    public bool isPassed;
    public bool isOverlapped;

    [Header("LineDraw Script")]
    [SerializeField]
    Draw_Beam lineDrawer;

    [Header("LaserAudio")]
    [SerializeField] AudioSource laserStart, laserStay;

    [Header("Outer Ring Color")]
    [SerializeField]
    outerRingColour ringColourScript;

    [Header("Inner Ring Color")]
    [SerializeField]
    innerRingColour outerRingColour; //ringColourScript;

    [SerializeField]
    StageManager stgManager;

    // Start is called before the first frame update
    void Start()
    {
        coneCollider = GetComponent<Collider>();
        timer = 0;
        requiredTime = 15f;
        isPassed = false;
        isOverlapped = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log("Collided Timer: " + timer);
        

        if (timer > requiredTime)
        {
           if(isPassed == false)
           {
                correctSound.Play();
                //Debug.Log("Next Level");
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
        isOverlapped = true;
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log (other.name);
        timer += 1 * Time.deltaTime;
        lineDrawer.drawLine();
        isOverlapped = true;
        //laserStay.Play();
        lineDrawer.lineRenderer.material.SetColor("_beamColour", Color.Lerp(lineDrawer.beamColour, new Color(255,255,0),Time.deltaTime/requiredTime));
    }

    void OnTriggerExit(Collider other)
    {
        timerDecrease();
        isPassed=false;
        lineDrawer.endLine();
        laserStay.Stop();
        lineDrawer.particleTrail.GetComponent<ParticleSystem>().Stop() ;
        lineDrawer.lineRenderer.material.SetColor("_beamColour", Color.white);
        isOverlapped = false;
    }

    void timerDecrease()
    {
        while (timer >0)
        {
            timer -= 1;
        }
        if (timer <=0)
        {
            timer = 0;
        }    
    }
}
