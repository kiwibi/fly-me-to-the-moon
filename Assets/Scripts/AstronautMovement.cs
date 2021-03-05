using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautMovement : MonoBehaviour
{
    private enum rotationDir
    {
        LEFT,
        RIGHT
    }

    [Header("Astronauts")]
    public GameObject astronaut1;
    public GameObject astronaut2;

    [Header("arrow variables")]
    public GameObject arrowAnchor_;
    public float orbitSpeed_;
    public float orbitDistance_;
    public float pushforce_;

    private float orbit_;

    private Vector3 tmpPos_;
    private Vector3 releaseDir_;

    private Rigidbody2D rb1_;
    private Rigidbody2D rb2_;

    private GameObject currentAstronaut_;

    private Transform arrowChild_;

    private rotationDir currentDir = rotationDir.LEFT;

    private LineRenderer lr_;

    void Start()
    {
        lr_ = GetComponent<LineRenderer>();
        rb1_ = astronaut1.GetComponent<Rigidbody2D>();
        rb2_ = astronaut2.GetComponent<Rigidbody2D>();
        arrowChild_ = arrowAnchor_.transform.GetChild(0);
        currentAstronaut_ = astronaut1;
        tmpPos_ = Vector3.zero;
        orbit_ = (Mathf.PI / 2) * 3;
    }

    void Update()
    {
        UpdateArrowPos(currentAstronaut_.transform);
        RotateArrow(currentDir);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Push(currentDir);
        }
        lr_.SetPosition(0, currentAstronaut_.transform.position);
        lr_.SetPosition(1, arrowChild_.position);
    }

    void RotateArrow(rotationDir dir)
    {
        switch(dir)
        {
            case rotationDir.LEFT:
                orbit_ -= orbitSpeed_ * Time.deltaTime / 10;
                tmpPos_.x = currentAstronaut_.transform.position.x + Mathf.Cos(orbit_) * orbitDistance_;
                tmpPos_.y = currentAstronaut_.transform.position.y + Mathf.Sin(orbit_) * orbitDistance_;
                tmpPos_.z = transform.position.z;
                arrowChild_.position = tmpPos_;
                break;
            case rotationDir.RIGHT:
                orbit_ += orbitSpeed_ * Time.deltaTime / 10;
                tmpPos_.x = currentAstronaut_.transform.position.x + Mathf.Cos(orbit_) * orbitDistance_;
                tmpPos_.y = currentAstronaut_.transform.position.y + Mathf.Sin(orbit_) * orbitDistance_;
                tmpPos_.z = arrowChild_.position.z;
                arrowChild_.position = tmpPos_;
                break;
        }
    }
    private void Push(rotationDir dir)
    {
        float magnitude = 0;
        switch (dir)
        {
            case rotationDir.LEFT:
                releaseDir_ = arrowChild_.position - currentAstronaut_.transform.position;
                magnitude = releaseDir_.magnitude;
                releaseDir_ = releaseDir_ / magnitude;
                currentAstronaut_.GetComponent<Rigidbody2D>().AddForce(releaseDir_ * pushforce_ * orbitSpeed_);
                break;
            case rotationDir.RIGHT:
                releaseDir_ = Vector2.Perpendicular(currentAstronaut_.transform.position - arrowChild_.position);
                magnitude = releaseDir_.magnitude;
                releaseDir_ = releaseDir_ / magnitude;
                releaseDir_ *= -1;
                currentAstronaut_.GetComponent<Rigidbody2D>().AddForce(releaseDir_ * pushforce_ * orbitSpeed_);
                break;
        }
        ChangeAstronaut();
    }

    private void ChangeAstronaut()
    {
        if(currentAstronaut_.name == astronaut1.name)
        {
            currentAstronaut_ = astronaut2;
        }
        else
        {
            currentAstronaut_ = astronaut1;
        }
    }

    private void UpdateArrowPos(Transform newPos)
    {
        arrowAnchor_.transform.position = newPos.position;
    }
}
