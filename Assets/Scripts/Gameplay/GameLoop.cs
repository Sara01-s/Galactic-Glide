using UnityEngine;

[System.Serializable]
public struct StartData {
    public float PlayerEntranceDurationSec;
}

public class GameLoop : Triggereable {
    
    [SerializeField] private Data _data;
    [Space(20.0f)]

    [SerializeField] private StartData _startData;
    [SerializeField] private AudioClip _deathSound;
    
    private IAudioService _audioService;

    private void Awake() {
        _audioService = Services.Instance.GetService<IAudioService>();
    }   

    public void StartGame() {
        _data.OnGameStart?.Invoke(_startData);
		_data.GameStarted = true;
    }

	public void ResetGame() {
        _audioService.PlaySound(_deathSound);

        _data.PlayerIsFrozen = true;
        _data.OnPlayerDeath?.Invoke();

        Invoke(nameof(ReloadScene), _deathSound.length);

	}

    private void ReloadScene() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

	private void OnDisable() {
		_data.Reset();
	}

    public override void Trigger() {
        ResetGame();
    }

}