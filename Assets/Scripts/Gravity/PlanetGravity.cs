using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetGravity : MonoBehaviour {

	private float GravityForce = .1f;

	public bool AutoCalculateInitial;
	public bool ParentControl;


	private PlanetControl planetControl;
	private Rigidbody MyRigidbody;


	//for testing. Set the z value to give the planet an initial speed along it's z-axis
	public Vector3 intialForwardSpeed;

	// Use this for initialization
	void Start () {

		if (ParentControl == true) {
			planetControl = this.transform.GetComponentInParent<PlanetControl> ();
		} else {
			planetControl = GameObject.FindObjectOfType<PlanetControl> ();
		}


		if (AutoCalculateInitial == true) {
			intialForwardSpeed = planetControl.transform.position - this.transform.position;

			float distance = Vector3.Distance (planetControl.transform.position, this.transform.position);
			float massCombined = GetComponent<Rigidbody> ().mass;
				
				//planetControl.GetComponent<Rigidbody> ().mass;

			//intialForwardSpeed = Vector3.Cross(Vector3.one,intialForwardSpeed.normalized) * (GravityForce * ((massCombined))/(distance * distance));
			intialForwardSpeed = Vector3.Cross(Vector3.one,intialForwardSpeed.normalized) * (GravityForce * (massCombined));

			Debug.Log (intialForwardSpeed);
			//Debug.Log (this.transform.forward);

			//Debug.Log (intialForwardSpeed);
		}

		MyRigidbody = this.GetComponent<Rigidbody> ();
		MyRigidbody.velocity = transform.TransformDirection (intialForwardSpeed);
	
	}
		
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 pos = MyRigidbody.position;
		Vector3 acc = Vector3.zero;

		//more realistic gravity sim
		/*
		foreach (Rigidbody planet in planetControl.planets) {

			if (planet == MyRigidbody)
				continue;

			Vector3 direction = (planet.position - pos);
			acc += GravityForce * (direction.normalized * planet.mass)/ direction.sqrMagnitude;

		}*/

		Rigidbody sun = planetControl.GetComponent<Rigidbody> ();

		Vector3 direction = (sun.position - pos);
		acc += GravityForce * (direction.normalized * sun.mass)/ direction.sqrMagnitude;

		MyRigidbody.velocity += acc * Time.fixedDeltaTime;

	
	}
}
