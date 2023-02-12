    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Input;
using Liminal.SDK.VR.Input;
using UnityEngine.UI;

public class PlayerControlled : MonoBehaviour
{
    Transform cylinder;
    float horizontalInput,verticalInput;
    float moveSpeed = 2f;
    float rightHorizontalInput, rightVerticalInput;
    public float rHoriSpeed, rVertSpeed, lHoriSpeed, lVertSpeed;
    Rigidbody rb;
    [SerializeField]
    float slerpSpeed;
    [SerializeField]
    Transform tgt;
    Quaternion rotationGoal;
    Vector3 direction;
	public bool hasXInput, hasYInput;
    public float floatDirection;


    /*[Header("UI")]
    [SerializeField]
    Text uiOutPut;*/

    // Start is called before the first frame update
    void Start()
    {
        horizontalInput = Input.GetAxis("Oculus_GearVR_LThumbstick");
        verticalInput = Input.GetAxis("Oculus_GearVR_LThumbstickY");
        rightHorizontalInput = Input.GetAxis("Oculus_GearVR_RThumbstickX");
        rightVerticalInput = Input.GetAxis("Oculus_GearVR_RThumbstickY");
        string[] joyName = Input.GetJoystickNames();
        rb = GetComponent<Rigidbody>();
        hasXInput = false;
        hasYInput = false;
    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log(rVertSpeed);
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
        else if (hasXInput == false && hasYInput == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, tgt.rotation, 0.01f*Time.deltaTime);
            
        }
        else 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, slerpSpeed);
        }
        #region commented out codes
        //transform.localRotation = Quaternion.Euler(0, 0, (rVertSpeed-rHoriSpeed)*2000*Time.deltaTime);

        //MOVEMENT
        //Quaternion target = Quaternion.Euler(rVertSpeed, 0, -rHoriSpeed);


        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, new Vector3(rHoriSpeed,rVertSpeed, 0)), slerpSpeed);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, new Vector3(rHoriSpeed,rVertSpeed,0)), slerpSpeed);
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(lHoriSpeed, lVertSpeed, 0));

        //Dampen toward the target roation
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, slerpSpeed);

        #endregion
    }
}
