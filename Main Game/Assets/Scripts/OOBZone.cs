using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOBZone : MonoBehaviour {
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<KeyboardPlayerController>() == null && other.gameObject.GetComponent<PotionItem>() == null) {
            other.gameObject.transform.position = respawnPoint.position;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
