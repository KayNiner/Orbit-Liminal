using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.Core;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public enum Stages { STAGE1, STAGE2, STAGE3, STAGE4, STAGE5, STAGE6 }
    public Stages currentStage;

    public float slerpSpeed;
    public float rotationAngle;
    public GameObject hitChecker;
    public HitDetection hitDetection;

    [Header("Star Setting")]
    [SerializeField]
    GameObject level1, level2, level3, level4, level5, level6;
    [SerializeField]
    Material starRendMat;
    float intensityValue = -1;
    Color starColour;
    float t;


    [SerializeField]
    GameObject outerRing;

    [SerializeField]
    bool stagePass;

    [Header("UI")]
    [SerializeField]
    Text intensityValueUI;

    void Awake()
    {
        hitDetection = hitChecker.GetComponent<HitDetection>();
        //starRend = level1.GetComponent<Renderer>();
        /*
        star1 = level1.GetComponent<Renderer>().material;
        star2 = level2.GetComponent<Renderer>().material;
        star3 = level3.GetComponent<Renderer>().material;
        star4 = level4.GetComponent<Renderer>().material;
        star5 = level5.GetComponent<Renderer>().material;
        star6 = level6.GetComponent<Renderer>().material;
        */

    }

    // Start is called before the first frame update
    void Start()
    {
        currentStage = Stages.STAGE1;
        StartCoroutine(StagingMachine());
        starRendMat = level1.GetComponent<Renderer>().material;
        intensityValue = starRendMat.GetFloat("_intensityAdjust");
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
                //hitDetection.isPassed = false;
                //hitDetection.timer = 0;
                starRendMat = level2.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE2;
                starColour = Color.red;

            }
            else if (currentStage == Stages.STAGE2)
            {
                StartCoroutine("lightUpStar");
                hitDetection.isPassed = false;
                hitDetection.timer = 1;
                starRendMat = level3.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE3;
                starColour = Color.yellow;

            }
            else if (currentStage == Stages.STAGE3)
            {
                StartCoroutine("lightUpStar");
                hitDetection.isPassed = false;
                hitDetection.timer = 2;
                starRendMat = level4.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE4;
                starColour = Color.blue;

            }
            else if (currentStage == Stages.STAGE4)
            {

                StartCoroutine("lightUpStar");
                currentStage = Stages.STAGE5;
                hitDetection.isPassed = false;
                hitDetection.timer = 3;
                starRendMat = level5.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE5;
                starColour = Color.green;
            }
            else if (currentStage == Stages.STAGE5)
            {
                StartCoroutine("lightUpStar");
                hitDetection.isPassed = false;
                hitDetection.timer = 4;
                starRendMat = level6.GetComponent<Renderer>().material;
                currentStage = Stages.STAGE6;
                starColour = Color.cyan;
            }
            else if (currentStage == Stages.STAGE6)
            {
                StartCoroutine("lightUpStar");
                starRendMat = level1.GetComponent<Renderer>().material;
                starColour = Color.white;
                Debug.Log("Experience Over");
                ExperienceApp.End();
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
    }

    IEnumerator StagingMachine()
    {
        while (true)
        {
            
            yield return StartCoroutine(currentStage.ToString());
        }
    }

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
            outerRing.transform.Rotate(new Vector3(0, rotationAngle, 0) * Time.deltaTime);
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
            outerRing.transform.Rotate(new Vector3(0, rotationAngle, 0) * Time.deltaTime);
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
            outerRing.transform.Rotate(new Vector3(0, rotationAngle, 0) * Time.deltaTime);
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
            outerRing.transform.Rotate(new Vector3(0, rotationAngle, 0) * Time.deltaTime);
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
            outerRing.transform.Rotate(new Vector3(0, rotationAngle, 0) * Time.deltaTime);
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
            outerRing.transform.Rotate(new Vector3(0, rotationAngle, 0) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

    }

    #endregion
}
