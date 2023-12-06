using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public void OnDeath() {
		panelDeath.SetActive(true);
		timeAliveText.text = "Tu as survécu "+ Math.Round(timeAlive, 1) + " secondes.";
		Time.timeScale = 0.3f;
	}

	public void RestartBtn() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("GameScene");
	}	
	
	public void MenuBtn() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("MenuScene");
	}
}
