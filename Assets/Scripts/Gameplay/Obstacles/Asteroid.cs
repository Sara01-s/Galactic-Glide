using UnityEngine;

public class Asteroid : Triggereable {

	public override void Trigger() {
		gameObject.AddComponent<Rigidbody2D>().AddForce(Vector3.right * 10.0f, ForceMode2D.Impulse);
	}
}