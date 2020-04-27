using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LAST MINUTE COINS
public class GBCoin : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        GBPlayerController player = collision.GetComponent<GBPlayerController>();
        if(player != null) {
            player.GetCoin();
            Destroy(this.gameObject);
        }
    }
}
