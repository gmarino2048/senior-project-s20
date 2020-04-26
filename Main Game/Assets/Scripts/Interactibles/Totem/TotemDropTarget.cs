using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemDropTarget : ObjectPlacementVolume
{
    [SerializeField] private TotemController.GemColor color;

    private TotemController _controller; 
    
    private void Start()
    {
        _controller = GetComponentInParent<TotemController>();
    }
    
    public override void PlacementTrigger(InteractiveObject placedObject)
    {
        _controller.AddGem(color);
    }
}
