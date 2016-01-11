using UnityEngine;
using System.Collections;

public class TargettingLineScript : MonoBehaviour {

	GameObject child;
	LineRenderer lineRenderer;
	float normalDistance = 20f;
	Vector3 verticalOffset;
	Vector3 verticalOffsetLine;
	// Use this for initialization
	void Start ()
	{
		lineRenderer = GetComponentInChildren<LineRenderer> ();
		normalDistance = 20f;
		verticalOffset = Vector3.up * 0.2f;
		verticalOffsetLine = Vector3.up * 2f;
		lineRenderer.SetPosition(0, transform.position + verticalOffsetLine);
	}
	
	// Update is called once per frame
	void Update () 
	{
		LineTargetting ();
	}

	void LineTargetting()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position + verticalOffset, -transform.forward, out hit, 70f)) 
		{
			lineRenderer.enabled=true;
			lineRenderer.SetPosition(1, hit.point + verticalOffsetLine);
			if(hit.collider.gameObject.tag=="Boundary")
			{
				//Vector3 pos = Vector3.Reflect (hit.point - this.transform.position, hit.normal);
				//lineRenderer.SetPosition(2, pos);
			}
		}
		else {
			lineRenderer.SetPosition(1, transform.position + verticalOffsetLine - transform.forward * normalDistance);
		}
	}
}
