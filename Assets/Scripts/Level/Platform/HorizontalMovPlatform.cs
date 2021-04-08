using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovPlatform : MonoBehaviour
{
    [Range(0, 10f)] public float speed = 1f;
    public Transform leftpoint, rightpoint;
    private Rigidbody2D rb2D;
    public bool faceRight = true;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        leftpoint.parent = null;
        rightpoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (faceRight)
        {
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
            if (transform.position.x > rightpoint.transform.position.x)
            {
                faceRight = false;
            }
        }
        else
        {
            rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);
            if (transform.position.x < leftpoint.transform.position.x)
            {
                faceRight = true;
            }
        }
    }
}
