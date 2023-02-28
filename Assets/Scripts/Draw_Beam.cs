using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw_Beam : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform Destination;

    public float lineDrawSpeed = 2f;
    public float speed = 1f;

    [Header("Particle Trails")]
    [SerializeField]
    public ParticleSystem particleTrail;
    [SerializeField]
    float distParticleToOrigin, distParticleToDestination;

    public Color beamColour;
    



    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth= 0.3f;
        particleTrail.transform.position = origin.position;
        dist = Vector3.Distance(origin.position, Destination.position);
        lineRenderer.material.SetColor("_beamColour", beamColour);
    }

    // Update is called once per frame
    void Update()
    {
        distParticleToOrigin = Vector3.Distance(particleTrail.transform.position, origin.position);
        distParticleToDestination = Vector3.Distance(particleTrail.transform.position, Destination.position);
        beamColour = lineRenderer.material.GetColor("_beamColour");
    }
    public void drawLine()
    {
        Debug.Log("DrawlineActivate");
        lineRenderer.SetPosition(0, origin.position) ;
        particleOnMove();
        counter = 0.1f;
        dist = Vector3.Distance(origin.position, Destination.position);
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
    public void endLine()
    {
        Debug.Log("EndlineActivate");
        lineRenderer.SetPosition(0, Destination.position);
        
        counter = 0.1f;

        dist = Vector3.Distance(origin.position, Destination.position);
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

    private void particleOnMove()
    {
        particleTrail.transform.position = origin.position;
        lineRenderer.SetPosition(0, origin.position);
        

        if (distParticleToOrigin < 0.1f)
        {
            
            particleTrail.transform.position = Destination.position;
            
        }
        else if (distParticleToDestination<0.1f)
        {
            particleTrail.transform.position = origin.position;
            
        }
       
    }

}
