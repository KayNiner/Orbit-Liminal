using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Input;
using Liminal.SDK.VR.Input;
using UnityEngine.UI;

public class PlayerControlled : MonoBehaviour
{
    float horizontalInput,verticalInput;
    public float rHoriSpeed, rVertSpeed, lHoriSpeed, lVertSpeed;
    [SerializeField]
    float slerpSpeed;
    [SerializeField]
    Transform tgt, starter, destination;
    Quaternion rotationGoal;
    Vector3 direction;
	public bool hasXInput, hasYInput;
    public float distanceBetweenConnector;

    // Start is called before the first frame update
    void Start()
    {

        hasXInput = false;
        hasYInput = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenConnector = Vector3.Distance(starter.position, destination.position);
        float rHoriSpeed = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickHorizontal");
        float rVertSpeed = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical");
        if (rHoriSpeed !=0  )
        {
            hasXInput=true;
        }
        else
        {
            hasXInput = false;
        }
        if (rVertSpeed !=0)
        {
            hasYInput=true;
        }    
        else
        {
            hasYInput = false;
        }
    
        Quaternion target = Quaternion.FromToRotation(Vector3.up, new Vector3(rHoriSpeed, rVertSpeed, 0));
        Quaternion target2 = Quaternion.Euler(0, 0, 180);

        if (rVertSpeed >= -1f && rVertSpeed < -1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target2, slerpSpeed);
        }
        else if (hasXInput == false && rVertSpeed < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target2, slerpSpeed);
        }
        else if (hasXInput == false && hasYInput == false &&distanceBetweenConnector >=2.5f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, tgt.rotation, 0.0001f);
            
        }
        else if (hasXInput == false && hasYInput == false && distanceBetweenConnector < 2.5f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, tgt.rotation, 0.005f);
        }
        else if (hasXInput == false && hasYInput == false )
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation, 0.2f) ;
        }
        else 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, slerpSpeed);
        }
    }
}
