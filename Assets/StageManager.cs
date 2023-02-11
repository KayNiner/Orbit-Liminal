using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Liminal.SDK.Core;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public enum Stages { TUTORIAL1, TUTORIAL2, TUTORIAL3, NEEDINPUT, STAGE1, STAGE2, STAGE3, STAGE4, STAGE5, STAGE6 } 
    public Stages currentStage;

    public float slerpSpeed;
    public float rotationAngle;
    public GameObject hitChecker;
    public HitDetection hitDetection;
    public PlayerControlled playerControl;

    [Header("Star Setting")]
    [SerializeField]
    GameObject level1, level2, level3, level4, level5, level6;
    [SerializeField]
    Material starRendMat, laserBeamMat;
    float intensityValue = -1;
    Color starColour;
    float t;
    public bool hasInput;


    [SerializeField]
    GameObject outerRing;


    [SerializeField]
    bool stagePass;

    [Header("UI")]
    [SerializeField]
    Text UIText;
    [SerializeField]
    CanvasGroup canvasGroup;



    void Awake()
    {
        hitDetection = hitChecker.GetComponent<HitDetection>();

    }

    // Start is called before the first frame update
    void Start()
    {
        currentStage = Stages.TUTORIAL1;
        StartCoroutine(StagingMachine());
        //starRendMat = GetComponent<Renderer>().material;
        //laserBeamMat = GetComponent<Renderer>().material;
        intensityValue = starRendMat.GetFloat("_intensityAdjust");
        //noiseAdjust = laserBeamMat.GetFloat("noiseAmount");
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //intensityValueUI.text = currentStage.ToString();

        if(hitDetection.isPassed == true)
        {
            if (currentStage == Stages.STAGE1)
            {
                StartCoroutine("lightUpStar");
                StartCoroutine("switchingLevel");
                //hitDetection.isPassed = false;
                //hitDetection.timer = 0;
                starRendMat = level1.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE2;
                starColour = Color.red;

            }
            else if (currentStage == Stages.STAGE2)
            {
                StartCoroutine("lightUpStar");
                StartCoroutine("switchingLevel");
                //hitDetection.isPassed = false;
                //hitDetection.timer = 1;
                starRendMat = level2.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE3;
                starColour = Color.yellow;

            }
            else if (currentStage == Stages.STAGE3)
            {
                StartCoroutine("lightUpStar");
                StartCoroutine("switchingLevel");
                // hitDetection.isPassed = false;
                //hitDetection.timer = 2;
                starRendMat = level3.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE4;
                starColour = Color.blue;

            }
            else if (currentStage == Stages.STAGE4)
            {

                StartCoroutine("lightUpStar");
                StartCoroutine("switchingLevel");
                //hitDetection.isPassed = false;
                //hitDetection.timer = 3;
                starRendMat = level4.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE5;
                starColour = Color.green;
            }
            else if (currentStage == Stages.STAGE5)
            {
                StartCoroutine("lightUpStar");
                StartCoroutine("switchingLevel");
                //hitDetection.isPassed = false;
                // hitDetection.timer = 4;
                starRendMat = level5.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE6;
                starColour = Color.cyan;
            }
            else if (currentStage == Stages.STAGE6)
            {
                StartCoroutine("lightUpStar");
                StartCoroutine("switchingLevel");
                starRendMat = level6.GetComponent<Renderer>().material;
                starColour = Color.white;
                Debug.Log("Experience Over");
                ExperienceApp.End();
            }
			else if (currentStage == Stages.TUTORIAL2)
			{
                currentStage = Stages.TUTORIAL3;
                
			}
            else if (currentStage == Stages.TUTORIAL3)
            {
                StartCoroutine("switchingLevel");
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

    //IEnumerator laserBeamAdjust()
    //{
        //need something here to adjust noice level in the laser beam material to focus the laser
    //}
    IEnumerator canvasAlphaIn()
    {
        float c = 0;
        while (c <2)
        {
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, 0.1f);
            c += Time.deltaTime;
            yield return new WaitForEndOfFrame();
		}
      yield break;
    }
    IEnumerator canvasAlphaOut()
    {
        float c = 0;
        while (c <2)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, 0.1f);
            c += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
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
        Debug.Log("Start of Experience - Tutorial 1");

        //Tutorial Settings

        //slerpSpeed = 3f;
        //rotationAngle = 10;

        //Looping/ Stageing
        while (currentStage == Stages.TUTORIAL1)
        {
            Debug.Log("Tutorial 1 - LOOPING");

            
            //UIText.text = "Welcome to Orbit";
           // StartCoroutine(canvasAlphaIn());
           // yield return new WaitForSeconds(5f);
           // StartCoroutine(canvasAlphaOut());
           // yield return new WaitForSeconds(2f);
            //UIText.text = "Use your controller <b>Rightstick</b> to rotate the inner ring.";
           // StartCoroutine(canvasAlphaIn());
           // yield return new WaitForSeconds(5f);
           // StartCoroutine(canvasAlphaOut());
           // yield return new WaitForSeconds(2f);
            //UIText.text = "Rotate the inner ring to the bottom and hold the position";
			//StartCoroutine(canvasAlphaIn());
			//yield return new WaitForSeconds(5f);
			//StartCoroutine(canvasAlphaOut());
			//yield return new WaitForSeconds(2f);
			//StartCoroutine(canvasAlphaOut());
            currentStage = Stages.TUTORIAL2;
			outerRing.transform.Rotate(new Vector3(0,0, rotationAngle)* Time.deltaTime);
			yield return new WaitForEndOfFrame();
            
        }    


        yield return null;
    }
    IEnumerator TUTORIAL2()
    {
        hitDetection.requiredTime = 5f;

        while(currentStage == Stages.TUTORIAL2)
        {
            //UIText.text = "Hold the position for a period of time to complete the connection";
            StartCoroutine(canvasAlphaIn());
			yield return new WaitForSeconds(2f);
			StartCoroutine(canvasAlphaOut());
			yield return new WaitForSeconds(2f);

		}

    }
    
    IEnumerator TUTORIAL3()
    {
        hitDetection.requiredTime = 20f;
        slerpSpeed = 10f;
        rotationAngle = 6;
        yield return new WaitForSeconds(0.5f);
		//UIText.text = "Now match the inner ring and outer ring rotation";
		//StartCoroutine(canvasAlphaIn());
		//yield return new WaitForSeconds(2f);
		//StartCoroutine(canvasAlphaOut());

		//Looping Tutorial 3
		while (currentStage == Stages.TUTORIAL3)
        {
            Debug.Log("Tutorial 3");
			outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle)* Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    } 
    
	#endregion

	#region Staging
	IEnumerator STAGE1()
    {
        //Entering Stage 1
        Debug.Log("Start Stage 1");
        slerpSpeed = 10f;
        rotationAngle = 10;
        yield return new WaitForSeconds(2f);

        //Loop while in Stage1
        while(currentStage == Stages.STAGE1)
        {
            Debug.Log("Looping Stage 1");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        
    }
    IEnumerator STAGE2()
    {
        //Entering Stage 2
        slerpSpeed = 10f;
        rotationAngle = -12;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage1
        while (currentStage == Stages.STAGE2)
        {
            Debug.Log("Stage2");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
    }
    IEnumerator STAGE3()
    {
        //Entering Stage 3
        slerpSpeed = 10f;
        rotationAngle = 13;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage1
        while (currentStage == Stages.STAGE3)
        {
            Debug.Log("Stage 3");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
    }
    IEnumerator STAGE4()
    {
        //Entering Stage 4
        slerpSpeed = 10f;
        rotationAngle = -14;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage1
        while (currentStage == Stages.STAGE4)
        {
            Debug.Log("Stage 4");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
    }
    IEnumerator STAGE5()
    {
        //Entering Stage 5
        slerpSpeed = 10f;
        rotationAngle = -10;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage1
        while (currentStage == Stages.STAGE5)
        {
            Debug.Log("Stage 5");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

    }
    IEnumerator STAGE6()
    {
        //Entering Stage 6
        slerpSpeed = 10f;
        rotationAngle = 15;
        yield return new WaitForSeconds(0.5f);

        //Loop while in Stage1
        while (currentStage == Stages.STAGE6)
        {
            Debug.Log("Stage 6");
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator switchingLevel()
    {
        t = 0;
        while (t < 1.2)
        {
            outerRing.transform.Rotate(new Vector3(0, 0, rotationAngle) * (Mathf.Lerp(0.35f, 0.1f,0.25f* Time.deltaTime)));
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    #endregion
}
