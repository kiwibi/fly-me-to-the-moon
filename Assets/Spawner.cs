using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Tier1")]
    public GameObject[] Tier1Objects_;
    public float Tier1MinSpawnDelay_;
    public float Tier1MaxSpawnDelay_;
    [Header("Tier2")]
    public GameObject[] Tier2Objects__;
    public float Tier2MinSpawnDelay_;
    public float Tier2MaxSpawnDelay_;
    [Header("BothTiers")]
    public float RampUp_;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
