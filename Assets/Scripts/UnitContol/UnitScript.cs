using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(MeshRenderer))]
public class UnitScript : MonoBehaviour {
	public Camera camera;
	public Texture hpTexture;
	public MeshRenderer selectionMesh;
	public float trueRange;
	public float attackSpeed;
	public float attackDamage;
	public float speed = 3.5f;
	public float maxHealth;

	NavMeshAgent navAgent;
	MeshRenderer meshRenderer;
	UnitScript target;
	float range;
	float health;
	float attackCountdown;

	// Use this for initialization
	void Start () {
		navAgent = GetComponent<NavMeshAgent>();
		meshRenderer = GetComponent<MeshRenderer>();
		range = Mathf.Pow(trueRange, 2);
		navAgent.speed = speed;
		health = maxHealth;
	}

	void OnGUI() {
		if (meshRenderer.isVisible) {
			Vector3 onScreen = camera.WorldToScreenPoint(transform.position);
			GUI.DrawTexture(new Rect(onScreen.x - 20, Screen.height - onScreen.y - 20, 40 * health / maxHealth, 5), hpTexture);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (attackCountdown > 0) {
			attackCountdown -= Time.deltaTime;
		}

		if (target != null) {
			if (getDistance(transform.position, target.transform.position) > range) {
				navAgent.SetDestination(target.transform.position);
				navAgent.Resume();
			} else {
				navAgent.Stop();
				if (attackCountdown <= 0) {
					attackCountdown = attackSpeed;
					target.attack(attackDamage);
				}
			}
		}
	}

	static float getDistance(Vector3 a, Vector3 b) {
		return Mathf.Pow((b.x - a.x), 2) + Mathf.Pow((b.y - a.y), 2) + Mathf.Pow((b.z - a.z), 2);
	}

	public void goTo(Vector3 point){
		target = null;
		navAgent.SetDestination(point);
		navAgent.Resume();
	}

	public void setTarget(UnitScript target) {
		this.target = target;
	}

	public void select() {
		selectionMesh.enabled = true;
	}

	public void desselect() {
		selectionMesh.enabled = false;
	}

	public void attack(float damage) {
		this.health -= damage;
		if (health <= 0) {
			GameObject.Destroy(this.gameObject);
		}
	}
}
