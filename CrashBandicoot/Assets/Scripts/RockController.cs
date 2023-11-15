using UnityEngine;

public class RockController : MonoBehaviour
{
	public float speed; 
	public GameController gameController;

	private void Update() {
		transform.Translate(Vector3.back * speed * Time.deltaTime);
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player") {
			gameController.OnDeath();
		}
	}
}
