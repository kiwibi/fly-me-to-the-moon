using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundFollow : MonoBehaviour
{
    private enum Dir
    {
        LEFT,
        RIGHT,
    }
    public Transform[] childen_;

    private Transform camera_;
    private float xStep_ = 70.78f;
    private float travelledX_;
    private float lastCamX_;

    private Vector3 rotationFlip = new Vector3(180, 0, 180);


    void Awake()
    {
        camera_ = Camera.main.transform;
        lastCamX_ = camera_.position.x;


    }

    private void Update()
    {
        travelledX_ = transform.position.x;
        if ((camera_.position.x - lastCamX_ - travelledX_) >= xStep_)
        {
            Debug.Log("W");
            MovePanel(FindLastPanel(Dir.LEFT),Dir.LEFT);
            lastCamX_ = camera_.position.x;
        }
        else if((camera_.position.x - lastCamX_ - travelledX_) <= -xStep_)
        {
            MovePanel(FindLastPanel(Dir.RIGHT), Dir.RIGHT);
            lastCamX_ = camera_.position.x;
        }

    }

    private Vector3 FindCameraPos()
    {
        Vector3 tmp = camera_.position;
        tmp.z = 0;
        return tmp;
    }

    private void MovePanel(int index, Dir dir)
    {
        childen_[index].position = getNewPos(index, dir);
        childen_[index].Rotate(rotationFlip);
    }

    private int FindLastPanel(Dir dir)
    {
        int tmp = 0;
        float lowX_ = 0;
        switch (dir)
        {
            case Dir.LEFT:
                
                for(int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        lowX_ = childen_[i].position.x;
                        tmp = i;
                        continue;
                    }
                    if (childen_[i].position.x <= lowX_)
                    {
                        lowX_ = childen_[i].position.x;
                        tmp = i;
                    }
                }
                break;
            case Dir.RIGHT:
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        lowX_ = childen_[i].position.x;
                        tmp = i;
                        continue;
                    }
                    if (childen_[i].position.x >= lowX_)
                    {
                        lowX_ = childen_[i].position.x;
                        tmp = i;
                    }
                }
                break;
        }
        return tmp;
    }

    private Vector3 getNewPos(int index, Dir dir)
    {
        Vector3 tmp = Vector3.zero;
        switch (dir)
        {
            case Dir.LEFT:
                tmp = new Vector3(childen_[index].position.x + (xStep_ * 3), childen_[index].position.y, childen_[index].position.z);
                break;
            case Dir.RIGHT:
                tmp = new Vector3(childen_[index].position.x - travelledX_ - (xStep_ * 3), childen_[index].position.y, childen_[index].position.z);
                break;
        }
        return tmp;
    }

    private void Rotate(Transform trans)
    {
        trans.Rotate(rotationFlip);
        Vector3 up = new Vector3(0, 0, 1);
        var rotation = Quaternion.LookRotation(rotationFlip, up);
        rotation.x = 0;
        rotation.y = 0;
        trans.rotation = rotation;
    }
}
