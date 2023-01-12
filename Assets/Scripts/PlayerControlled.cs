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
    

    // Start is called before the first frame update
    void Start()
    {
        horizontalInput = Input.GetAxis("Oculus_GearVR_LThumbstick");
        verticalInput = Input.GetAxis("Oculus_GearVR_LThumbstickY");
        rightHorizontalInput = Input.GetAxis("Oculus_GearVR_RThumbstickX");
        rightVerticalInput = Input.GetAxis("Oculus_GearVR_RThumbstickY");
        string[] joyName = Input.GetJoystickNames();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float lHoriSpeed = Input.GetAxis("Oculus_GearVR_LThumbstick") * Time.deltaTime;
        float lVertSpeed = Input.GetAxis("Oculus_GearVR_LThumbstickY") * Time.deltaTime;
        float rHoriSpeed = Input.GetAxis("Oculus_GearVR_RThumbstickX") * Time.deltaTime;
        float rVertSpeed = Input.GetAxis("Oculus_GearVR_RThumbstickY") * Time.deltaTime;
        Debug.Log(lHoriSpeed);
        Debug.Log(lVertSpeed);
        Debug.Log(rVertSpeed);
        Debug.Log(rHoriSpeed);
        
        if(horizontalInput !=0 || verticalInput !=0)
        {
            Debug.Log(horizontalInput);
            Debug.Log(verticalInput);
        }
        if(rightVerticalInput !=0 || rightHorizontalInput !=0)
        {
            Debug.Log(rightVerticalInput);
            Debug.Log(rightHorizontalInput);
        }
        //transform.localRotation = Quaternion.Euler(0, 0, (rVertSpeed-rHoriSpeed)*2000*Time.deltaTime);

        //MOVEMENT
        transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(rHoriSpeed,rVertSpeed, 0));
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(lHoriSpeed, lVertSpeed, 0));
    }
}
