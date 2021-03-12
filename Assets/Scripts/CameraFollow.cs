using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject[] astronauts_;
    private Vector3 smoothedPosition_;

    public float speed_;

    void Start()
    {
        astronauts_ = GameObject.FindGameObjectsWithTag("Astronaut");
    }

    void Update()
    {
        float interpolation = speed_ * Time.deltaTime;

        Vector3 position = this.transform.position;
        Vector3 desiredPos = FindMiddle();
        float dst = Vector3.Distance(transform.position, desiredPos);
        position.y = Mathf.Lerp(this.transform.position.y, desiredPos.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, desiredPos.x, interpolation);

        this.transform.position = FindMiddle();
    }
    private Vector3 FindMiddle()
    {
        Vector3 tmp = Vector3.zero;
        tmp.x = ((astronauts_[0].transform.position.x + astronauts_[1].transform.position.x) / 2) + 7;
        tmp.y = (astronauts_[0].transform.position.y + astronauts_[1].transform.position.y) / 2;
        tmp.z = -10;
        return tmp;
    }

    /*
     TODO:
        feel:
            static / screen flicker
        soundManager
        musicplayer
        
     */
}
