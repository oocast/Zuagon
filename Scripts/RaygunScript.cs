using UnityEngine;
using System.Collections;

public class RaygunScript : MonoBehaviour {
	GameObject sc;
	public Texture red_tex, green_tex;
	ArrayList bc;
	LineRenderer line,line2;
	// Use this for initialization
	void Start () 
	{
		sc = GameObject.Find ("SpawnController");
		bc = sc.GetComponent<SpawnMarbles> ().ballChain;
		line = GetComponent<LineRenderer> ();
		line.useWorldSpace = true;
		line2 = GetComponent<LineRenderer> ();
		line2.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit hit;
		Ray a = new Ray (transform.position, transform.forward);
		Ray b;
		if (Deflect (a, out b, out hit)) {
			line.SetPosition (0, a.origin);
			line.SetPosition (1, hit.point);
			line2.SetPosition (0, b.origin);
			line2.SetPosition (1, b.origin + 3 * b.direction);
//			Debug.DrawLine(a.origin, hit.point);
//			Debug.DrawLine (b.origin, b.origin+3*b.direction);
		} 
		else
		{
			if(Physics.Raycast (a, out hit))
			{
				if(hit.collider.gameObject.tag=="Red" || hit.collider.gameObject.tag=="Yellow" || hit.collider.gameObject.tag=="Green" || hit.collider.gameObject.tag=="Blue")
				{
					line.SetPosition(0,a.origin);
					line.SetPosition(1,hit.point);
				}
			}
		}
	}

	bool Deflect(Ray ray, out Ray deflected, out RaycastHit hit)
	{
		if(Physics.Raycast (ray, out hit))
		{
			if(hit.collider.gameObject.tag=="Boundary")
			{
				Vector3 normal=hit.normal;
				Vector3 deflect=Vector3.Reflect(ray.direction, normal);
				deflected= new Ray(hit.point, deflect);
				return true;
			}
			else
			{
				deflected=new Ray(Vector3.zero, Vector3.zero);
				return false;
			}
		}
		deflected = new Ray (Vector3.zero, Vector3.zero);
		return false;
	}
}

