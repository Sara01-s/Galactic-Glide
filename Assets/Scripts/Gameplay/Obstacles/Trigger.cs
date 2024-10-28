using UnityEngine;

[System.Serializable]
public abstract class Triggereable : MonoBehaviour {

	public abstract void Trigger();

}

public class Trigger : MonoBehaviour {

	[SerializeField] private Data _data;
	[SerializeField] private Triggereable _target;
	
	private void Update() {
		if (_data.PlayerX > transform.position.x) {
			_target.Trigger();
			Destroy(gameObject);
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + transform.up * 200.0f);
		Gizmos.DrawLine(transform.position, transform.position - transform.up * 200.0f);
	}

}