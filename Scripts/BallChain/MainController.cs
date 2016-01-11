using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour {

	public List<GameObject> matchingballs=new List<GameObject>();
	int ind=0,collided_ind,i=0,j=0;
	GameObject ahead,behind;
	GameObject sc;
	bool goAhead=false,goBehind=false;
	ArrayList bc;
	// Use this for initialization
	void Start () 
	{
		sc = GameObject.Find ("SpawnController");
		bc = sc.GetComponent<SpawnMarbles> ().ballChain;
	}
	
	// Update is called once per frame
	void Update ()
	{
			
	}

	void OnCollisionEnter(Collision col)
	{
		foreach(GameObject b in bc)
		{
			ind++;
			if(col.gameObject.name == b.gameObject.name)
			{
				collided_ind=ind;
				if(b.gameObject.tag==gameObject.tag)
				{
					matchingballs.Add(b.gameObject);
					matchingballs.Add(gameObject);
					if(collided_ind==0)
					{
						CheckNeighboursBehind(collided_ind, gameObject.tag);
					}
					else if(collided_ind==bc.Count-1)
					{
						CheckNeighboursAhead(collided_ind, gameObject.tag);
					}
					else
					{
						CheckNeighboursBehind(collided_ind , gameObject.tag);
						CheckNeighboursAhead(collided_ind, gameObject.tag);
					}
					GetTotalMatchingCount(collided_ind);
				}
			}
		}
	}

	void GetTotalMatchingCount(int c_i)
	{
		if (matchingballs.Count > 3) {
			foreach (GameObject m in matchingballs) {
				Destroy (m);
			}
		}
		else
		{
			AddCannonBallToChain(c_i);
		}
	}

	void AddCannonBallToChain(int c_index)
	{
		bc.Insert (c_index + 1, gameObject);
	}

	void CheckNeighboursBehind(int ci, string t)
	{
		i=0;
		foreach (GameObject b in bc) {
			i++;
			if (b != null) {
				if (i == ci + 1) {
					behind = b;
					if (behind.tag == t) {
						goBehind = true;
						matchingballs.Add (behind);
					} else {
						goBehind = false;
						break;
					}
				}
			}
			else{
				break;
			}
		}
		if(goBehind)
		{
			CheckNeighboursBehind(ci+1, t);
		}
	}

	void CheckNeighboursAhead(int ci, string t)
	{
		j = 0;
		foreach (GameObject b in bc) {
			j++;
			if (b != null) {
				if (i == ci - 1) {
					ahead = b;
					if (ahead.tag == t) {
						goAhead = true;
						matchingballs.Add (ahead);
					} else {
						goAhead = false;
						break;
					}
				}
			}
			else
			{
				break;
			}
		}
		if (goAhead)
		{
			CheckNeighboursAhead(ci-1,t);
		}
	}
}
