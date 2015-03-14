using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour {
	public float baseHeight = 15;
	public float minHeight = 10;
	public float maxHeight = 30;
	public float moveSpeed = 15;
	public float edgeGap = 5;

	float steps;
	Camera camera;

	// Use this for initialization
	void Start () {
		steps = (maxHeight - minHeight) * 0.05f;
		camera = (Camera)this.GetComponent("Camera");
		camera.transform.position = new Vector3(0, baseHeight, -baseHeight);
	}
	
	// Update is called once per frame
	void Update () {
		handleZoom();
		handleMovement();
	}

	void handleZoom() {
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll > 0 && camera.transform.position.y > minHeight) {
			transform.position += transform.forward * steps;
		} else if (scroll < 0 && camera.transform.position.y < maxHeight) {
			transform.position += -transform.forward * steps;
		}
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
}
