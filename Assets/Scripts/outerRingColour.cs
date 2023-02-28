using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outerRingColour : MonoBehaviour
{
    [SerializeField]
    public GameObject outerRingObject;
    [SerializeField]
    public List<Material> materials = new List<Material>();
    public Color originalColour, currentColour;
    [SerializeField]
    public StageManager stageManager;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Material m in outerRingObject.GetComponentInChildren<Renderer>().materials)
        {
            materials.Add(m);
        }

        originalColour = materials[0].GetColor("_emission");

    }
    // Update is called once per frame
    void Update()
    {
        currentColour = materials[0].GetColor("_emission");

    }
}
