using UnityEngine;

public interface IObstacle {
	void Collide();
}

public class Obstacle : MonoBehaviour, IObstacle {




	public void Collide() {
		
	}
}