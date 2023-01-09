using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.Platform.Experimental.App.Experiences;
using Liminal.SDK.Core;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR;

public class RotationControll : MonoBehaviour
{
    public float horizontalInput, verticalInput;
    public float horiSpeed, vertiSpeed;

    // Start is called before the first frame update
    void Start()
    {
        horizontalInput = Input.GetAxis("Oculus_GearVR_RThumbstickX");
        verticalInput = Input.GetAxis("Oculus_GearVR_RThumbstickY");
    }

    // Update is called once per frame
    void Update()
    {
       horiSpeed = horizontalInput * Time.deltaTime;
       vertiSpeed = verticalInput * Time.deltaTime;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(horiSpeed, vertiSpeed, 0));
    }
}
