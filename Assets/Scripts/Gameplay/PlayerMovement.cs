using static Unity.Mathematics.math;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] private Data _data;

    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

	[SerializeField] private float _speed;
    [SerializeField] private float _minHeight = -10.0f;
    [SerializeField] private float _maxHeight = 10.0f;

	private void Start() {
		transform.position = _start.position;
	}

	private void OnEnable() {
		_data.OnGameStart += IntroducePlayer;
	}

    private void OnDisable() {
        _data.PlayerDy.Dispose();
		_data.OnGameStart -= IntroducePlayer;
    }

    public void Move(float dy) {
		if (_data.PlayerIsFrozen) {
			return;
		}

        float playerAltitude = lerp(_minHeight, _maxHeight, dy);
        transform.position = new Vector3(transform.position.x, playerAltitude);
    }

	private IEnumerator MoveForward() {
		_data.OnPlayerStartMoving?.Invoke();

		while (_data.GameStarted && !_data.PlayerIsFrozen) {
			transform.position += _speed * Time.deltaTime * Vector3.right;
			_data.PlayerX = transform.position.x;
			yield return null;
		}
	}

    private void IntroducePlayer(StartData startData) {
		_data.PlayerIsFrozen = true;
        StartCoroutine(IntroducePlayer(startData.PlayerEntranceDurationSec));

        IEnumerator IntroducePlayer(float entranceDurationSec) {
            float elapsed = 0.0f;

            while (elapsed <= entranceDurationSec) {
                float t = elapsed / entranceDurationSec;
                float dx = lerp(_start.position.x, _end.position.x, OutBack(t));

				transform.position = new Vector3(dx, transform.position.y);

                elapsed += Time.deltaTime;
                yield return null;
            }

       		_data.PlayerDy.Subscribe(Move);
			_data.PlayerIsFrozen = false;

			StartCoroutine(MoveForward());
        }
    }

	public static float OutBack(float t, float overshoot = 1.70158f) {
		t -= 1;
		return t * t * ((overshoot + 1) * t + overshoot) + 1;
	}

    
    private void OnDrawGizmos() {
        var pos = Vector3.zero;
        var maxOffset = new Vector3(pos.x, pos.y + _maxHeight);
        var minOffset = new Vector3(pos.x, pos.y + _minHeight);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos, maxOffset);
        Gizmos.DrawSphere(maxOffset, 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, minOffset);
        Gizmos.DrawSphere(minOffset, 0.5f);
    }

}