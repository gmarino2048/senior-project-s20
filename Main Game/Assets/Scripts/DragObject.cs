﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

    private Vector3 mOffSet;
    private float mZCoord;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffSet = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);  
    }

    private void OnMouseUp()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffSet;
    }   
}
