using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

	public GameObject baseTerrain;

	public int nbOfFloorInPool;
	private List<GameObject> allTerrain = new List<GameObject>();
	private List<GameObject> terrainShown = new List<GameObject>();
	private List<GameObject> terrainHidden = new List<GameObject>();

	private void Awake() {
		for (int i = 0; i < nbOfFloorInPool; i++)
		{
			allTerrain.Add(baseTerrain);
		}
		foreach (var terrain in allTerrain)
		{
			GameObject terrainInstantiated = Instantiate(terrain, new Vector3(0, 0, 0), transform.rotation);
			terrainInstantiated.SetActive(false);
			terrainHidden.Add(terrainInstantiated);
		}
	}
	public GameObject ShowObject(GameObject neighbour) {
		GameObject terrainToPlace = terrainHidden[Random.Range(0, terrainHidden.Count)];
		Vector3 neighbourPos = neighbour.transform.position;
		terrainToPlace.transform.position = new Vector3(neighbourPos.x, neighbourPos.y, neighbourPos.z - neighbour.GetComponent<Collider>().bounds.size.z);
		terrainHidden.Remove(terrainToPlace);
		terrainShown.Add(terrainToPlace);
		terrainToPlace.SetActive(true);
		terrainToPlace.GetComponent<ObstacleGenerator>().OnSpawn();
		return terrainToPlace;
	}

	public void HideObjet(GameObject terrainToHide) {
		terrainShown.Remove(terrainToHide);
		terrainToHide.SetActive(false);
		terrainHidden.Add(terrainToHide);
		terrainToHide.GetComponent<ObstacleGenerator>().OnDespawn();
	}
}
