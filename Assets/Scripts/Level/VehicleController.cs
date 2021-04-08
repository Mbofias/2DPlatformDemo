using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Range(0, 10f)] public float speed = 1f;
    public Transform topPoint, bottomPoint;
    private Rigidbody2D rb2D;
    public bool faceUp = true;

    public float offset;
    Vector3 offsetV;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        offsetV = new Vector3(0, offset, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
       
        if (faceUp)
        {
            //rb2D.velocity = new Vector2(rb2D.velocity.x, speed);
            rb2D.position = Vector2.Lerp(transform.position, topPoint.position + offsetV, speed * Time.deltaTime);
            if (transform.position.y >= topPoint.transform.position.y)
            {
                faceUp = false;
            }
        }
        else
        {
            //rb2D.velocity = new Vector2(rb2D.velocity.x, -speed);
            rb2D.position = Vector2.Lerp(transform.position, bottomPoint.position- offsetV, speed * Time.deltaTime);
            if (transform.position.y <= bottomPoint.transform.position.y)
            {
                faceUp = true;
            }
        }
    }
}
