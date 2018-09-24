using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using HedgehogTeam.EasyTouch;

public class Player : MonoBehaviour {
	[DllImport("o3d_audio")]
	public static extern int initO3d();	// 0 success 
	[DllImport("o3d_audio")]
	public static extern void deinitO3d();
	[DllImport("o3d_audio")]
	public static extern void playO3d();
	[DllImport("o3d_audio")]
	public static extern void stopO3d();
	[DllImport("o3d_audio")]
	public static extern void setListenerPosition(float x, float y, float z);
	[DllImport("o3d_audio")]
	public static extern void setAudioPosition(long id, float x, float y, float z);	// default id 0
	
	public  	Transform 				mTransform;
	private 	Transform 				mCamTransform;
	private 	Vector3 				mCamRot;
	private 	CharacterController 	mCharacterCtrl;
	private 	float 					mMoveSpeed = 3.0f;
	private 	float 					mYRotSpeed = 2.4f;
	private 	float 					mXRotSpeed = 5.0f;
	private 	float 					mGravity = 2.0f;

	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer>().enabled = false;	// hide player
		mTransform = this.transform;
		mCharacterCtrl = this.GetComponent<CharacterController> ();
		mCamTransform = Camera.main.transform;

        // camera follow player
		Vector3 pos = mTransform.position;
		mCamTransform.position = pos;
		mCamTransform.rotation = mTransform.rotation;
		mCamRot = mCamTransform.eulerAngles;

		// test so 
    #if (UNITY_iOS || UNITY_ANDROID)
		int ret = initO3d();
		playO3d();
		setAudioPosition(0, 0, 1.5f, 0);
		Debug.LogFormat("--- initO3d ret:{0}", ret);
    #endif

		// lock mouse
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
#if (UNITY_iOS || UNITY_ANDROID)
		MobileInput();
		// Debug.Log("EasyTouch setListenerPosition x = " + mTransform.position.x + ", z = " + mTransform.position.z);
		setListenerPosition(mTransform.position.x, mTransform.position.y, mTransform.position.z);
#else
		DesktopInput();
#endif
	}

	void MobileInput() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
		if (Input.GetKeyDown(KeyCode.Home)) {

		}

		// if (Input.touchCount == 1) {
		// 	if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).position.x > Screen.width/2) {
		// 		mCamRot.y += Input.GetAxis("Mouse X") * mYRotSpeed;
        //         mCamRot.x -= Input.GetAxis("Mouse Y") * mXRotSpeed;
		// 		mCamTransform.eulerAngles = mCamRot;
		// 		Vector3 camrot = mCamTransform.eulerAngles;
		// 		camrot.x = 0; 
		// 		camrot.z = 0;
		// 		mTransform.eulerAngles = camrot;
		// 	}
		// }

		float x = 0;
		float y = 0;
		float z = 0;
		y -= mGravity * Time.deltaTime;

		// move
		mCharacterCtrl.Move(mTransform.TransformDirection(new Vector3(x, y, z)));

		Vector3 pos = mTransform.position;
		mCamTransform.position = pos;

		Gesture current = EasyTouch.current;
		Debug.Log("EasyTouch current get current.type = " + current.type);
 		if (current == null) {
			Debug.Log("EasyTouch current is null");
 			return;
		 }
		if (current.type == EasyTouch.EvtType.On_Drag) {	// TODO
			Debug.Log("EasyTouch On_Drag");
			mCamRot.y += current.deltaPosition.x / Screen.width * mYRotSpeed;
			mCamRot.x -= current.deltaPosition.y / Screen.height * mXRotSpeed;
			mCamTransform.eulerAngles = mCamRot;
			Vector3 camrot = mCamTransform.eulerAngles;
			camrot.x = 0; 
			camrot.z = 0;
			mTransform.eulerAngles = camrot;
		}
	}

	void DesktopInput() {
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
		mCamTransform.position = pos;
	}
}
