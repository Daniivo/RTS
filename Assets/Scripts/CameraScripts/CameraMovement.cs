using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public float moveSpeed = 15;
	public float edgeGap = 5;
	public CameraZoom camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		handleMovement();
		handleCameraPosition();
	}

	void handleMovement() {
		if (Input.GetKey(KeyCode.W)) {
			transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
		} if (Input.GetKey(KeyCode.S)) {
			transform.position += Vector3.back * moveSpeed * Time.deltaTime;
		} if (Input.GetKey(KeyCode.A)) {
			transform.position += Vector3.left * moveSpeed * Time.deltaTime;
		} if (Input.GetKey(KeyCode.D)) {
			transform.position += Vector3.right * moveSpeed * Time.deltaTime;
		} 
	}

	void handleCameraPosition() {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit)) {
			camera.updateCamera(hit.point);
		}
	}
}
