using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenicCameraRig : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    public bool active;

    private void Update()
    {
        if (active)
        {
            gameObject.transform.RotateAround(
                gameObject.transform.localPosition, 
                Vector3.up,
                rotationSpeed * Time.deltaTime);
        }
    }
}
