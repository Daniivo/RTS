using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class UnitManager : MonoBehaviour {
	public Texture selectionTexture;

	Vector3 startingPoint;
	Rect rectangle;
	Rect selectionRectangle;
	bool selecting = false;
	Camera camera;

	List<UnitScript> selectedUnitList;
	public List<UnitScript> unitList;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		selectedUnitList = new List<UnitScript>();
		unitList = new List<UnitScript>();
	}
	
	// Update is called once per frame
	void Update () {
		handleMouseInput();
	}

	void OnGUI() {
		drawRectangle();
	}

	void handleMouseInput() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			startingPoint = Input.mousePosition;
			selecting = true;
		} else if (Input.GetKeyUp(KeyCode.Mouse0)) {
			selecting = false;
			if (startingPoint == Input.mousePosition) {
				RaycastHit hit;
				if (Physics.Raycast(camera.ScreenPointToRay(new Vector3(startingPoint.x, startingPoint.y, camera.nearClipPlane)), out hit)) {
					Debug.DrawLine(transform.position, hit.point, Color.red, 1);
					if (hit.collider.tag.Equals("Unit")) {
						if (Input.GetKey(KeyCode.LeftControl)) {
							selectUnit(hit.collider.GetComponent<UnitScript>());
						} else {
							clearSelection();
							selectUnit(hit.collider.GetComponent<UnitScript>());
						}
					} else clearSelection();
				} else clearSelection();
			} else {
				selectionRectangle = new Rect(startingPoint.x, startingPoint.y, Input.mousePosition.x - startingPoint.x, Input.mousePosition.y - startingPoint.y);
				if (Input.GetKey(KeyCode.LeftControl)) {
					checkSelection(selectionRectangle);
				} else {
					clearSelection();
					checkSelection(selectionRectangle);
				}
			}
		} else if (Input.GetKeyDown(KeyCode.Mouse1)) {
			RaycastHit hit;
			if (Physics.Raycast(camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane)), out hit)) {
				Debug.DrawLine(transform.position, hit.point, Color.red, 1);
				foreach (UnitScript u in selectedUnitList)
					u.goTo(hit.point);
			}
		}
	}

	void checkSelection(Rect r) {
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Unit")) {
			UnitScript u = g.GetComponent<UnitScript>();
			if (r != null && r.Contains(camera.WorldToScreenPoint(u.transform.position), true))
				selectUnit(u);
		}
	}

	void drawRectangle() {
		if (selecting) {
			rectangle = new Rect(startingPoint.x, Screen.height - startingPoint.y, Input.mousePosition.x - startingPoint.x, -1 * ((Screen.height - startingPoint.y) - (Screen.height - Input.mousePosition.y)));
			GUI.DrawTexture(rectangle, selectionTexture);
		}
	}

	public void selectUnit(UnitScript u) {
		u.select();
		selectedUnitList.Add(u);
	}

	public void desselectUnit(UnitScript u) {
		u.desselect();
		selectedUnitList.Remove(u);
	}

	void clearSelection() {
		foreach (UnitScript u in selectedUnitList)
			u.desselect();
		selectedUnitList.Clear();

	}
}
