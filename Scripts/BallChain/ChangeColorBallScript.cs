using UnityEngine;
using System.Collections;

public class ChangeColorBallScript : MonoBehaviour {

	string[] colorBalls=new string[]{"Red","Yellow","Green","Blue"};
	public Texture red_Tex, green_Tex, blue_Tex, yellow_Tex;
	public int ballsToBeChanged;
	ArrayList bc;
	GameObject sc, collided_GO,behind,ahead,beh, ahe;
	int i=0, collided_ind,k,m,behind_ind,ahead_ind,beh_index,ahe_index;
	// Use this for initialization
	void Start ()
	{
		sc = GameObject.Find ("SpawnController");
		bc = sc.GetComponent<SpawnMarbles> ().ballChain;
	}

	void OnCollisionEnter(Collision col)
	{
		foreach (GameObject ba in bc) 
		{
			i++;
			if (col.gameObject.name == ba.gameObject.name) 
			{
				collided_ind = i;
				collided_GO=ba.gameObject;
				collided_GO.tag=colorBalls[Random.Range(0,colorBalls.Length)];
				if(collided_GO.tag=="Red")
				{
					collided_GO.GetComponent<Renderer>().material.mainTexture=red_Tex;
				}
				else if(collided_GO.tag=="Yellow")
				{
					collided_GO.GetComponent<Renderer>().material.mainTexture=yellow_Tex;
				}
				else if(collided_GO.tag=="Blue")
				{
					collided_GO.GetComponent<Renderer>().material.mainTexture=blue_Tex;
				}
				else
				{
					collided_GO.GetComponent<Renderer>().material.mainTexture=green_Tex;
				}
			}
		}
		ballsToBeChanged--;
		if (ballsToBeChanged > 0) {
			ChangeColor(collided_ind);
		}
	}

	void ChangeColor(int j)
	{
		k = 0;
		foreach (GameObject ba in bc) 
		{
			k++;
			if(k==j+1)
			{
				behind=ba.gameObject;
				behind_ind=k;
			}
			if(k==j-1)
			{
				ahead=ba.gameObject;
				ahead_ind=k;
			}
		}
		if (behind != null && ballsToBeChanged > 0) {
			behind.tag = colorBalls [Random.Range (0, colorBalls.Length)];
			if(behind.tag=="Red")
			{
				behind.GetComponent<Renderer>().material.mainTexture=red_Tex;
			}
			else if(behind.tag=="Yellow")
			{
				behind.GetComponent<Renderer>().material.mainTexture=yellow_Tex;
			}
			else if(behind.tag=="Blue")
			{
				behind.GetComponent<Renderer>().material.mainTexture=blue_Tex;
			}
			else
			{
				behind.GetComponent<Renderer>().material.mainTexture=green_Tex;
			}
			ballsToBeChanged--;
		} 
		else
		{
			Debug.Log ("Stop recursion");
		}
		if (ahead != null && ballsToBeChanged > 0) {
			ahead.tag = colorBalls [Random.Range (0, colorBalls.Length)];
			if(ahead.tag=="Red")
			{
				ahead.GetComponent<Renderer>().material.mainTexture=red_Tex;
			}
			else if(ahead.tag=="Yellow")
			{
				ahead.GetComponent<Renderer>().material.mainTexture=yellow_Tex;
			}
			else if(ahead.tag=="Blue")
			{
				ahead.GetComponent<Renderer>().material.mainTexture=blue_Tex;
			}
			else
			{
				ahead.GetComponent<Renderer>().material.mainTexture=green_Tex;
			}
			ballsToBeChanged--;
		}
		else
		{
			Debug.Log ("Stop recursion");
		}
		if (ballsToBeChanged > 0)
		{
			ChangeColorAlternate(ahead_ind,behind_ind);
		}

	}

	void ChangeColorAlternate(int a, int b)
	{
		m = 0;
		foreach(GameObject ba in bc)
		{
			m++;
			if(m==a-1)
			{
				ahe=ba.gameObject;
				ahe_index=m;
			}
			if(m==b+1)
			{
				beh=ba.gameObject;
				beh_index=m;
			}
		}
		if(ahe!=null && ballsToBeChanged>0)
		{
			ahe.tag = colorBalls[Random.Range(0,colorBalls.Length)];
			if(ahe.tag=="Red")
			{
				ahe.GetComponent<Renderer>().material.mainTexture=red_Tex;
			}
			else if(ahe.tag=="Yellow")
			{
				ahe.GetComponent<Renderer>().material.mainTexture=yellow_Tex;
			}
			else if(ahe.tag=="Blue")
			{
				ahe.GetComponent<Renderer>().material.mainTexture=blue_Tex;
			}
			else
			{
				ahe.GetComponent<Renderer>().material.mainTexture=green_Tex;
			}
			ballsToBeChanged--;
		}
		else
		{
			Debug.Log ("Stop recursion");
		}
		if(beh!=null && ballsToBeChanged >0)
		{
			beh.tag = colorBalls [Random.Range (0, colorBalls.Length)];
			if(beh.tag=="Red")
			{
				beh.GetComponent<Renderer>().material.mainTexture=red_Tex;
			}
			else if(beh.tag=="Yellow")
			{
				beh.GetComponent<Renderer>().material.mainTexture=yellow_Tex;
			}
			else if(beh.tag=="Blue")
			{
				beh.GetComponent<Renderer>().material.mainTexture=blue_Tex;
			}
			else
			{
				beh.GetComponent<Renderer>().material.mainTexture=green_Tex;
			}
			ballsToBeChanged--;
		}
		else
		{
			Debug.Log("Stop recursion");
		}
		if(ballsToBeChanged>0)
		{
			ChangeColorAlternate(ahe_index,beh_index);
		}
	}


	// Update is called once per frame
	void Update () 
	{
	
	}
}
