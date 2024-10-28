using UnityEngine;

[System.Serializable]
public struct StartData {
    public float PlayerEntranceDurationSec;
}

public class GameLoop : MonoBehaviour {
    
    [SerializeField] private Data _data;
    [Space(20.0f)]

    [SerializeField] private StartData _startData;

    public void StartGame() {
        _data.OnGameStart?.Invoke(_startData);
		_data.GameStarted = true;
    }

	public void ResetGame() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	private void OnDisable() {
		_data.Reset();
	}

}