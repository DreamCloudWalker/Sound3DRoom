using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Transform m_transform;
	private Transform m_camTransform;
	private Vector3 m_camRot;
	private float m_camHeight = 1.4f;
	private CharacterController m_ch;
	private float m_moveSpeed = 3.0f;
	private float m_gravity = 2.0f;

	// Use this for initialization
	void Start () {
		m_transform = this.transform;
		m_ch = this.GetComponent<CharacterController> ();
		m_camTransform = Camera.main.transform;

        // camera follow player
		Vector3 pos = m_transform.position;
		pos.y += m_camHeight;
		m_camTransform.position = pos;
		m_camTransform.rotation = m_transform.rotation;
		m_camRot = m_camTransform.eulerAngles;

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
		m_camRot.x -= mouseY;
		m_camRot.y += mouseX;
		m_camTransform.eulerAngles = m_camRot;
		Vector3 camrot = m_camTransform.eulerAngles;
		camrot.x = 0; camrot.z = 0;
		m_transform.eulerAngles = camrot;

		float x = 0, y = 0, z = 0;
		y -= m_gravity * Time.deltaTime;

		if (Input.GetKey(KeyCode.W)) {
			z += m_moveSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.S)) {
			z -= m_moveSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.A)) {
			x -= m_moveSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.D)) {
			x += m_moveSpeed * Time.deltaTime;
		}
		// move
		m_ch.Move (m_transform.TransformDirection(new Vector3(x, y, z)));

		Vector3 pos = m_transform.position;
		pos.y += m_camHeight;
		m_camTransform.position = pos;
	}

	void OnDrawGizmos() {
		Gizmos.DrawIcon (this.transform.position, "Spawn.tif");
	}
}
