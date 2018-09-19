using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Player : MonoBehaviour {
	// [DllImport("o3d_audio")]
	// public static extern int initO3d();	// 0 success 
	// [DllImport("o3d_audio")]
	// public static extern void deinitO3d();
	// [DllImport("o3d_audio")]
	// public static extern void playO3d();
	// [DllImport("o3d_audio")]
	// public static extern void stopO3d();
	// [DllImport("o3d_audio")]
	// public static extern void setListenerPosition(float x, float y, float z);
	// [DllImport("o3d_audio")]
	// public static extern void setAudioPosition(long id, float x, float y, float z);	// default id 0
	
	public  	Transform 				mTransform;
	private 	Transform 				mCamTransform;
	private 	Vector3 				mCamRot;
	private 	float 					mCamHeight = 1.4f;
	private 	CharacterController 	mCharacterCtrl;
	private 	float 					mMoveSpeed = 3.0f;
	private 	float 					mGravity = 2.0f;

	// Use this for initialization
	void Start () {
		mTransform = this.transform;
		mCharacterCtrl = this.GetComponent<CharacterController> ();
		mCamTransform = Camera.main.transform;

        // camera follow player
		Vector3 pos = mTransform.position;
		pos.y += mCamHeight;
		mCamTransform.position = pos;
		mCamTransform.rotation = mTransform.rotation;
		mCamRot = mCamTransform.eulerAngles;

		// lock mouse
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateControl ();
	}

	void UpdateControl() {
		// rot camera
		float mouseX = Input.GetAxis ("Mouse X");
		float mouseY = Input.GetAxis ("Mouse Y");
		mCamRot.x -= mouseY;
		mCamRot.y += mouseX;
		mCamTransform.eulerAngles = mCamRot;
		Vector3 camrot = mCamTransform.eulerAngles;
		camrot.x = 0; 
		camrot.z = 0;
		mTransform.eulerAngles = camrot;

		float x = 0;
		float y = 0;
		float z = 0;
		y -= mGravity * Time.deltaTime;

		if (Input.GetKey(KeyCode.W)) {
			z += mMoveSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.S)) {
			z -= mMoveSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.A)) {
			x -= mMoveSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.D)) {
			x += mMoveSpeed * Time.deltaTime;
		}
		// move
		mCharacterCtrl.Move(mTransform.TransformDirection(new Vector3(x, y, z)));

		Vector3 pos = mTransform.position;
		pos.y += mCamHeight;
		mCamTransform.position = pos;
	}
}
