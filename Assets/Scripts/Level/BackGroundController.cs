using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    Rigidbody2D player;
    SpriteRenderer[] sprs;
    [Range(0, 100f)] public float trainSpeed = 0.2f;
    [Range(0, 100f)] public float playerSpeed = 0.2f;
    float imageHalfWidth;
    // Start is called before the first frame update
    void Start()
    {
        sprs = GetComponentsInChildren<SpriteRenderer>();
        imageHalfWidth = sprs[0].sprite.bounds.max.x-sprs[0].sprite.bounds.center.x;
        int index = 0;
        foreach(SpriteRenderer spr in sprs)
        {
            spr.transform.SetPositionAndRotation(new Vector3(imageHalfWidth * 2 * index, 0, 0), new Quaternion(0, 0, 0, 0));
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        foreach (SpriteRenderer spr in sprs)
        {
            if (LevelManager.Instance.TrainEventPlaying)
                spr.transform.Translate(Vector3.left * Time.deltaTime * trainSpeed);
            else
                spr.transform.Translate((player.velocity * Vector2.left).normalized * Time.deltaTime * playerSpeed);

            float newPosX = spr.transform.position.x;
            if (newPosX <= -imageHalfWidth * 2)
                spr.transform.Translate(new Vector3(imageHalfWidth * 4f, 0f, 0f));
            else if (newPosX >= imageHalfWidth * 4)
                spr.transform.Translate(new Vector3(-imageHalfWidth * 2f, 0f, 0f));
        }
    }
}
