using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitScript : MonoBehaviour {
	public MeshRenderer selectionMesh;

	NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goTo(Vector3 point){
		navAgent.SetDestination(point);
	}

	public void select() {
		selectionMesh.enabled = true;
	}

	public void desselect() {
		selectionMesh.enabled = false;
	}
}
