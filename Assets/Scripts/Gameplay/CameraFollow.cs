using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] private Data _data;
	[SerializeField] private Transform _player;
	[SerializeField] private float _xOffset;
	[SerializeField] private Material _middleground;
	[SerializeField] private float _shakeDuration;
	[SerializeField] private float _shakeStrength;

	private bool _startFollowing;

	private void OnEnable() {
		_data.OnPlayerStartMoving += StartFollowingPlayer;
		_middleground.mainTextureOffset = Vector2.zero;
		_data.OnPlayerDeath += Shake;
	}

	private void OnDisable() {
		_data.OnPlayerStartMoving -= StartFollowingPlayer;
		_startFollowing = false;
		_data.OnPlayerDeath -= Shake;
	}

	private void StartFollowingPlayer() {
		_startFollowing = true;
	}

	private void LateUpdate() {
		if (!_startFollowing) {
			return;
		}

		transform.position = new Vector3(_player.position.x + _xOffset, 0.0f, -10.0f);

		_middleground.mainTextureOffset = new Vector2(transform.position.x / 100.0f, 0.0f);
	}

	private void Shake() {
		_startFollowing = false;

		StartCoroutine(Shake());

        IEnumerator Shake() {
            float elapsed = 0.0f;

            while (elapsed < _shakeDuration) {
                transform.position += Random.insideUnitSphere * _shakeStrength;

                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }

}