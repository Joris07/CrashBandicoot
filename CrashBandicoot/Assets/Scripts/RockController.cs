using UnityEngine;

public class RockController : MonoBehaviour
{
	public float speed; 
	public GameController gameController;
	public AudioSource audioSource;

	private void Update() {
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.name == "box_low(Clone)") {
			// audioSource.Play();
			other.gameObject.GetComponent<ParticleSystem>().Play();
			other.gameObject.GetComponent<AudioSource>().Play();
			other.gameObject.GetComponent<MeshRenderer>().enabled = false;	
			Destroy(other.gameObject, 1);
		}
		if (other.gameObject.tag == "Player") {
			Destroy(other.gameObject);
			gameController.OnDeath();
		}
	}
}
