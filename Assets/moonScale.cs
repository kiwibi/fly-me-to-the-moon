using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonScale : MonoBehaviour
{
    [Header("Moon")]
    [Tooltip("how much should be traveled for it to grow by growthFactor")]
    public float distanceToGrow_;
    [Tooltip("amount it grows for every distanceToGrow")]
    public float growthFactor_;

    private GameObject[] astronauts_;

    private float distTraveled_;
    private float oldX_;
    void Start()
    {
        astronauts_ = GameObject.FindGameObjectsWithTag("Astronaut");
        distTraveled_ = 0;
        oldX_ = FindMiddle();
    }

    void Update()
    {
        distTraveled_ = FindMiddle() - oldX_;
        if(distTraveled_ >= distanceToGrow_)
        {
            ScaleUp();
            distTraveled_ = distTraveled_ - distanceToGrow_;
        }
    }

    private float FindMiddle()
    {
        float tmp = 0;
        tmp = ((astronauts_[0].transform.position.x + astronauts_[1].transform.position.x) / 2);
        return tmp;
    }

    private void ScaleUp()
    {
        Vector3 tmp = new Vector3(transform.localScale.x + growthFactor_, transform.localScale.y + growthFactor_);
        transform.localScale = tmp;
    }
}
