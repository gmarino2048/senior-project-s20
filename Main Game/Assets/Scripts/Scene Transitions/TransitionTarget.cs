using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TransitionTarget : MonoBehaviour
{
    [SerializeField] private float groundOffset = 0.01f;
    public Vector3 spawnPoint { get; private set; }
    
    private void Start()
    {
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out var hit))
        {
            var hitPosition = hit.point;
            spawnPoint = new Vector3(
                hitPosition.x,
                hitPosition.y + groundOffset,
                hitPosition.z
            );
        }
        else throw new InvalidConstraintException("Could not find the floor to establish spawn point");
    }
}
