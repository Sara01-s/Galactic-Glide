using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data"), Serializable]
public class Data : ScriptableObject {

	public Action OnPlayerDeath;
	public bool PlayerIsFrozen = true;
	public bool GameStarted = false;
    public Action<StartData> OnGameStart;
    public Action OnPlayerStartMoving;

	public float PlayerX;
    public ReactiveValue<float> PlayerDy = new () { Value = 0.0f };
    public ReactiveValue<int> PlayerScore = new () { Value = 0 };
	
	public void Reset() {
		GameStarted = false;
		PlayerX = -20.0f;
		PlayerDy = new () { Value = 0.0f };
		PlayerScore = new () { Value = 0 };
	}
}