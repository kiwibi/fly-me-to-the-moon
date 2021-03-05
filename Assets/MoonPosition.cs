using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonPosition : MonoBehaviour
{
    Transform camera_;
    void Start()
    {
        camera_ = Camera.main.transform;
    }

    private Vector3 GetNewPos()
    {
        Vector3 tmp = Vector3.zero;
        tmp.x = camera_.position.x + 7;
        tmp.y = camera_.position.y;
        tmp.z = 0;
        return tmp;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = GetNewPos();
    }
}
