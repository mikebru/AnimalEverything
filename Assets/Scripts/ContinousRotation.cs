﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousRotation : MonoBehaviour {

	public Vector3 RotationAxis;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Rotate (RotationAxis);

	}
}
