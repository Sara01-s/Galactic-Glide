using UnityEngine;

[System.Serializable]
public struct StartData {
    public float PlayerEntranceDurationSec;
}

public class GameLoop : MonoBehaviour {
    
    [SerializeField] private Data _data;
    [Space(20.0f)]

    [SerializeField] private StartData _startData;

    private void OnGameStart() {
        _data.OnGameStart?.Invoke(_startData);
    }



}