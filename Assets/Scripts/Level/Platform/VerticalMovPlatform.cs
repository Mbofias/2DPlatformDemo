using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovPlatform : MonoBehaviour
{
    [Range(0, 10f)] public float speed = 1f;
    public Transform botPoint, topPoint;
    private Rigidbody2D rb2D;
    public bool goingUp = true;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        botPoint.parent = null;
        topPoint.parent = null;
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (goingUp)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, speed);
            if (transform.position.y > topPoint.transform.position.y)
            {
                goingUp = false;
            }
        }
        else
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, -speed);
            if (transform.position.y < botPoint.transform.position.y)
            {
                goingUp = true;
            }
        }
    }
}
