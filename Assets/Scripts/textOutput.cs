using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Liminal.SDK.VR.UI;

public class textOutput : MonoBehaviour
{
    public Text inputText;
    float inputCounterX, inputCounterY;
    PlayerControlled playController;
    // Start is called before the first frame update
    void Start()
    {
        inputText = GetComponent<Text>();
        inputCounterX = playController.rHoriSpeed;
        inputCounterY = playController.rVertSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        inputText.text = inputCounterX.ToString()+ "  " + inputCounterY.ToString();
    }
}
