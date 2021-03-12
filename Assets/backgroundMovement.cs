using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMovement : MonoBehaviour
{
    public static backgroundMovement instance_;
    public Transform[] childen_;

    public GameObject astronaut_;
    public GameObject astronaut2_;
    private Rigidbody2D rb_;
    private Rigidbody2D rb2_;
    void Start()
    {
        instance_ = this;
        rb_ = astronaut_.GetComponent<Rigidbody2D>();
        rb2_ = astronaut2_.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {

        if(rb_.velocity != Vector2.zero)
        {
            Vector2 tmp = rb_.velocity + rb2_.velocity;
            foreach (var child in childen_)
            {
                child.position += new Vector3(tmp.x,tmp.y, 0) * Time.deltaTime / 4;
            }
        }
        
    }
}
