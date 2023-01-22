using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum Stages{ STAGE1, STAGE2,STAGE3,STAGE4}
    public Stages currentStage;

    public float slerpSpeed;
    public float rotationAngle;
    public GameObject hitChecker;
    public HitDetection hitDetection;

    [Header("Star Setting")]
    [SerializeField]
    GameObject level1, level2, level3, level4;
    Light light1, light2, light3, light4;
    

    [SerializeField]
    GameObject outerRing;

    [SerializeField]
    bool stagePass;

    void Awake()
    {
        hitDetection = hitChecker.GetComponent<HitDetection>();
        light1 = level1.GetComponent<Light>();
        light2 = level2.GetComponent<Light>();
        light3 = level3.GetComponent<Light>();
        light4 = level4.GetComponent<Light>();
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
            light1.intensity = Mathf.Lerp(0, 0.5f, 1);
        }
        if (currentStage == Stages.STAGE2 && hitDetection.isPassed == true)
        {
            currentStage = Stages.STAGE3;
            hitDetection.isPassed = false;
            hitDetection.timer = 0;
            light2.intensity = Mathf.Lerp(0, 0.5f, 1);
        }
        if (currentStage == Stages.STAGE3 && hitDetection.isPassed == true)
        {
            currentStage = Stages.STAGE4;
            hitDetection.isPassed = false;
            hitDetection.timer = 0;
            light3.intensity = Mathf.Lerp(0, 0.5f, 1);
        }
        if (currentStage == Stages.STAGE4 && hitDetection.isPassed == true)
        {
            Debug.Log("Experience Over");
            light4.intensity = Mathf.Lerp(0, 0.5f, 1);
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
        slerpSpeed = 0.03f;
        rotationAngle = 20;
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
        slerpSpeed = 0.08f;
        rotationAngle = -40;

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
        slerpSpeed = 0.09f;
        rotationAngle = 60;

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
        slerpSpeed = 0.09f;
        rotationAngle = -70;

        //Loop while in Stage1
        while (currentStage == Stages.STAGE4)
        {
            Debug.Log("Srage 4");
            outerRing.transform.Rotate(new Vector3(0, rotationAngle, 0) * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
    }

    #endregion
}
