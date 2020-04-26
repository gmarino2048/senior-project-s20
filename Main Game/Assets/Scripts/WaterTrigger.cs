using UnityEngine;

public class WaterTrigger : MonoBehaviour {
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private float fadeInTime, fadeOutTime;


	private void OnTriggerEnter(Collider other) {
		Debug.Log("yeh");
		KeyboardPlayerController player = other.gameObject.GetComponent<KeyboardPlayerController>();
		if(player != null) {
			StartCoroutine(player.OutOfBounds(spawnPoint, fadeInTime, fadeOutTime));
		}
	}
}
