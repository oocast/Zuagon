using UnityEngine;
using System.Collections;

public class ChangeColorBallScriptCanonball : MonoBehaviour {
	ExplosionController explosion;
	SoundManagerScript soundManager;
	VillainController villain;

	public Material red_Tex, green_Tex, blue_Tex, yellow_Tex;
	public int ballsToBeChanged;
	int temp_ballstobeChanged;
	GameObject sc,collided_GO,ahead,behind;
	GameObject temp_ahead,temp_behind;
	int temp_ahead_index,temp_behind_index;
	string temp_behind_color, temp_ahead_color;
	ArrayList bc;
	int ind = 0,collision_index, ahead_index, behind_index;
	string co;
	bool checkAhead=true,checkBehind=false,col_recurse=true;
	bool hitOnce = false;
	float speedLimit = 65f;
	int hitCount = 0;

	void Awake () {
		soundManager = GameObject.Find ("SoundManager")
			.GetComponent<SoundManagerScript> ();
		explosion = GameObject.Find ("ExplosionController")
			.GetComponent<ExplosionController>();
	}

	// Use this for initialization
	void Start () 
	{
		sc = GameObject.Find ("SpawnController");
		bc = sc.GetComponent<SpawnMarbles> ().ballChain;
		temp_ballstobeChanged = ballsToBeChanged;
		villain = GameObject.Find ("JamODrum")
			.GetComponent<VillainController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (temp_ballstobeChanged - ballsToBeChanged >= 2) 
		{
			col_recurse=false;
		}
		float mag = GetComponent<Rigidbody>().velocity.magnitude;
		if (mag < speedLimit && mag > 0f) {
			GetComponent<Rigidbody>().velocity = speedLimit / mag * GetComponent<Rigidbody>().velocity;
		}
		if (hitCount > 3) {
			explosion.CreateExplosion(transform.position);
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (!hitOnce && col.gameObject.GetComponent<BallChainMovement>() != null) {
			hitOnce = true;
			villain.spawnCount++;
			foreach (GameObject b in bc)
			{
				ind++;
				if(col.gameObject.name==b.gameObject.name)
				{
					collision_index=ind;
					collided_GO=b.gameObject;
				}
			}
			if (collision_index < bc.Count - 1)
				CheckNeighbourColors (collision_index, collided_GO);
			//GetComponent<MeshRenderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			transform.GetChild(0).gameObject.SetActive(false);
			explosion.CreateExplosion(transform.position);
			Destroy(gameObject, 0.5f);
			soundManager.PlayChangeColorAudio ();
		}
		else {
			hitCount++;
		}
	}

	void CheckNeighbourColors(int j, GameObject j_GO)
	{
		int i=0;
		if (bc.Count < ballsToBeChanged)
		{
			ballsToBeChanged=bc.Count;
		}
		foreach (GameObject b in bc)
		{
			i++;
			if(i==j-1)
			{
				if(b.gameObject!=null)
				{
					ahead=b.gameObject;
					ahead_index=i;
				}
				else
				{
					break;
				}
			}
			if(i==j+1)
			{
				if(b.gameObject!=null)
				{
					behind=b.gameObject;
					behind_index=i;
					break;
				}
				else
				{
					break;
				}
			}
		}
		if (ballsToBeChanged > 0 && ahead != null && behind != null) {
			if (behind.tag == "Blue") {
				if (ahead.tag == "Green") {
					string[] s = new string[]{"Yellow","Red"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Red") {
					string[] s = new string[]{"Yellow","Green"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Yellow") {
					string[] s = new string[]{"Red","Green"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Blue") {
					string[] s = new string[]{"Yellow","Red","Green"};
					ChangeCurrentColor (s, j_GO);
				} else {
					Debug.Log ("Does not exist");
				}
			} else if (behind.tag == "Red") {
				if (ahead.tag == "Green") {
					string[] s = new string[]{"Yellow","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Red") {
					string[] s = new string[]{"Green","Yellow","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Yellow") {
					string[] s = new string[]{"Blue","Green"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Blue") {
					string[] s = new string[]{"Green","Yellow"};
					ChangeCurrentColor (s, j_GO);
				} else {
					Debug.Log ("Does not exist");
				}
			} else if (behind.tag == "Green") {
				if (ahead.tag == "Green") {
					string[] s = new string[]{"Yellow","Red","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Red") {
					string[] s = new string[]{"Blue","Yellow"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Yellow") {
					string[] s = new string[]{"Red","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Blue") {
					string[] s = new string[]{"Red","Yellow"};
					ChangeCurrentColor (s, j_GO);
				} else {
					Debug.Log ("Does not exist");
				}
			} else if (behind.tag == "Yellow") {
				if (ahead.tag == "Green") {
					string[] s = new string[]{"Red","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Red") {
					string[] s = new string[]{"Blue","Green"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Yellow") {
					string[] s = new string[]{"Red","Green","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (ahead.tag == "Blue") {
					string[] s = new string[]{"Red","Green"};
					ChangeCurrentColor (s, j_GO);
				} else {
					Debug.Log ("Does not exist");
				}
			} else {
				Debug.Log ("Does not exist");
			}
		} else {
			Debug.Log ("Done");
		}
		if (ballsToBeChanged > 0) {

				CheckNeighbourColorsAhead (ahead_index, ahead);

		}
	}

	void ChangeCurrentColor(string[] c, GameObject GO)
	{
		if (GO.tag != "CounterMagic") {
			co=c[Random.Range(0,c.Length)];
			if (co == "Red") 
			{
				GO.tag = co;
				GO.transform.GetChild(0).GetComponent<MeshRenderer> ().material = red_Tex;
			} else if (co == "Yellow")
			{
				GO.tag=co;
				GO.transform.GetChild(0).GetComponent<MeshRenderer>().material=yellow_Tex;
			}
			else if(co=="Blue")
			{
				GO.tag=co;
				GO.transform.GetChild(0).GetComponent<MeshRenderer>().material=blue_Tex;
			}
			else if(co=="Green")
			{
				GO.tag=co;
				GO.transform.GetChild(0).GetComponent<MeshRenderer>().material=green_Tex;
			}
		}
		ballsToBeChanged--;
	}

	void CheckNeighbourColorsAhead(int j, GameObject j_GO)
	{
		int i = 0;
		if (bc.Count < ballsToBeChanged) {
			ballsToBeChanged = bc.Count;
		}
		foreach (GameObject b in bc) {
			i++;
			if (i == j - 1) {
				if (b.gameObject != null) {
					temp_ahead = b.gameObject;
					temp_ahead_index = i;
				} else {
					break;
				}
			}
			if (i == j + 1) {
				if (b.gameObject != null) {
					temp_behind = b.gameObject;
					temp_behind_index = i;
					break;
				} else {
					break;
				}
			}
		}
		if (ballsToBeChanged > 0 && temp_ahead != null && temp_behind != null) {
			if (temp_behind.tag == "Blue") {
				if (temp_ahead.tag == "Green") {
					string[] s = new string[]{"Yellow","Red"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Red") {
					string[] s = new string[]{"Yellow","Green"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Yellow") {
					string[] s = new string[]{"Red","Green"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Blue") {
					string[] s = new string[]{"Yellow","Red","Green"};
					ChangeCurrentColor (s, j_GO);
				} else {
					Debug.Log ("Does not exist");
				}
			} else if (temp_behind.tag == "Red") {
				if (temp_ahead.tag == "Green") {
					string[] s = new string[]{"Yellow","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Red") {
					string[] s = new string[]{"Green","Yellow","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Yellow") {
					string[] s = new string[]{"Blue","Green"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Blue") {
					string[] s = new string[]{"Green","Yellow"};
					ChangeCurrentColor (s, j_GO);
				} else {
					Debug.Log ("Does not exist");
				}
			} else if (temp_behind.tag == "Green") {
				if (temp_ahead.tag == "Green") {
					string[] s = new string[]{"Yellow","Red","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Red") {
					string[] s = new string[]{"Blue","Yellow"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Yellow") {
					string[] s = new string[]{"Red","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Blue") {
					string[] s = new string[]{"Red","Yellow"};
					ChangeCurrentColor (s, j_GO);
				} else {
					Debug.Log ("Does not exist");
				}
			} else if (temp_behind.tag == "Yellow") {
				if (temp_ahead.tag == "Green") {
					string[] s = new string[]{"Red","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Red") {
					string[] s = new string[]{"Blue","Green"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Yellow") {
					string[] s = new string[]{"Red","Green","Blue"};
					ChangeCurrentColor (s, j_GO);
				} else if (temp_ahead.tag == "Blue") {
					string[] s = new string[]{"Red","Green"};
					ChangeCurrentColor (s, j_GO);
				} else {
					Debug.Log ("Does not exist");
				}
			} else {
				Debug.Log ("Does not exist");
			}
		} else {
			//Debug.Log ("Done");
		}
		checkBehind = true;
		checkAhead = false;
		if (ballsToBeChanged > 0) {
			if (checkBehind) {
				if(col_recurse)
				{
					CheckNeighbourColorsBehind (behind_index, behind);
				}
				else
				{
					CheckNeighbourColorsBehind(temp_behind_index,temp_behind);
				}
			}
		}
	}

		void CheckNeighbourColorsBehind(int j, GameObject j_GO)
		{
			int i=0;
			if (bc.Count < ballsToBeChanged)
			{
				ballsToBeChanged=bc.Count;
			}
			foreach (GameObject b in bc)
			{
				i++;
				if(i==j-1)
				{
					if(b.gameObject!=null)
					{
						ahead=b.gameObject;
						ahead_index=i;
					}
					else
					{
						break;
					}
				}
				if(i==j+1)
				{
					if(b.gameObject!=null)
					{
						behind=b.gameObject;
						behind_index=i;
						break;
					}
					else
					{
						break;
					}
				}
			}
			if (ballsToBeChanged > 0 && ahead != null && behind != null) {
				if (behind.tag == "Blue") {
					if (ahead.tag == "Green") {
						string[] s = new string[]{"Yellow","Red"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Red") {
						string[] s = new string[]{"Yellow","Green"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Yellow") {
						string[] s = new string[]{"Red","Green"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Blue") {
						string[] s = new string[]{"Yellow","Red","Green"};
						ChangeCurrentColor (s, j_GO);
					} else {
						Debug.Log ("Does not exist");
					}
				} else if (behind.tag == "Red") {
					if (ahead.tag == "Green") {
						string[] s = new string[]{"Yellow","Blue"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Red") {
						string[] s = new string[]{"Green","Yellow","Blue"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Yellow") {
						string[] s = new string[]{"Blue","Green"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Blue") {
						string[] s = new string[]{"Green","Yellow"};
						ChangeCurrentColor (s, j_GO);
					} else {
						Debug.Log ("Does not exist");
					}
				} else if (behind.tag == "Green") {
					if (ahead.tag == "Green") {
						string[] s = new string[]{"Yellow","Red","Blue"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Red") {
						string[] s = new string[]{"Blue","Yellow"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Yellow") {
						string[] s = new string[]{"Red","Blue"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Blue") {
						string[] s = new string[]{"Red","Yellow"};
						ChangeCurrentColor (s, j_GO);
					} else {
						Debug.Log ("Does not exist");
					}
				} else if (behind.tag == "Yellow") {
					if (ahead.tag == "Green") {
						string[] s = new string[]{"Red","Blue"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Red") {
						string[] s = new string[]{"Blue","Green"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Yellow") {
						string[] s = new string[]{"Red","Green","Blue"};
						ChangeCurrentColor (s, j_GO);
					} else if (ahead.tag == "Blue") {
						string[] s = new string[]{"Red","Green"};
						ChangeCurrentColor (s, j_GO);
					} else {
						Debug.Log ("Does not exist");
					}
				} else {
					Debug.Log ("Does not exist");
				}
			} else {
				Debug.Log ("Done");
			}
		checkAhead = true;
		checkBehind = false;
			if (ballsToBeChanged > 0) {
				if (checkAhead) {
				if(col_recurse)
				{
					CheckNeighbourColorsAhead (ahead_index, ahead);
				}
				else
				{
					CheckNeighbourColorsAhead(temp_ahead_index, temp_ahead);
				}
			}
				
			}
		}

}
