using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data")]
public class Data : ScriptableObject {

    public Action<StartData> OnGameStart;

    public ReactiveValue<float> PlayerDy = new () { Value = 0.0f };
    public ReactiveValue<int> PlayerScore = new () { Value = 0 };
    
}