using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using Liminal.Core.Fader;
using Liminal.SDK.Core;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public enum Stages {TUTORIAL3, STAGE1, STAGE2, STAGE3, STAGE4, STAGE5, STAGE6 } 
    public Stages currentStage;

    float slerpSpeed;
    float rotationAngle;
    public GameObject hitChecker;
    public HitDetection hitDetection;
    public PlayerControlled playerControl;
    public outerRingColour outerRingColour;
    public innerRingColour innerRingColour;

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

    [Header("MISC")]
    Material starRendMat, laserBeamMat;
    float intensityValue = -1;
    Color starColour, ringColour;
    float t;
    public bool hasInput;


    [SerializeField]
    GameObject outerRing;


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
                starColour = Color.white;
                particle = level6.GetComponent<ParticleSystem>();
                particle.Play();
                Debug.Log("Experience Over");
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
        rotationAngle = 5;
        while (currentStage == Stages.TUTORIAL3)
        {
            Debug.Log("Tutorial 3 - LOOPING");
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
        //Entering Stage 1
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        stage1Audio.Play();
        outerRing.transform.rotation = Quaternion.Euler(0, 0, 0);
        fadeToClearInTimer(1f);
        Debug.Log("Start Stage 1");
        slerpSpeed = 10f;
        rotationAngle = -5;
        yield return new WaitForSeconds(2f);

        //Loop while in Stage1
        while(currentStage == Stages.STAGE1)
        {
            Debug.Log("Looping Stage 1");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.red, (Time.deltaTime+0.05f)/hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.red, (Time.deltaTime+0.05f)/hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }      
    }
    IEnumerator STAGE2()
    {
        //Entering Stage 2
        yield return new WaitForSeconds(5f);
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        hitDetection.enabled = true;
        stage2Audio.Play();
        outerRing.transform.rotation = Quaternion.Euler(0, 0, 180);
        outerRingColour.materials[0].SetColor("_emission", outerRingColour.originalColour);
        innerRingColour.materials[0].SetColor("_emission", innerRingColour.originalColour);
        fadeToClearInTimer(1f);
        slerpSpeed = 10f;
        rotationAngle = -10;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage1
        while (currentStage == Stages.STAGE2)
        {
            Debug.Log("Stage2");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.yellow, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.yellow, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }
        
    }
    IEnumerator STAGE3()
    {
        //Entering Stage 3
        yield return new WaitForSeconds(5f);
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        stage3Audio.Play();
        outerRing.transform.rotation = Quaternion.Euler(0, 0, 90);
        outerRingColour.materials[0].SetColor("_emission", outerRingColour.originalColour);
        innerRingColour.materials[0].SetColor("_emission", innerRingColour.originalColour);
        fadeToClearInTimer(1f);
        slerpSpeed = 10f;
        rotationAngle = 13;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage1
        while (currentStage == Stages.STAGE3)
        {
            Debug.Log("Stage 3");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.blue, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.blue, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }
        
    }
    IEnumerator STAGE4()
    {
        //Entering Stage 4
        yield return new WaitForSeconds(5f);
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        stage4Audio.Play();
        outerRing.transform.rotation = Quaternion.Euler(0, 0, -90);
        outerRingColour.materials[0].SetColor("_emission", outerRingColour.originalColour);
        innerRingColour.materials[0].SetColor("_emission", innerRingColour.originalColour);
        fadeToClearInTimer(1f);
        slerpSpeed = 10f;
        rotationAngle = -14;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage1
        while (currentStage == Stages.STAGE4)
        {
            Debug.Log("Stage 4");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.green, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.green, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }
        
    }
    IEnumerator STAGE5()
    {
        //Entering Stage 5
        yield return new WaitForSeconds(5f);
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        stage5Audio.Play();
        outerRing.transform.Rotate(0, 0, 180);
        outerRingColour.materials[0].SetColor("_emission", outerRingColour.originalColour);
        innerRingColour.materials[0].SetColor("_emission", innerRingColour.originalColour);
        fadeToClearInTimer(1f);
        slerpSpeed = 10f;
        rotationAngle = -15;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage1
        while (currentStage == Stages.STAGE5)
        {
            Debug.Log("Stage 5");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.magenta, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.magenta, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }

    }
    IEnumerator STAGE6()
    {
        //Entering Stage 6
        yield return new WaitForSeconds(5f);
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        stage6Audio.Play();
        outerRing.transform.Rotate(0, 0, 180);
        outerRingColour.materials[0].SetColor("_emission", outerRingColour.originalColour);
        innerRingColour.materials[0].SetColor("_emission", innerRingColour.originalColour);
        fadeToClearInTimer(1f);
        slerpSpeed = 10f;
        rotationAngle = 15;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage6
        while (currentStage == Stages.STAGE6)
        {
            Debug.Log("Stage 6");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (hitDetection.isOverlapped == true)
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.white, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.white, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
            else
            {
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
                innerRingColour.materials[0].SetColor("_emission", Color.Lerp(innerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
            }
        }

    }
    
    public IEnumerator switchingLevel()
    {
        t = 0;
        while (t < 1)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * (Mathf.Lerp(0.35f, 0.1f,0.25f* Time.deltaTime)));
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    #endregion
}
