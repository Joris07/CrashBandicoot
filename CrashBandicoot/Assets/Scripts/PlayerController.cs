using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	public float jumpForce;
	public bool canJump = true;
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey("down")) {
			transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("left")) {
			transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("right")) {
			transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("up")) {
			transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
		}
		//ne fonctionne pas (pas tester) penser à vérifier pour le saut infini
		if (Input.GetKeyDown("space") && canJump) {
			GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
		}
	}

	//Attention si vous voulez sauter sur les cubes, il faudra aussi leur mettre le tag Floor (avec ce développement en tout cas)
	//Sauf que s'ils ont le tag floor, même entrer en contact de face / côté avec eux déclenchera la réinitialisation du saut et donc de pouvoir spam pour grimper
	private void OnCollisionExit(Collision other) {
		if (other.gameObject.tag == "Floor") {
			canJump = false;
			Debug.Log("APPLLLE");
		}
	}
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Floor") {
			canJump = true;
			Debug.Log("APPLLLE");
		}
		if (other.gameObject.name == "Apple(Clone)") {
			Debug.Log("APPLLLE");
			other.transform.Translate(Vector3.up * speed * Time.deltaTime);
			Destroy(other.gameObject, 1);
		}
		if (other.gameObject.name == "box_low(Clone)") {
			Debug.Log("box");
		}
	}
}
