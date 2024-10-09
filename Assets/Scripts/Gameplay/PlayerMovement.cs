using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] private float _minHeight = -10.0f;
    [SerializeField] private float _maxHeight = 10.0f;
    [SerializeField] private float _playerYSpeed;
    [SerializeField] private Data _data;

    private IPlayerInput _playerInput;

    private void Awake() {
        _playerInput = new UnityPlayerInput();
    }

    private void Update() {
        float playerY = _playerInput.GetVerticalInput() * _playerYSpeed * Time.deltaTime;
        var playerPosition = new Vector3(0.0f, playerY);
        
        transform.position += playerPosition;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minHeight, _maxHeight));
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