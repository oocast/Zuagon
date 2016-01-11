using UnityEngine;
using System.Collections;

public class VillainHealth : MonoBehaviour {
	SoundManagerScript soundManager;
	GameController controller;
	DragonHP hpUI;

	int maxHealth;
	public int health;

	void Awake () {
		controller = GameObject.Find ("GameController")
			.GetComponent<GameController> ();
		soundManager = GameObject.Find ("SoundManager")
			.GetComponent<SoundManagerScript> ();
		hpUI = GameObject.Find ("HPStuff")
			.GetComponentInChildren<DragonHP> ();
		maxHealth = health;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		UpdateUIHP ();
		if (health < 0) {
			controller.HeroWin();
		}
	}

	public void GetDamage (int ballCount, int magicBallCount, int combo) {
		int baseDamage = 10;
		if (magicBallCount > 0) 
			baseDamage = 100 * magicBallCount;
		int damage = ballCount * baseDamage * combo;
		if (combo > 1) {
			soundManager.PlayDragonGrowling();
		}
		health -= damage;
	}

	void UpdateUIHP () {
		int value = health * 100 / maxHealth;
		hpUI.UpdateHealth(value);
	}
}
