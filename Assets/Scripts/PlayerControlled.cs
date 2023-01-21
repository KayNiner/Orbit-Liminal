using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Input;
using Liminal.SDK.VR.Input;

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
    float fixedRotation = 0;


    // Start is called before the first frame update
    void Start()
    {
        horizontalInput = Input.GetAxis("Oculus_GearVR_LThumbstick");
        verticalInput = Input.GetAxis("Oculus_GearVR_LThumbstickY");
        rightHorizontalInput = Input.GetAxis("Oculus_GearVR_RThumbstickX");
        rightVerticalInput = Input.GetAxis("Oculus_GearVR_RThumbstickY");
        string[] joyName = Input.GetJoystickNames();
        rb = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);

    }

    // Update is called once per frame
    void Update()
    {
        //float lHoriSpeed = Input.GetAxis("Oculus_GearVR_LThumbstick") * Time.deltaTime;
        //float lVertSpeed = Input.GetAxis("Oculus_GearVR_LThumbstickY") * Time.deltaTime;
        float rHoriSpeed = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickHorizontal");
        float rVertSpeed = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical");
        //Debug.Log(lHoriSpeed);
        //Debug.Log(lVertSpeed);
        Debug.Log("Y input: "+ rVertSpeed);
        Debug.Log("X input: "+ rHoriSpeed);
         
        //transform.localRotation = Quaternion.Euler(0, 0, (rVertSpeed-rHoriSpeed)*2000*Time.deltaTime);

        //MOVEMENT
        
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, new Vector3(rHoriSpeed,rVertSpeed, 0)), slerpSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, new Vector3(rHoriSpeed,rVertSpeed,0)), slerpSpeed);
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(lHoriSpeed, lVertSpeed, 0));
    }
}
