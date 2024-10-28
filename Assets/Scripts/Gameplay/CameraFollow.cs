using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] private Data _data;
	[SerializeField] private Transform _player;
	[SerializeField] private float _xOffset;

	private bool _startFollowing;

	private void OnEnable() {
		_data.OnPlayerStartMoving += StartFollowingPlayer;
	}

	private void OnDisable() {
		_data.OnPlayerStartMoving -= StartFollowingPlayer;
		_startFollowing = false;
	}

	private void StartFollowingPlayer() {
		_startFollowing = true;
	}

	private void LateUpdate() {
		if (!_startFollowing) {
			return;
		}

		transform.position = new Vector3(_player.position.x + _xOffset, 0.0f, -10.0f);
	}
}