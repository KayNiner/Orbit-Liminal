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
    public enum Stages { TUTORIAL1, TUTORIAL2, TUTORIAL3, TUTORIAL4, STAGE1, STAGE2, STAGE3, STAGE4, STAGE5, STAGE6 } 
    public Stages currentStage;

    float slerpSpeed;
    float rotationAngle;
    public GameObject hitChecker;
    public HitDetection hitDetection;
    public PlayerControlled playerControl;
    public outerRingColour outerRingColour;

    public ParticleSystem particle;
    public ParticleSystem endSceneParticle;
    //public ShaderSystem emission;

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
        currentStage = Stages.TUTORIAL1;
        StartCoroutine(StagingMachine());
        playerControl.enabled = false;
        intensityValue = starRendMat.GetFloat("_intensityAdjust");
        //noiseAdjust = laserBeamMat.GetFloat("noiseAmount");
        radialBar.fillAmount = 0;
        starRendMat = level1.GetComponent<Renderer>().material;
        starColour = Color.red;
        particle = gameObject.GetComponent<ParticleSystem>();
        //particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //intensityValueUI.text = currentStage.ToString();
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
                //hitDetection.isPassed = false;
                //hitDetection.timer = 0;
                starRendMat = level1.GetComponent<Renderer>().material;
                starColour = Color.red;
                //ringColour = Color.red;
                particle = level1.GetComponent<ParticleSystem>();
                particle.Play();;
                currentStage = Stages.STAGE2;

            }
            else if (currentStage == Stages.STAGE2)
            {
                StartCoroutine("lightUpStar");
                //hitDetection.isPassed = false;
                //hitDetection.timer = 1;
                starRendMat = level2.GetComponent<Renderer>().material;
                starColour = Color.yellow;
                //ringColour = Color.yellow;
                particle = level2.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE3;
            }
            else if (currentStage == Stages.STAGE3)
            {
                StartCoroutine("lightUpStar");
                // hitDetection.isPassed = false;
                //hitDetection.timer = 2;
                starRendMat = level3.GetComponent<Renderer>().material;
                starColour = Color.blue;
                //ringColour = Color.blue;
                particle = level3.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE4;

            }
            else if (currentStage == Stages.STAGE4)
            {

                StartCoroutine("lightUpStar");
                //hitDetection.isPassed = false;
                //hitDetection.timer = 3;
                starRendMat = level4.GetComponent<Renderer>().material;
                starColour = Color.green;
                //ringColour = Color.green;
                particle = level4.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE5;
            }
            else if (currentStage == Stages.STAGE5)
            {
                StartCoroutine("lightUpStar");
                //hitDetection.isPassed = false;
                // hitDetection.timer = 4;
                starRendMat = level5.GetComponent<Renderer>().material;
                starColour = Color.cyan;
                //ringColour = Color.cyan;
                particle = level5.GetComponent<ParticleSystem>();
                particle.Play();
                currentStage = Stages.STAGE6;
            }
            else if (currentStage == Stages.STAGE6)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level6.GetComponent<Renderer>().material;
                starColour = Color.white;
                //ringColour = Color.white;
                particle = level6.GetComponent<ParticleSystem>();
                particle.Play();
                Debug.Log("Experience Over");
                Invoke("endScene", 5.0f);
            }
            else if (currentStage == Stages.TUTORIAL1)
            {
                currentStage = Stages.TUTORIAL4;
            }
            else if (currentStage == Stages.TUTORIAL2)
			{
                currentStage = Stages.TUTORIAL3;                
			}
            else if (currentStage == Stages.TUTORIAL3)
            {
                currentStage = Stages.TUTORIAL4;
            }
            else if (currentStage == Stages.TUTORIAL4)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level1.GetComponent<Renderer>().material;
                starColour = Color.red;
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
            //Debug.Log("starColour: " +starColour.ToString());
            starRendMat.SetFloat("_intensityAdjust", Mathf.Lerp(intensityValue, 0f,0.01f));
           // Debug.Log("intensity value "+intensityValue.ToString());
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

    //IEnumerator laserBeamAdjust()
    //{
    //need something here to adjust noice level in the laser beam material to focus the laser
    //}
    IEnumerator StagingMachine()
    {
        while (true)
        {
            
            yield return StartCoroutine(currentStage.ToString());
        }
    }

	#region Tutorials
	IEnumerator TUTORIAL1()
    {

		//Start of the Experience
		//Wait for a few seconds before starting.
		outerRing.transform.Rotate(0, 0,180);
        yield return new WaitForSeconds(1f);
        expereinceStartAudio.Play();
		yield return new WaitForSeconds(1f);
        
        Debug.Log("Start of Experience - Tutorial 1");
        //Tutorial Settings
        //Looping/ Stageing
        while (currentStage == Stages.TUTORIAL1)
        {
            Debug.Log("Tutorial 1 - LOOPING");
			outerRing.transform.Rotate(new Vector3(0,0, rotationAngle)* Time.deltaTime);
			yield return new WaitForEndOfFrame();
            
        }    
        yield return null;
    } 
    IEnumerator TUTORIAL2()
    {
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        outerRing.transform.Rotate(0, 0, 180);
        fadeToClearInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        while (currentStage ==Stages.TUTORIAL2)
        {
            Debug.Log("Tutorial 2 - LOOPING");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    IEnumerator TUTORIAL3()
    {
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        outerRing.transform.rotation = Quaternion.Euler(0, 0,120);
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
    IEnumerator TUTORIAL4()
    {
        fadeToBlackInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        outerRing.transform.rotation = Quaternion.Euler(0, 0, -75);
        fadeToClearInTimer(1f);
        yield return new WaitForSeconds(1.5f);
        rotationAngle = -10;
        while (currentStage == Stages.TUTORIAL4)
        {
            Debug.Log("Tutorial 4 - LOOPING");
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
                outerRingColour.materials[0].SetColor("_emission", Color.Lerp(outerRingColour.materials[0].GetColor("_emission"), Color.cyan, (Time.deltaTime + 0.05f) / hitDetection.requiredTime));
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
