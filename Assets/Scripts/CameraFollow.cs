using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject[] astronauts_;
    private Vector3 smoothedPosition_;

    public float smoothSpeed_;

    void Start()
    {
        astronauts_ = GameObject.FindGameObjectsWithTag("Astronaut");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCamera();
    }

    private Vector3 FindMiddle()
    {
        Vector3 tmp = Vector3.zero;
        tmp.x = (astronauts_[0].transform.position.x + astronauts_[1].transform.position.x) / 2;
        tmp.y = (astronauts_[0].transform.position.y + astronauts_[1].transform.position.y) / 2;
        tmp.z = -10;
        return tmp;
    }

    private void MoveCamera()
    {
        smoothedPosition_ = Vector3.Lerp(transform.position, FindMiddle(), smoothSpeed_);
        transform.position = smoothedPosition_;
    }
}
