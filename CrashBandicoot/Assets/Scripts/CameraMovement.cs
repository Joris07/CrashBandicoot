using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public float speed;
	public List<GameObject> objetToFollow;
	public GameObject currentObjetToFollow;
	public PoolManager poolManager;
	public float distanceBeforeShowingNextTerrain;
	// Update is called once per frame
	void Update()
	{
		//mouvement de la caméra
		transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.World);

		//génération du terrain
		if (Vector3.Distance(transform.position, currentObjetToFollow.transform.position) > distanceBeforeShowingNextTerrain) {
			GameObject nextObjetToFollow = poolManager.ShowObject(currentObjetToFollow);
			objetToFollow.Add(currentObjetToFollow);
			currentObjetToFollow = nextObjetToFollow;
			if (objetToFollow.Count > 3) {
				GameObject objetToRemove = objetToFollow[0];
				objetToFollow.Remove(objetToRemove);
				poolManager.HideObjet(objetToRemove);
			}
		}
	}
}
