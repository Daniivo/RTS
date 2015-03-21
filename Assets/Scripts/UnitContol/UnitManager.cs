using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class UnitManager : MonoBehaviour {
	public Vector3 startingPoint;
	Camera camera;

	List<UnitScript> unitList;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		unitList = new List<UnitScript>(); 
	}
	
	// Update is called once per frame
	void Update () {
		handleMouseInput();
	}

	void handleMouseInput() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			startingPoint = Input.mousePosition;
		} else if (Input.GetKeyUp(KeyCode.Mouse0)) {
			if (startingPoint == Input.mousePosition) {
				RaycastHit hit;
				if (Physics.Raycast(camera.ScreenPointToRay(new Vector3(startingPoint.x, startingPoint.y, camera.nearClipPlane)), out hit)) {
					Debug.DrawLine(transform.position, hit.point, Color.red, 1);
					if (hit.collider.tag.Equals("Unit")) {
						if (Input.GetKey(KeyCode.LeftControl)) {
							unitList.Add(hit.collider.GetComponent<UnitScript>());
						} else {
							unitList.Add(hit.collider.GetComponent<UnitScript>());
						}
					} else unitList.Clear();
				} else unitList.Clear();
			}
		} else if (Input.GetKeyDown(KeyCode.Mouse1)) {
			RaycastHit hit;
			if (Physics.Raycast(camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane)), out hit)) {
				Debug.DrawLine(transform.position, hit.point, Color.red, 1);
				foreach (UnitScript u in unitList)
					u.goTo(hit.point);
			}
		}
	}
}
