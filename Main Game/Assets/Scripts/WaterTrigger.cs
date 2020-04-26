using UnityEngine;

public class WaterTrigger : MonoBehaviour {
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private float fadeInTime, fadeOutTime;


	private void OnTriggerEnter(Collider other) {
		KeyboardPlayerController player = other.gameObject.GetComponent<KeyboardPlayerController>();
		if(player != null) {
			StartCoroutine(player.OutOfBounds(spawnPoint, fadeInTime, fadeOutTime));
			return;
		}
		IcePotion potion = other.gameObject.GetComponent<IcePotion>();
		if(potion != null) {
			potion.TriggerFromWater(transform);
		}
	}
}
