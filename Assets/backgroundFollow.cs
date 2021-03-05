using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundFollow : MonoBehaviour
{
    public float speed_;

    private Transform camera_;

    void Awake()
    {
        camera_ = Camera.main.transform;
    }

    private void Update()
    {
        float interpolation = speed_ * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, camera_.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, camera_.position.x, interpolation);

        this.transform.position = position;
    }
}
