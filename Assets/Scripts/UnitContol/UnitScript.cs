using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitScript : MonoBehaviour {
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
}
