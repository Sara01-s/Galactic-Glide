using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [SerializeField] private Data _data;

    public void SetPlayerDy(float dy) {
        _data.PlayerDy.Value = dy;
    }

}