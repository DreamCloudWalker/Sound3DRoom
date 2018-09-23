using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : MonoBehaviour {
	public  	Transform 				mTransform;
	public 		float 					mRotationSpeed = 1.0f;


	// Use this for initialization
	void Start () {
		mTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		mTransform.Rotate(Vector3.down * mRotationSpeed, Space.World); 
	}
}
