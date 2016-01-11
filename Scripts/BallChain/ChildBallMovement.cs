using UnityEngine;
using System.Collections;

public class ChildBallMovement : MonoBehaviour {
	float percentPerSecond=0.05f;
	float currPathPercent=0.0f;
	GameObject sc;
	ArrayList bc;
	int collided_index;
	int ind=0,i=0;
	
	public bool moveFlag = true;
	// Use this for initialization
	void Start ()
	{
		sc = GameObject.Find ("SpawnController");
		bc = sc.GetComponent<SpawnMarbles> ().ballChain;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (moveFlag) {
			//Transform[] sm = sc.GetComponent<SpawnMarbles> ().waypointArray;
			currPathPercent += percentPerSecond * Time.deltaTime;
			//iTween.PutOnPath (gameObject, sm, currPathPercent);
		}
	}
	
	
//	void OnCollisionEnter(Collision col)
//	{
//		
//		foreach(GameObject b in bc)
//		{
//			ind++;
//			if(col.gameObject.name== b.gameObject.name)
//			{
//				ContactPoint contact=col.contacts[0];
//				Debug.Log("Collided");
//				
//				Rigidbody other=gameObject.GetComponent<Rigidbody>();
//				collided_index=ind;
//				b.gameObject.AddComponent<HingeJoint>();
//				b.gameObject.GetComponent<HingeJoint>().connectedBody=other;
//				b.gameObject.GetComponent<HingeJoint>().anchor=contact.point;
//				//GetComponent<HingeJoint>().axis.y=1;
//				Vector3 ax=b.gameObject.GetComponent<HingeJoint>().axis;
//				ax.x=1;
//				b.gameObject.GetComponent<HingeJoint>().axis = ax;
//				b.GetComponent<BallChainMovement>().moveFlag=true;
////				MoveInFront ();
//			}
//		}
//		
//	}
//	
//	void MoveInFront()
//	{
//		foreach (GameObject ba in bc) 
//		{
//			i++;
//			if(i < ind)
//			{
//				ba.GetComponent<BallChainMovement>().moveFlag=true;
////				gameObject.GetComponent<BallChainMovement>().enabled=false;
//			}
//		}
//	}
}
