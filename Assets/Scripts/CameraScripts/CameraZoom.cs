using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
	public float baseHeight = 15;
	public float minHeight = 10;
	public float maxHeight = 30;

	float steps;
	float zoom;
	Vector3 lastTarget;

	// Use this for initialization
	void Start () {
		steps = (maxHeight - minHeight) * 0.05f;
		zoom = baseHeight;
	}
	
	// Update is called once per frame
	void Update () {
		handleZoom();
	}

	void handleZoom() {
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll > 0 && zoom > minHeight) {
			zoom -= steps;
		} else if (scroll < 0 && zoom < maxHeight) {
			zoom += steps;
		}
	}

	public void updateCamera(Vector3 target) {
		transform.position = target + Vector3.up * zoom + Vector3.back * zoom;
		transform.LookAt(target);
	}
}
