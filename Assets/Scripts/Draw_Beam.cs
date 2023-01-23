using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw_Beam : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform Destination;

    public float lineDrawSpeed = 6f;
    public float speed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetWidth(0.3f, 0.3f);

        dist = Vector3.Distance(origin.position, Destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, origin.position);
        if (counter < dist)
        {
            counter += .1f / lineDrawSpeed;
       
            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin.position;
            Vector3 pointB = Destination.position;

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            lineRenderer.SetPosition(1, pointAlongLine);
        }
        lineRenderer.SetPosition(1, Destination.position);
    }
}
