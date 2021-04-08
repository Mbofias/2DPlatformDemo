using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01ShootEffects : MonoBehaviour
{
    private void Start()
    {
        transform.parent = null;
    }
    void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
