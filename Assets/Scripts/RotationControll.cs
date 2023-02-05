using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Input;
using Liminal.SDK.VR.Input;

public class RotationControll : MonoBehaviour
{
    Transform cylinder;
    float pcHorizontalInput, pcVerticalInput;
    float moveSpeed = 2f;
    float rightHorizontalInput, rightVerticalInput;
    public float rHoriSpeed, rVertSpeed, lHoriSpeed, lVertSpeed;
    Rigidbody rb;
    bool hasInput;
    float lerpSpeed = 0.7f ;
    Quaternion targetRotation;


    // Start is called before the first frame update
    void Start()
    {
        pcHorizontalInput = Input.GetAxis("Horizontal");
        pcVerticalInput = Input.GetAxis("Vertical");
        //rightHorizontalInput = Input.GetAxis("Oculus_GearVR_RThumbstickX");
        //rightVerticalInput = Input.GetAxis("Oculus_GearVR_RThumbstickY");
        string[] joyName = Input.GetJoystickNames();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float pclHoriSpeed = Input.GetAxis("Horizontal") * Time.deltaTime;
        float pclVertSpeed = Input.GetAxis("Vertical") * Time.deltaTime;
        //float rHoriSpeed = Input.GetAxis("Oculus_GearVR_RThumbstickX") * Time.deltaTime;
        //float rVertSpeed = Input.GetAxis("Oculus_GearVR_RThumbstickY") * Time.deltaTime;
        //Debug.Log(pclHoriSpeed);
        //Debug.Log(pclVertSpeed);
        //Debug.Log(rVertSpeed);
        //Debug.Log(rHoriSpeed);

        if (pcHorizontalInput != 0 || pcVerticalInput != 0)
        {
            //Debug.Log(pcHorizontalInput);
            //Debug.Log(pcVerticalInput);
            hasInput = true;
        }
        else if (rightVerticalInput != 0 || rightHorizontalInput != 0)
        {
            //Debug.Log(rightVerticalInput);
            //Debug.Log(rightHorizontalInput);
            hasInput = true;
        }
        else
        {
            hasInput = false;
        }

        //if the player has made an input, use update the target position with the thumbsticks.
        if (hasInput)
        {
            targetRotation = Quaternion.FromToRotation(Vector3.up, new Vector3(rHoriSpeed, rVertSpeed, 0));
        }

        //Lerp to the target rotation to get a smooth movement.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed);


        //transform.localRotation = Quaternion.Euler(0, 0, (rVertSpeed-rHoriSpeed)*2000*Time.deltaTime);

        //MOVEMENT


        //transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(lHoriSpeed, lVertSpeed, 0));

        /*if (pcHorizontalInput != 0 || pcVerticalInput != 0)
        {
            Debug.Log(pcHorizontalInput);
            Debug.Log(pcVerticalInput);
        }
        /*if (rightVerticalInput != 0 || rightHorizontalInput != 0)
        {
            Debug.Log(rightVerticalInput);
            Debug.Log(rightHorizontalInput);
        }
        //transform.localRotation = Quaternion.Euler(0, 0, (rVertSpeed-rHoriSpeed)*2000*Time.deltaTime);

        //MOVEMENT
        transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(pclHoriSpeed, pclVertSpeed, 0));
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(lHoriSpeed, lVertSpeed, 0));
        */
    }
}
