using UnityEngine;
using System.Collections;

public class BadEndController : MonoBehaviour {
	GameController gameController;
	
	void Awake () {
		gameController = GameObject.Find ("GameController")
			.GetComponent<GameController>();
		gameController = GameObject.Find ("GameController")
			.GetComponent<GameController>();
		gameController.OnVillainWin += () => {
			Display();
		};
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Display () {
		GetComponent<SpriteRenderer>().enabled = true;
	}
}
