using UnityEngine;
using System.Collections;

public class IntroSceneStuff : MonoBehaviour {
	GameController gameController;
	SingleLauncher launcher;
	JamoDrum jod;
	JamoDrumKeyVersion jodkey;

	bool gamePad1=false, gamePad2=false, gamePad3=false, gamePad4=false;
	float timer=0.0f;
	GameObject storystuff,hero1,hero2,hero3,villain;
	bool instru=false;
	bool start = false;

	void Awake () {
		gameController = GameObject.Find ("GameController")
			.GetComponent<GameController>();

		jod = GameObject.Find ("JamODrum").GetComponent<JamoDrum> ();
		jodkey = GameObject.Find ("JamODrum").GetComponent<JamoDrumKeyVersion> ();
		launcher = GameObject.Find ("JamODrum").GetComponent<SingleLauncher> ();

		if (launcher.key) {
			jodkey.AddHitEvent(HitHandler);
			//jodkey.AddSpinEvent(SpinHandler);
		}
		else {
			jod.AddHitEvent(HitHandler);
			//jod.AddSpinEvent(SpinHandler);
		}
	}

	// Use this for initialization
	void Start () 
	{
		storystuff = GameObject.Find ("StoryStuff");
		hero1 = GameObject.Find ("IntroHero");
		hero2 = GameObject.Find ("IntroHero2");
		hero3 = GameObject.Find ("IntroHero3");
		villain = GameObject.Find ("IntroVillain");
		hero1.SetActive (false);
		hero2.SetActive (false);
		hero3.SetActive (false);
		villain.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer > 5.0f) 
		{
			storystuff.SetActive(false);
			hero1.SetActive (true);
			hero2.SetActive (true);
			hero3.SetActive (true);
			villain.SetActive (true);
			instru=true;

		}
		if (instru &&Input.GetKeyDown ("1"))
		{
			gamePad1=true;
		}
		if(instru && Input.GetKeyDown("2"))
		{
			gamePad2=true;
		}
		if(instru && Input.GetKeyDown("3"))
		{
			gamePad3=true;
		}
		if (instru && Input.GetKeyDown ("4"))
		{
			gamePad4=true;
		}
		if (gamePad1 && gamePad2 && gamePad3 && gamePad4) 
		{
			//Next Scene
			start = true;
			hero1.SetActive (false);
			hero2.SetActive (false);
			hero3.SetActive (false);
			villain.SetActive (false);
			gameController.GameStart();
		}
	}

	void HitHandler (int controllerID) {
		if (!start) {
			switch (controllerID) {
			case 1:
				gamePad1 = true;
				break;
			case 2:
				gamePad2 = true;
				break;
			case 3:
				gamePad3 = true;
				break;
			case 4:
				gamePad4 = true;
				break;
			}
		}
	}
}
