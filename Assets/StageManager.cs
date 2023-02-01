using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.Core;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum Stages{ STAGE1, STAGE2,STAGE3,STAGE4, STAGE5, STAGE6}
    public Stages currentStage;

    public float slerpSpeed;
    public float rotationAngle;
    public GameObject hitChecker;
    public HitDetection hitDetection;

    [Header("Star Setting")]
    [SerializeField]
    GameObject level1, level2, level3, level4, level5, level6;

    [SerializeField]
    Shader star1, star2, star3, star4, star5, star6;
    
    [SerializeField]
    GameObject outerRing;

    [SerializeField]
    bool stagePass;

    void Awake()
    {
        hitDetection = hitChecker.GetComponent<HitDetection>();
        //star1 = level1.GetComponent<Star_Shader.intensityAdjust>();
        //star2 = level2.GetComponent<Star_Shader.intensityAdjust>();
        //star3 = level3.GetComponent<Star_Shader.intensityAdjust>();
        //star4 = level4.GetComponent<Star_Shader.intensityAdjust>();
        //star5 = level5.GetComponent<Star_Shader.intensityAdjust>();
        //star6 = level6.GetComponent<Star_Shader.intensityAdjust>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStage = Stages.STAGE1;
        StartCoroutine(StagingMachine());
    }

    // Update is called once per frame
    void Update()
    {
        if(currentStage==Stages.STAGE1 && hitDetection.isPassed == true)
        {
            currentStage = Stages.STAGE2;
            hitDetection.isPassed = false;
            hitDetection.timer = 0;
            //star1.intensityAdjust = Mathf.Lerp(0, 0.5f, 0);
        }
        if (currentStage == Stages.STAGE2 && hitDetection.isPassed == true)
        {
            currentStage = Stages.STAGE3;
            hitDetection.isPassed = false;
            hitDetection.timer = 1;
            //star2.intensityAdjust = Mathf.Lerp(0, 0.5f, 0);
        }
        if (currentStage == Stages.STAGE3 && hitDetection.isPassed == true)
        {
            currentStage = Stages.STAGE4;
            hitDetection.isPassed = false;
            hitDetection.timer = 2;
            //star3.intensityAdjust = Mathf.Lerp(0, 0.5f, 0);
        }
        if (currentStage == Stages.STAGE4 && hitDetection.isPassed == true)
        {
            currentStage = Stages.STAGE5;
            hitDetection.isPassed = false;
            hitDetection.timer = 3;
            //star4.intensityAdjust = Mathf.Lerp(0, 0.5f, 0);
        }
        if (currentStage == Stages.STAGE5 && hitDetection.isPassed == true)
        {
            currentStage = Stages.STAGE6;
            hitDetection.isPassed = false;
            hitDetection.timer = 4;
            //star5.intensityAdjust = Mathf.Lerp(0, 0.5f, 0);
        }
        if (currentStage == Stages.STAGE6 && hitDetection.isPassed == true)
        {
            Debug.Log("Experience Over");
            //star6.intensityAdjust = Mathf.Lerp(0, 0.5f, 0);
            ExperienceApp.End();
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
