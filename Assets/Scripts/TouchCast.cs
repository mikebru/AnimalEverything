using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCast : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButtonDown (0)) {

			// Construct a ray from the current touch coordinates
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			// Create a particle if hit
			if (Physics.Raycast (ray.origin, ray.direction, out hit)) {

				Debug.Log ("here");


				if (hit.transform.tag == "apple") {
					hit.transform.GetComponent<Rigidbody> ().isKinematic = false;
					hit.transform.GetComponent<Rigidbody> ().AddForce (Vector3.up * 50);
				}
			}
		}

	}
}
