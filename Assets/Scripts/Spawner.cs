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
    public GameObject[] Tier2Objects_;
    public float Tier2MinSpawnDelay_;
    public float Tier2MaxSpawnDelay_;
    [Header("BothTiers")]
    public float RampUpMultiplier_;
    public float DistanceToRampUp_;

    private GameObject[] spawnPoints_;
    private float tier1SpawnTime_;
    private float tier1Timer_;
    private float tier2SpawnTime_;
    private float tier2Timer_;
    void Start()
    {
        spawnPoints_ = GameObject.FindGameObjectsWithTag("SpawnPoint");
        tier1Timer_ = 0;
        tier2Timer_ = 0;
    }

    void Update()
    {
        Tier1Update();
        Tier2Update();
    }

    private int GetRandomSpawn()
    {
        int tmp = Random.Range(0, spawnPoints_.Length);
        return tmp;
    }

    private void Tier1Update()
    {
        tier1Timer_ += Time.deltaTime;
        if(tier1Timer_ >=tier1SpawnTime_)
        {
            SpawnTier1();
            tier1Timer_ = 0;
            GetNewTier1Timer();
        }
    }

    private void GetNewTier1Timer()
    {
        tier1SpawnTime_ = Random.Range(Tier1MinSpawnDelay_, Tier1MaxSpawnDelay_ + 1);

    }

    private void SpawnTier1()
    {
        Debug.Log("spawn");
        var rot = Random.rotation;
        rot.x = 0;
        rot.y = 0;
        var pos = spawnPoints_[GetRandomSpawn()].transform.position;
        pos.z = 0;
        var trash = Instantiate(Tier1Objects_[Random.Range(0, Tier1Objects_.Length)], pos, rot);
        

    }

    private void Tier2Update()
    {
        tier2Timer_ += Time.deltaTime;
        if (tier1Timer_ >= tier1SpawnTime_)
        {
            SpawnTier2();
            tier2Timer_ = 0;
            GetNewTier2Timer();
        }
    }

    private void GetNewTier2Timer()
    {
        tier2SpawnTime_ = Random.Range(Tier2MinSpawnDelay_, Tier2MaxSpawnDelay_ + 1);

    }

    private void SpawnTier2()
    {
        var rot = Random.rotation;
        rot.x = 0;
        rot.y = 0;
        var pos = spawnPoints_[GetRandomSpawn()].transform.position;
        pos.z = 0;
        var trash = Instantiate(Tier1Objects_[Random.Range(0, Tier2Objects_.Length)], pos, rot);


    }
}
