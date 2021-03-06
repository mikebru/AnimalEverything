﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightControl : MonoBehaviour {

	private Vector3 currentHeading;
	private Vector3 currentDist;

	// Use this for initialization
	void Start () {
		currentHeading = transform.forward;

		currentDist = transform.position;
		GetComponent<Rigidbody> ().velocity = transform.forward * 2;

		StartCoroutine (DistanceCheck ());

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray.origin, ray.direction, out hit)) {



				if (hit.transform.tag == "apple") {
					Vector3 flyPoint = hit.point;
					FlyTo (flyPoint, 3);
				}
				else if(hit.transform.tag == "Ground")
				{
					Vector3 flyPoint = hit.point + new Vector3 (0, 3f, 0);
					FlyTo (flyPoint, 2);
				}

			}

		}

	}

	public void FlyTo(Vector3 newDirection, float speed)
	{

		Vector3 newHeading = newDirection - this.transform.position;
		newHeading = newHeading.normalized;
		currentDist = newDirection;


		StartCoroutine (ChangeDirection (newHeading, speed));
	}


	IEnumerator ChangeDirection(Vector3 newHeading, float speed)
	{
		float t = 0;

		Vector3 startHeading = transform.forward;

		float magnitudeDifference = (newHeading - startHeading).magnitude;


		while (t < magnitudeDifference) {

			t += Time.deltaTime * speed;

			currentHeading = Vector3.Lerp (startHeading, newHeading, t / magnitudeDifference);
			transform.forward = currentHeading;
			//transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentHeading.x * 20));


			GetComponent<Rigidbody> ().velocity = transform.forward * speed;

			yield return new WaitForFixedUpdate ();
		}


	}


	IEnumerator DistanceCheck()
	{

		yield return new WaitForSeconds (.5f);

		if (Vector3.Distance (currentDist, transform.position) > .5f) {
			FlyTo (currentDist, 2);
		}


		StartCoroutine (DistanceCheck ());

	}

}
