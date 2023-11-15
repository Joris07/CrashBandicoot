using System;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public float timeAlive;
	public GameObject panelDeath;
	public TextMeshProUGUI timeAliveText;

	public PoolManager poolManager;

	// Update is called once per frame
	void Update()
	{
		timeAlive += Time.deltaTime;
	}

	public void OnDeath () {
		panelDeath.SetActive(true);
		timeAliveText.text = "You were alive for "+ Math.Round(timeAlive, 1) + " seconds";
		Time.timeScale = .5f;
	}
}
