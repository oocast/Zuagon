using UnityEngine;
using System.Collections;

public class SpawnMarbles : MonoBehaviour
{
	ChainMovement chainMovement;
	VillainController villain;
	GameController gameController;
	public GameObject greenBallPrefab,blueBallPrefab,yellowBallPrefab,redBallPrefab, counterMagicBallPrefab;
	GameObject spawnPoint,endPoint;
	GameObject wayPointObj;
	PathRouter path;
	public Vector3[] waypointArray;
	public ArrayList ballChain = new ArrayList ();
	public GameObject head;
	public bool autoSpawn;
	public float ballFowardSpeed;
	public float ballBackwardSpeed;
	float ballDiameter;
	float magicRatio;
	float magicTimeGap;
	float spawnInterval;
	float forwardPercentagePerSecond;
	float backwardPercentagePerSecond;
	float prevSpawnTime;
	string autoSpawnColorTag;
	Vector3 newPos,min;
	int min_index;
	Transform balls;

	bool spawnLock;
	bool once;
	bool moveflag=false;

	int countYellow=0, countGreen=0, countRed=0, countBlue=0;
	GameObject last;

	void Awake () {
		chainMovement = GetComponent<ChainMovement>();
		gameController = GameObject.Find ("GameController")
			.GetComponent<GameController> ();

		villain = GameObject.Find ("JamODrum")
			.GetComponent<VillainController>();
		
		villain.RegisterMagicStartAction (() => {
			forwardPercentagePerSecond *= 0.02f;
			spawnInterval *= 50f;
		});
		villain.RegisterMagicEndAction (() => {
			forwardPercentagePerSecond *= 50f;
			spawnInterval *= 0.02f;
		});
		gameController.OnGameStart += () => {
			spawnLock = false;
		};
		gameController.OnVillainWin += () => {
			spawnLock = true;
		};
		gameController.OnHeroWin += () => {
			spawnLock = true;
		};
	}

	// Use this for initialization
	void Start ()
	{
		spawnPoint = GameObject.Find ("SpawnPoint");
		endPoint = GameObject.Find ("EndPoint");

		wayPointObj = GameObject.Find("Waypoints");
		waypointArray = new Vector3[wayPointObj.transform.childCount + 1];
		for (int i = 0; i < waypointArray.Length - 1; i++) {
			waypointArray[i] = wayPointObj.transform.GetChild(i).position;
		}
		waypointArray[waypointArray.Length - 1] = endPoint.transform.position;

		prevSpawnTime = 1f;
		autoSpawnColorTag = null;
		balls = GameObject.Find ("/TracingPanel/Balls").transform;
		once = false;

		path = GameObject.Find ("PathRouter")
			.GetComponent<PathRouter>();

		magicRatio = 0.5f;
		magicTimeGap = 0.02f;
		ballDiameter = greenBallPrefab.transform.localScale.x * 2f * magicRatio;
		spawnInterval = ballDiameter / ballFowardSpeed + magicTimeGap;

		spawnLock = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		LateInitialize ();
		if (Input.GetKeyDown ("r")) {
			InsertRedBall ();

		}
		if (Input.GetKeyDown ("b")) {
			InsertBlueBall ();
		}
		if (Input.GetKeyDown ("y")) {

			InsertYellowBall ();
		}
		if (Input.GetKeyDown ("g")) {
		
			InsertGreenBall ();
		}
		if (autoSpawn) {
			AutomaticSpawn ();
		}

		/*
		for (int i = 0; i < waypointArray.Length - 1; i++) {
			waypointArray[i] = wayPointObj.transform.GetChild(i).position;
		}
		*/
	}  

	void LateInitialize () {
		if (!once) {
			once = true;
			forwardPercentagePerSecond = ballFowardSpeed / path.pathLength;
			backwardPercentagePerSecond = ballBackwardSpeed / path.pathLength;
			chainMovement.SetDiameterPPS (ballDiameter / path.pathLength);
		}
	}

	void InsertYellowBall()
	{
		GameObject yBall = Instantiate (yellowBallPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;
		countYellow++;
		yBall.name = "Yellow" + countYellow;
		yBall.tag = "Yellow";
		BallChainMovement ballScript = yBall.AddComponent<BallChainMovement> ();
		ballScript.SetPPS(forwardPercentagePerSecond, backwardPercentagePerSecond);
		foreach (GameObject b in ballChain) 
		{
			//b.GetComponent<BallChainMovement>().moveFlag=false;
		}

		ballChain.Add (yBall);
		yBall.transform.parent = balls;

//		if (ballChain.Count == 1)
//		{
//			head=yBall;
//			yBall.AddComponent<BallChainMovement> ();
//			Debug.Log("Head is" + head.name);
//		}
//		else
//		{
//			yBall.transform.parent=head.transform;
//			yBall.AddComponent<ChildBallMovement> ();
//		}


	}

	void InsertGreenBall()
	{
		GameObject gBall = Instantiate (greenBallPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;
		countGreen++;
//		gBall.transform.parent = chain.transform;
		gBall.name = "Green" + countGreen;
		BallChainMovement ballScript = gBall.AddComponent<BallChainMovement> ();
		ballScript.SetPPS(forwardPercentagePerSecond, backwardPercentagePerSecond);
		gBall.tag = "Green";
		foreach (GameObject b in ballChain) 
		{
			//b.GetComponent<BallChainMovement>().moveFlag=false;
		}
		ballChain.Add (gBall);
		gBall.transform.parent = balls;

//		if (ballChain.Count == 1)
//		{
//			head=gBall;
//			gBall.AddComponent<BallChainMovement> ();
//			Debug.Log("Head is" + head.name);
//		}
//		else
//		{
//			gBall.transform.parent=head.transform;
//			gBall.AddComponent<ChildBallMovement>();
//		}


//		gBall.AddComponent<BallChainMovement> ();
	}

	void InsertRedBall()
	{
		GameObject rBall = Instantiate (redBallPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;
		countRed++;
		rBall.name = "Red" + countRed;
		BallChainMovement ballScript = rBall.AddComponent<BallChainMovement> ();
		ballScript.SetPPS(forwardPercentagePerSecond, backwardPercentagePerSecond);
		rBall.tag = "Red";
		foreach (GameObject b in ballChain) 
		{
			//b.GetComponent<BallChainMovement>().moveFlag=false;
		}

		ballChain.Add (rBall);
		rBall.transform.parent = balls;

//		if (ballChain.Count == 1)
//		{
//			head=rBall;
//			rBall.AddComponent<BallChainMovement> ();
//			Debug.Log("Head is" + head.name);
//		}
//		else
//		{
//			rBall.transform.parent=head.transform;
//			rBall.AddComponent<ChildBallMovement> ();
//		}
//		rBall.transform.parent = chain.transform;


//		rBall.AddComponent<BallChainMovement> ();
	}

	void InsertBlueBall()
	{
		GameObject bBall = Instantiate (blueBallPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;
		countBlue++;
		bBall.name = "Blue" + countBlue;
		BallChainMovement ballScript = bBall.AddComponent<BallChainMovement> ();
		ballScript.SetPPS(forwardPercentagePerSecond, backwardPercentagePerSecond);
		bBall.tag = "Blue";
		foreach (GameObject b in ballChain) 
		{
			//b.GetComponent<BallChainMovement>().moveFlag=false;
		}
//		bBall.transform.parent = chain.transform;

		ballChain.Add (bBall);
		bBall.transform.parent = balls;

//		if (ballChain.Count == 1) 
//		{
//			head = bBall;
//			bBall.AddComponent<BallChainMovement> ();
//			Debug.Log ("Head is" + head.name);
//		} 
//		else
//		{
//			bBall.transform.parent=head.transform;
//			bBall.AddComponent<ChildBallMovement> ();
//		}


//		bBall.AddComponent<BallChainMovement> ();
	}

//	

	void InsertCounterMagicBall () {
		GameObject cmBall = Instantiate (counterMagicBallPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;
		cmBall.name = "CounterMagic";
		BallChainMovement ballScript = cmBall.AddComponent<BallChainMovement> ();
		ballScript.SetPPS(forwardPercentagePerSecond, backwardPercentagePerSecond);
		cmBall.tag = "CounterMagic";
		foreach (GameObject b in ballChain) 
		{
			//b.GetComponent<BallChainMovement>().moveFlag=false;
		}
		//		bBall.transform.parent = chain.transform;
		
		ballChain.Add (cmBall);
		cmBall.transform.parent = balls;
	}
	public void InsertBallAtTail (string tag) {
		if (tag == "Red") {
			InsertRedBall();
		}
		else if (tag == "Green") {
			InsertGreenBall();
		}
		else if (tag == "Blue") {
			InsertBlueBall();
		}
		else if (tag == "Yellow") {
			InsertYellowBall();
		}
		else if (tag == "CounterMagic") {
			InsertCounterMagicBall();
		}
	}

	public void ChangeNextColor (string tag) {
		autoSpawnColorTag = tag;
	}

	public void SetPPSForBallScript (BallChainMovement ballScript) {
		ballScript.SetPPS(forwardPercentagePerSecond, backwardPercentagePerSecond);
	}

	void AutomaticSpawn () {
		if (Time.time - prevSpawnTime > spawnInterval && !spawnLock) {
			InsertBallAtTail ();
		}
	}

	void InsertBallAtTail () {
		prevSpawnTime = Time.time;
		if (autoSpawnColorTag == null) {
			int colorIdx = Random.Range(0,4);
			string color = null;
			switch (colorIdx){
			case 0:
				color = "Red";
				break;
			case 1:
				color = "Green";
				break;
			case 2:
				color = "Blue";
				break;
			case 3:
				color = "Yellow";
				break;
			}
			InsertBallAtTail (color);
		}
		else {
			InsertBallAtTail (autoSpawnColorTag);
			autoSpawnColorTag = null;
		}
	}
}

