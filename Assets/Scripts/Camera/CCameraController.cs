using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraController : MonoBehaviour
{
    [Range(0f, 1f)] public float cameraZone = 0.5f;
    [Range(0f, 1f)] public float smoothFactor = 0.1f;
    [Range(0f, 1f)] public float cameraZoomFactor = 0.01f;
    public float[] cameraOrtographicSize;
    public EdgeCollider2D collisionRight, collisionLeft;

    private int currentSize = 0;
    private Bounds cameraBounds;
    private float zoneH, zoneW;
    private Transform target;
    private Bounds deadZoneCube;


    void Start()
    {
        zoneH = Camera.main.orthographicSize * cameraZone;
        zoneW = zoneH * Camera.main.aspect;
        deadZoneCube = new Bounds(transform.position, new Vector3(zoneW * 2, zoneH * 2, 1f));
        SetCameraCollider();
    }

    void Update()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        if (cameraOrtographicSize[currentSize] != Camera.main.orthographicSize)
        {
            float increaseValue = (cameraOrtographicSize[currentSize] - Camera.main.orthographicSize) * cameraZoomFactor;

            transform.position += Vector3.up * increaseValue;
            Camera.main.orthographicSize += increaseValue;
            SetCameraCollider();
        }

        float x, y;
        if (!IsInDeadZone(out x, out y))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(x, y, 0f), smoothFactor);
        }
    }

    /// <summary>
    /// Checks out if the camera should interpolate towards the player.
    /// </summary>
    /// <param name="x">Is an out parameter, returns the X point the camera should move to, in case it needs to move.</param>
    /// <param name="y">Is an out parameter, returns the Y point the camera should move to, in case it needs to move.</param>
    /// <returns></returns>
    private bool IsInDeadZone(out float x, out float y)
    {
        deadZoneCube.center = transform.position;
        if (!deadZoneCube.Contains(target.position))
        {
            Vector3 point = deadZoneCube.ClosestPoint(target.position);
            x = target.position.x - point.x;
            y = target.position.y - point.y;

            if ((transform.position.x - Camera.main.orthographicSize * Camera.main.aspect < cameraBounds.min.x && x < 0) || (transform.position.x + Camera.main.orthographicSize * Camera.main.aspect > cameraBounds.max.x && x > 0))
                x = 0;

            if ((transform.position.y - Camera.main.orthographicSize < cameraBounds.min.y && y < 0) || (transform.position.y + Camera.main.orthographicSize > cameraBounds.max.y && y > 0))
                y = 0;

            return false;
        }
        x = y = 0;

        return true;
    }

    /// <summary>
    /// Function used to Start the cameraBounds at the start of anyLevel.
    /// </summary>
    /// <param name="min">Min position of the camera bounds.</param>
    /// <param name="max">Max position of the camera bounds.</param>
    public void StartCameraBounds(Vector2 min, Vector2 max)
    {
        cameraBounds = new Bounds();
        cameraBounds.min = min;
        cameraBounds.max = max;
    }

    /// <summary>
    /// Function used to modify either min or max on the current camera bounds.
    /// </summary>
    /// <param name="position">New min or max to set.</param>
    /// <param name="isMaxPoint">true to set max point, false to set min point. (false by default)</param>
    public void ModifyCameraBounds(Vector2 position, bool isMaxPoint = false)
    {
        if (isMaxPoint)
            cameraBounds.max = position;
        else
            cameraBounds.min = position;

        Debug.Log("Minimum camera bounds x: " + cameraBounds.min.x);
        Debug.Log("Minimum camera bounds y: " + cameraBounds.min.y);
        Debug.Log("Maximum camera bounds x: " + cameraBounds.max.x);
        Debug.Log("Maximum camera bounds y: " + cameraBounds.max.y);

    }

    /// <summary>
    /// Getter for the Camera Bounds.
    /// </summary>
    /// <returns>Camera Bounds value</returns>
    public Bounds GetCameraBounds()
    {
        return cameraBounds;
    }

    /// <summary>
    /// Modify the camera Ortographic Size acording to it's established values.
    /// </summary>
    /// <param name="value">0.Small, 1.Big, 2.Medium</param>
    public void UpdateCameraSize(int value)
    {
        value = Mathf.Abs(value);
        value = Mathf.Min(value, cameraOrtographicSize.Length - 1);

        currentSize = value;
    }

    private void SetCameraCollider()
    {
        Vector2[] ColliderPoints = new Vector2[2];

        ColliderPoints[0] = new Vector2(-cameraOrtographicSize[currentSize] * Camera.main.aspect, cameraOrtographicSize[currentSize]);
        ColliderPoints[1] = new Vector2(-cameraOrtographicSize[currentSize] * Camera.main.aspect, -cameraOrtographicSize[currentSize]);
        collisionLeft.points = ColliderPoints;

        ColliderPoints[0] = new Vector2(cameraOrtographicSize[currentSize] * Camera.main.aspect, cameraOrtographicSize[currentSize]);
        ColliderPoints[1] = new Vector2(cameraOrtographicSize[currentSize] * Camera.main.aspect, -cameraOrtographicSize[currentSize]);
        collisionRight.points = ColliderPoints;
    }
}
