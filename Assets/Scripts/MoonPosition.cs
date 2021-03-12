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
        tmp.x = transform.position.x;
        tmp.y = camera_.position.y;
        tmp.z = 0;
        return tmp;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = GetNewPos();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Astronaut")
        {
            Debug.Log("WIN");
        }
    }
}
