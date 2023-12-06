using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
	public GameObject cube;
	public int maxCube;
	public int maxHeight;
	public void OnSpawn() {
		//obliger de rajouter le UnityEngine devant tout les random.range sinon c'est ambigu avec le System.Random mais on a besoin de System pour le Math.round
		int nbCube = UnityEngine.Random.Range(10, maxCube);
		List<Vector3> positions = new List<Vector3>();
		for (int i = 0; i < nbCube; i++) {
			
			Vector3 pos = transform.position;
			//on le décale de 0.5 pour qu'il soit bien aligné sur la grille
			//on le met à 0.5 en hauteur parce que 0 il traverse le plane, et 1 il flotte
			float posX = (float)Math.Round(UnityEngine.Random.Range(pos.x - 5, pos.x + 4)) + 0.5f;
			float posZ = (float)Math.Round(UnityEngine.Random.Range(pos.z - 5, pos.z + 4)) + 0.5f;
			Vector3 cubePosition = new Vector3(posX, 0.5f, posZ);
			//TODO: Il faudrait faire en sorte de regénérer une pos random mais flemme de faire un while ou autre pour le moment
			if (!positions.Contains(cubePosition)) {
				positions.Add(cubePosition);
				GameObject cubeInstantiated = Instantiate(cube, cubePosition, Quaternion.identity);
				cubeInstantiated.transform.parent = gameObject.transform;
				AddHeight(cubePosition, 0);
			}
		}
	}

	public void AddHeight(Vector3 cubePosition, int height) {
		if (height < maxHeight && UnityEngine.Random.value >= 0.5f) {
			Vector3 pos = new Vector3(cubePosition.x, cubePosition.y + 1, cubePosition.z);
			GameObject cubeInstantiated = Instantiate(cube, pos, Quaternion.identity);
			cubeInstantiated.transform.parent = gameObject.transform;
			AddHeight(pos, height++);
		}
		return ;
	}

	public void OnDespawn() {
		// //TODO: pourquoi pas faire du pooling aussi comme pour le placement des terrains afin d'éviter de faire spawn despawn des trucs tout le temps
		// int i = 0;

		// //Array to hold all child obj
		// GameObject[] allChildren = new GameObject[transform.childCount];

		// //Find all child obj and store to that array
		// foreach (Transform child in transform)
		// {
		// 	allChildren[i] = child.gameObject;
		// 	i += 1;
		// }

		// //Now destroy them
		// foreach (GameObject child in allChildren)
		// {
		// 	DestroyImmediate(child.gameObject);
		// }

}
}
