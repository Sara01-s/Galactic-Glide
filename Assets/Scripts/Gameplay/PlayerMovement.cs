using static Unity.Mathematics.math;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] private Data _data;

    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

    [SerializeField] private float _minHeight = -10.0f;
    [SerializeField] private float _maxHeight = 10.0f;

    private void OnEnable() {
        _data.PlayerDy.Subscribe(Move);
        _data.OnGameStart += IntroducePlayer;
    }

    private void OnDisable() {
        _data.PlayerDy.Dispose();
    }

    public void Move(float dy) {
        float playerAltitude = lerp(_minHeight, _maxHeight, dy);
        transform.position = transform.position.withY(playerAltitude);
    }

    private void IntroducePlayer(StartData startData) {
        StartCoroutine(IntroducePlayer(startData.PlayerEntranceDurationSec));

        IEnumerator IntroducePlayer(float entranceDurationSec) {
            float elapsed = 0.0f;

            while (elapsed <= entranceDurationSec) {
                float t = elapsed / entranceDurationSec;
                float dx = lerp(_start.position.x, _end.position.x, pow(t, 5.0f));

                transform.position = transform.position.withX(dx);

                elapsed += Time.deltaTime;
                yield return null;
            }
        }
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