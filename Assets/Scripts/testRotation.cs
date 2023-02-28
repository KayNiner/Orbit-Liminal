using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRotation : MonoBehaviour
{
    public GameObject objectToRotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objectToRotate.transform.Rotate(new Vector3(20, 0,0)*Time.deltaTime);
        
    }
}
