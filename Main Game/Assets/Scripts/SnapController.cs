using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController: MonoBehaviour
{
    Vector3 sizeOfObject;

    void Start()
    {
        sizeOfObject = transform.parent.GetComponent<Collider>().bounds.size;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "SnappableObject")
        {
            float sizeX = sizeOfObject.x;
            float sizeY = sizeOfObject.y;
            float sizeZ = sizeOfObject.z;

            // Using switch in case we want to add more than one collider for an object
            switch (this.transform.tag) 
            {
                case "TopSnapCollider":
                    other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.   position.z);                             // Used to lock the snappable object with respect to the snappable collider of the original object
                    //other.transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + sizeY, transform.parent.position.z); // Used to lock the snappable object with respect to the original object
                    other.GetComponentInParent<Rigidbody>().useGravity = false;
                    break;
            }
        }
    }
}
