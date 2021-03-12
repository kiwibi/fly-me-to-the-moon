using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float G;
    public float minDistance_;
    public Rigidbody2D rb;

    private Rigidbody2D[] astronauts_ = new Rigidbody2D[2];
    private void Start()
    {
        var astronauts = GameObject.FindGameObjectsWithTag("Astronauts");
        for (int i = 0; i < 2; i++)
        {
            astronauts_[i] = astronauts[i].GetComponent<Rigidbody2D>();
        }
    }
    private void FixedUpdate()
    {
        foreach (Rigidbody2D attractor in astronauts_)
        {
                Attract(attractor);
        }
    }
    void Attract(Rigidbody2D rbToAttract)
    {
        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;
        if (distance > minDistance_)
        {
            rbToAttract.AddForce(force);
        }
    }
}
