using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundYshuffle : MonoBehaviour
{
    private enum Dir
    {
        UP,
        DOWN,
    }
    public Transform[] childen_;

    private Transform camera_;
    private float yStep_ = 17.45f;
    private float lastCamY_;

    private Vector3 rotationFlip = new Vector3(180, 0, 0);


    void Awake()
    {
        camera_ = Camera.main.transform;
        lastCamY_ = camera_.position.y;


    }

    private void Update()
    {
        if ((camera_.position.y - lastCamY_) >= yStep_)
        {
            MovePanel(FindLastPanel(Dir.UP), Dir.UP);
            lastCamY_ = camera_.position.y;
        }
        else if ((camera_.position.y - lastCamY_) <= -yStep_)
        {
            MovePanel(FindLastPanel(Dir.DOWN), Dir.DOWN);
            lastCamY_ = camera_.position.y;
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
        float lowY_ = 0;
        switch (dir)
        {
            case Dir.UP:
                for (int i = 0; i < 3; i++)
                {
                    if(i == 0)
                    {
                        lowY_ = childen_[i].position.y;
                        tmp = i;
                        continue;
                    }
                    if (childen_[i].position.y <= lowY_)
                    {
                        lowY_ = childen_[i].position.y;
                        tmp = i;
                    }
                }
                break;
            case Dir.DOWN:
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        lowY_ = childen_[i].position.y;
                        tmp = i;
                        continue;
                    }
                    if (childen_[i].position.y >= lowY_)
                    {
                        lowY_ = childen_[i].position.y;
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
            case Dir.UP:
                tmp = new Vector3(childen_[index].position.x, childen_[index].position.y + (yStep_ * 3), childen_[index].position.z);
                break;
            case Dir.DOWN:
                tmp = new Vector3(childen_[index].position.x, childen_[index].position.y - (yStep_ * 3), childen_[index].position.z);
                break;
        }
        return tmp;
    }
}
