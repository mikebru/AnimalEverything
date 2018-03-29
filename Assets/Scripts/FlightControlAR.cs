using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class FlightControlAR : MonoBehaviour {

	private Vector3 currentHeading;
	private Vector3 currentDist;

	// Use this for initialization
	void Start () {
		currentHeading = transform.forward;
		currentDist = transform.position;
		GetComponent<Rigidbody> ().velocity = transform.forward * .5f;

		//StartCoroutine (DistanceCheck ());

	}
	
	// Update is called once per frame
	void Update () {


		// If the player has not touched the screen, we are done with this update.
		Touch touch;
		if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
		{
			return;
		}

		// Raycast against the location the player touched to search for planes.
		TrackableHit hit;
		TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
			TrackableHitFlags.FeaturePointWithSurfaceNormal;

		if (Frame.Raycast (touch.position.x, touch.position.y, raycastFilter, out hit)) {

			Vector3 flyPoint = hit.Pose.position + new Vector3 (0, .3f, 0);
			FlyTo (flyPoint, .5f);

		}


		/*
		if (Input.GetMouseButton (0)) {

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray.origin, ray.direction, out hit)) {



				if (hit.transform.tag == "apple") {
					Vector3 flyPoint = hit.point;
					FlyTo (flyPoint, 6);
				}
				else if(hit.transform.tag == "Ground")
				{

				}

			}
		}
		*/

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

			t += Time.deltaTime * 2;

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
			FlyTo (currentDist, .5f);
		}


		StartCoroutine (DistanceCheck ());

	}

}
