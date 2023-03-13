using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Xml.Serialization;
using Liminal.Core.Fader;
using Liminal.SDK.Core;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public enum Stages {TUTORIAL3, STAGE1, STAGE2, STAGE3, STAGE4, STAGE5, STAGE6, STAGE7 } 
    public Stages currentStage;

    float slerpSpeed;
    float rotationAngle;
    public GameObject hitChecker;
    public HitDetection hitDetection;
    public PlayerControlled playerControl;
    public outerRingColour outerRingColour;
    public innerRingColour innerRingColour;
    public LineRenderer lineRenderer;

    public ParticleSystem particle;
    public ParticleSystem endSceneParticle;

    [Header("Stage 1")]
    [SerializeField]
    GameObject level1;
    [SerializeField]
    AudioSource stage1Audio;
    [Header("Star 2")]
    [SerializeField]
    GameObject level2;
    [SerializeField]
    AudioSource stage2Audio;
    [Header("Star 3")]
    [SerializeField]
    GameObject level3;
    [SerializeField]
    AudioSource stage3Audio;
    [Header("Star 4")]
    [SerializeField]
    GameObject level4;
    [SerializeField]
    AudioSource stage4Audio;
    [Header("Star 5")]
    [SerializeField]
    GameObject level5;
    [SerializeField]
    AudioSource stage5Audio;
    [Header("Star 6")]
    [SerializeField]
    GameObject level6;
    [SerializeField]
    AudioSource stage6Audio;

    [Header("LineDraw Script")]
    [SerializeField]
    Draw_Beam lineDrawer;

    [Header("MISC")]
    Material starRendMat, laserBeamMat;
    float intensityValue = -1;
    Color starColour, ringColour;
    float t;
    public bool hasInput;


    [SerializeField]
    GameObject outerRing, innerRing;


    [SerializeField]
    bool stagePass;
    Text UIText;
    CanvasGroup canvasGroup;
    [SerializeField]
    Image radialBar;
    float maxBarAmount = 1.0f;
    float barElaspeTIme;

    [SerializeField]
    AudioSource expereinceStartAudio;

    void Awake()
    {
        hitDetection = hitChecker.GetComponent<HitDetection>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStage = Stages.TUTORIAL3;
        StartCoroutine(StagingMachine());
        playerControl.enabled = false;
        radialBar.fillAmount = 0;
        starRendMat = level1.GetComponent<Renderer>().material;
        intensityValue = starRendMat.GetFloat("_intensityAdjust");
        particle = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        barElaspeTIme = hitDetection.timer;
        radialBar.fillAmount = barElaspeTIme / hitDetection.requiredTime;
        if (radialBar.fillAmount >= maxBarAmount)
        {
            radialBar.fillAmount = maxBarAmount;
        }
        if(hitDetection.isPassed == true)
        {
            if (currentStage == Stages.STAGE1)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level1.GetComponent<Renderer>().material;
                starColour = Color.red;
                particle = level1.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE2;
            }
            else if (currentStage == Stages.STAGE2)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level2.GetComponent<Renderer>().material;
                starColour = Color.yellow;
                particle = level2.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE3;
            }
            else if (currentStage == Stages.STAGE3)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level3.GetComponent<Renderer>().material;
                starColour = Color.blue;
                particle = level3.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE4;
            }
            else if (currentStage == Stages.STAGE4)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level4.GetComponent<Renderer>().material;
                starColour = Color.green;
                particle = level4.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE5;
            }
            else if (currentStage == Stages.STAGE5)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level5.GetComponent<Renderer>().material;
                starColour = Color.magenta;
                particle = level5.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE6;
            }
            else if (currentStage == Stages.STAGE6)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level6.GetComponent<Renderer>().material;
                starColour = Color.red;
                particle = level6.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE7;
            }
            else if (currentStage == Stages.STAGE7)
            {
                Invoke("endScene", 5.0f);
            }
            else if (currentStage == Stages.TUTORIAL3)
            {
                currentStage = Stages.STAGE1;
            }
            else if (currentStage == Stages.TUTORIAL3)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level1.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE1;
            }
        } 
    }

    IEnumerator lightUpStar()
    {
        t = 0;
        while (t < 2)
        {
            t += Time.deltaTime;
            starRendMat.SetColor("_starColorAdjust", starColour);
            starRendMat.SetFloat("_intensityAdjust", Mathf.Lerp(intensityValue, 0f,0.01f));
            intensityValue = starRendMat.GetFloat("_intensityAdjust");
            yield return null ;
        }

        yield break;
    }

    void endScene()
    {
        endSceneParticle.Play();
        ExperienceApp.End();
    }

    void fadeToBlackInTimer(float t)
    {
        var fader = ScreenFader.Instance;
        fader.FadeToBlack(t);
    }
    void fadeToClearInTimer(float t)
    {
        var fader = ScreenFader.Instance;
        fader.FadeToClear(t);
    }

    IEnumerator StagingMachine()
    {
        while (true)
        {
            
            yield return StartCoroutine(currentStage.ToString());
        }
    }

	#region Tutorials
    IEnumerator TUTORIAL3()
    {
        fadeToBlackInTimer(1f);
        outerRing.transform.Rotate(0, 0, 80);
        yield return new WaitForSeconds(1.5f);
        expereinceStartAudio.Play();
        fadeToClearInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        lineDrawer.lineRenderer.material.SetColor("_beamColour", Color.cyan);
        rotationAngle = 5;
        while (currentStage == Stages.TUTORIAL3)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

	#endregion

	#region Staging
	IEnumerator STAGE1()
    {
        radialBar.enabled = false;
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        stage1Audio.Play();
        outerRing.transform.rotation = Quaternion.Euler(0, 0, 0);
        innerRingColour.materials[0].SetColor("_emission", Color.red);
        lineDrawer.lineRenderer.material.SetColor("_beamColour", Color.red);
        fadeToClearInTimer(1f);
        slerpSpeed = 10f;
        rotationAngle = -5;
        hitDetection.requiredTime = 14f; //adjusts the time the stage takes to clear
        yield return new WaitForSeconds(2f);

        //Loop while in Stage1
        while(currentStage == Stages.STAGE1)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.red, (Time.deltaTime+0.05f)/hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }      
    }
    IEnumerator STAGE2()
    {
        float t = 0;
        Vector3 rot2 = innerRing.transform.rotation.eulerAngles;
        rot2 = new Vector3(rot2.x,rot2.y, rot2.z+90);
        Quaternion targetRot2 = Quaternion.Euler(rot2);
        Vector3 rot = outerRing.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x,rot.y,rot.z-179);
        Quaternion targetRot = Quaternion.Euler(rot);
        while (t < 3)
        {
            t += Time.deltaTime;
            outerRing.transform.rotation = Quaternion.Lerp(outerRing.transform.rotation, targetRot, 0.015f);
            innerRing.transform.rotation = Quaternion.Lerp(innerRing.transform.rotation, targetRot2, 0.015f);
            lineDrawer.lineRenderer.material.SetColor("_beamColour", Color.yellow);
            outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"),outerRingColour.originalColour, 0.005f));
            innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.yellow, 0.1f));
            yield return new WaitForEndOfFrame();
        }
        hitDetection.enabled = true;
        stage2Audio.Play();
        slerpSpeed = 10f;
        rotationAngle = -10;
        hitDetection.requiredTime = 17f; //adjusts the time the stage takes to clear
        yield return new WaitForSeconds(0.5f);

        while (currentStage == Stages.STAGE2)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.yellow, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }
        
    }
    IEnumerator STAGE3()
    {
        float t = 0;
        Vector3 rot2 = innerRing.transform.rotation.eulerAngles;
        rot2 = new Vector3(rot2.x, rot2.y, rot2.z - 90);
        Quaternion targetRot2 = Quaternion.Euler(rot2);
        Vector3 rot = outerRing.transform.rotation.eulerAngles;;
        rot = new Vector3(rot.x, rot.y, rot.z + 179);
        Quaternion targetRot = Quaternion.Euler(rot);
        while (t < 3)
        {
            t += Time.deltaTime;
            outerRing.transform.rotation = Quaternion.Lerp(outerRing.transform.rotation, targetRot, 0.015f);
            innerRing.transform.rotation = Quaternion.Lerp(innerRing.transform.rotation, targetRot2, 0.015f);
            lineDrawer.lineRenderer.material.SetColor("_beamColour", Color.blue);
            outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), outerRingColour.originalColour, 0.005f));
            innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.blue, 0.1f));
            yield return new WaitForEndOfFrame();
        }
        stage3Audio.Play();
        slerpSpeed = 10f;
        rotationAngle = 13;
        hitDetection.requiredTime = 22f; //adjusts the time the stage takes to clear
        yield return new WaitForSeconds(0.5f);

        while (currentStage == Stages.STAGE3)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.blue, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }
        
    }
    IEnumerator STAGE4()
    {
        float t = 0;
        Vector3 rot2 = innerRing.transform.rotation.eulerAngles;
        rot2 = new Vector3(rot2.x, rot2.y, rot2.z + 90);
        Quaternion targetRot2 = Quaternion.Euler(rot2);
        Vector3 rot = outerRing.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y, rot.z - 179);
        Quaternion targetRot = Quaternion.Euler(rot);
        while (t < 3)
        {
            t += Time.deltaTime;
            outerRing.transform.rotation = Quaternion.Lerp(outerRing.transform.rotation, targetRot, 0.015f);
            innerRing.transform.rotation = Quaternion.Lerp(innerRing.transform.rotation, targetRot2, 0.015f);
            lineDrawer.lineRenderer.material.SetColor("_beamColour", Color.green);
            outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), outerRingColour.originalColour, 0.005f));
            innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.green, 0.1f));
            yield return new WaitForEndOfFrame();
        }
        stage4Audio.Play();
        fadeToClearInTimer(1f);
        slerpSpeed = 10f;
        rotationAngle = -14;
        hitDetection.requiredTime = 26f; //adjusts the time the stage takes to clear
        yield return new WaitForSeconds(0.5f);

        while (currentStage == Stages.STAGE4)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.green, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }
        
    }
    IEnumerator STAGE5()
    {
        float t = 0;
        Vector3 rot2 = innerRing.transform.rotation.eulerAngles;
        rot2 = new Vector3(rot2.x, rot2.y, rot2.z - 90);
        Quaternion targetRot2 = Quaternion.Euler(rot2);
        Vector3 rot = outerRing.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y, rot.z + 179);
        Quaternion targetRot = Quaternion.Euler(rot);
        while (t < 3)
        {
            t += Time.deltaTime;
            outerRing.transform.rotation = Quaternion.Lerp(outerRing.transform.rotation, targetRot, 0.015f);
            innerRing.transform.rotation = Quaternion.Lerp(innerRing.transform.rotation, targetRot2, 0.015f);
            lineDrawer.lineRenderer.material.SetColor("_beamColour", Color.magenta);
            outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), outerRingColour.originalColour, 0.005f));
            innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.magenta, 0.1f));
            yield return new WaitForEndOfFrame();
        }
        stage5Audio.Play();
        slerpSpeed = 10f;
        rotationAngle = -10;
        hitDetection.requiredTime = 29f; //adjusts the time the stage takes to clear
        yield return new WaitForSeconds(0.5f);

        while (currentStage == Stages.STAGE5)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.magenta, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }

    }
    IEnumerator STAGE6()
    {
        float t = 0;
        Vector3 rot2 = innerRing.transform.rotation.eulerAngles;
        rot2 = new Vector3(rot2.x, rot2.y, rot2.z - 90);
        Quaternion targetRot2 = Quaternion.Euler(rot2);
        Vector3 rot = outerRing.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y, rot.z + 180);
        Quaternion targetRot = Quaternion.Euler(rot);
        while (t < 3)
        {
            t += Time.deltaTime;
            outerRing.transform.rotation = Quaternion.Lerp(outerRing.transform.rotation, targetRot, 0.015f);
            innerRing.transform.rotation = Quaternion.Lerp(innerRing.transform.rotation, targetRot2, 0.015f);
            lineDrawer.lineRenderer.material.SetColor("_beamColour", Color.red);
            outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), outerRingColour.originalColour, 0.005f));
            innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.red, 0.1f));
            yield return new WaitForEndOfFrame();
        }
        stage6Audio.Play();
        slerpSpeed = 10f;
        rotationAngle = 15;
        hitDetection.requiredTime = 33f; //adjusts the time the stage takes to clear
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage6
        while (currentStage == Stages.STAGE6)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.red, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }
    }

    IEnumerator STAGE7() // end scene, handles all the turning off of stuff
    {
        playerControl.enabled = false;
        hitDetection.enabled = false;
        lineRenderer.enabled = false;
        hitDetection.LaserOn.Stop();
        hitDetection.LaserOn.mute = !hitDetection.LaserBreak.mute;
        hitDetection.laserStart.Stop();
        hitDetection.laserStart.mute = !hitDetection.laserStart.mute;
        hitDetection.LaserBreak.Stop();
        hitDetection.LaserBreak.mute = !hitDetection.LaserBreak.mute;
        hitDetection.laserStay.Stop();
        hitDetection.laserStay.mute = !hitDetection.laserStay.mute;
        slerpSpeed = 10f;
        rotationAngle = 30f;
        yield return new WaitForSeconds(0.5f);

        while (currentStage == Stages.STAGE7)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
            innerRing.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }


    public IEnumerator switchingLevel()
    {
        t = 0;
        while (t < 1)
        {
            playerControl.enabled = false;
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * (Mathf.Lerp(0.35f, 0.1f,0.25f* Time.deltaTime)));
            innerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * (Mathf.Lerp(0.35f, 0.1f, 0.25f * Time.deltaTime)));
            t += Time.deltaTime;
            playerControl.enabled = true;
            yield return new WaitForEndOfFrame(); 
        }
        yield break;
    }

    #endregion
}
