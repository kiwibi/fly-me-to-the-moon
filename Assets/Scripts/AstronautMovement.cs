using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautMovement : MonoBehaviour
{
    private enum rotationDir
    {
        RIGHT,
        LEFT
        
    }

    [Header("Astronauts")]
    public GameObject astronaut1;
    public GameObject astronaut2;
    public GameObject pssh_;

    public float astronautSpeedCap_;
    public float fakeOut_;

    [Header("arrow variables")]
    public GameObject arrowAnchor_;
    public float orbitSpeed_;
    public float orbitDistance_;
    public float pushforce_;
    public float arrowDelay_ = 0.25f;
    public Color[] arrowColors_;

    private float orbit_;
    private float fakeTimer_;
    private float fakeRange_;
    
    private bool onDelay_ = false;

    private Vector3 tmpPos_;
    private Vector3 releaseDir_ = Vector3.zero;

    private GameObject currentAstronaut_;

    private Transform arrowChild_;

    private SpriteRenderer arrowSprite_;

    private rotationDir currentDir = rotationDir.LEFT;


    void Start()
    {
        arrowChild_ = arrowAnchor_.transform.GetChild(0);
        currentAstronaut_ = astronaut1;
        tmpPos_ = Vector3.zero;
        orbit_ = (Mathf.PI / 2) * 3;
        fakeRange_ = Random.Range(0, fakeOut_ + 1);
        fakeTimer_ = 0;
        arrowSprite_ = arrowChild_.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateArrowPos(currentAstronaut_.transform);
        RotateArrow(currentDir);
        if (Input.GetKeyDown(KeyCode.Space) && onDelay_ == false)
        {
            Push();
            if(Random.Range(0, 8) == 2)
            {
                ChangeArrowDir();
            }
        }
    }

    private void FixedUpdate()
    {
        capSpeed();
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
    private void Push()
    {
        float magnitude = 0;
        if (currentAstronaut_.name == "Astronaut 2")
        {
            if(fakeTimer_ >= fakeRange_)
            {
                releaseDir_ = arrowChild_.position - currentAstronaut_.transform.position;
                releaseDir_ *= -1;
                changeArrowColor();
                fakeTimer_ = 0;
                fakeRange_ = Random.Range(0, fakeOut_ + 1);
            }
            else
            {
                releaseDir_ = arrowChild_.position - currentAstronaut_.transform.position;
                fakeTimer_++;
            }
        }
        else
        {
            
            releaseDir_ = arrowChild_.position - currentAstronaut_.transform.position;
        }
        ArrowCooldown(onDelay_);
        StartCoroutine(delay());
        Instantiate(pssh_, currentAstronaut_.transform.position, arrowChild_.rotation);
        releaseDir_.Normalize();
        currentAstronaut_.GetComponent<Rigidbody2D>().AddForce(releaseDir_ * pushforce_);
        ChangeAstronaut();
    }

    private void ChangeAstronaut()
    {
        if(currentAstronaut_.name == astronaut1.name)
        {
            currentAstronaut_ = astronaut2;
            if(fakeTimer_ >= fakeRange_)
            {
                changeArrowColor();
            }
        }
        else
        {
            currentAstronaut_ = astronaut1;
        }
    }

    private void changeArrowColor()
    {
        if(arrowSprite_.color == arrowColors_[0])
        {
            arrowSprite_.color = arrowColors_[1];
        }
        else
        {
            arrowSprite_.color = arrowColors_[0];
        }
    }

    private void ChangeArrowDir()
    {
        if(currentDir == rotationDir.LEFT)
        {
            currentDir = rotationDir.RIGHT;
        }else
        {
            currentDir = rotationDir.LEFT;
        }
    }

    private void UpdateArrowPos(Transform newPos)
    {
        arrowAnchor_.transform.position = newPos.position;
        RotateArrowSprite();
    }
    private void ArrowCooldown(bool onCooldown)
    {
        if(onCooldown)
        {
            arrowSprite_.enabled = true;
            onDelay_ = false;           
        }
        else
        {
            arrowSprite_.enabled = false;
            onDelay_ = true;
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(arrowDelay_);
        ArrowCooldown(onDelay_);
    }

    private void RotateArrowSprite()
    {
        if((arrowChild_.position - currentAstronaut_.transform.position) != Vector3.zero)
        {
            Vector3 dir = (arrowChild_.position - currentAstronaut_.transform.position);
            Vector3 up = new Vector3(0, 0, 1);
            var rotation = Quaternion.LookRotation(dir, up);
            rotation.x = 0;
            rotation.y = 0;
            arrowChild_.rotation = rotation;
        }
       
    }

    private Quaternion RotatePssh(Vector3 dir)
    {
        Vector3 up = new Vector3(0, 1, 0);
        var rotation = Quaternion.LookRotation(dir, up);
        rotation.z = 90;
        rotation.y = 90;
        return rotation;
    }
    private void capSpeed()
    {
        if(currentAstronaut_.GetComponent<Rigidbody2D>().velocity.magnitude >= astronautSpeedCap_)
        {
            currentAstronaut_.GetComponent<Rigidbody2D>().velocity = currentAstronaut_.GetComponent<Rigidbody2D>().velocity.normalized * astronautSpeedCap_;
        }
    }
}
