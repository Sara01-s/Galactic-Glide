using UnityEngine;

[CreateAssetMenu(menuName = "Data")]
public class Data : ScriptableObject {

    public ReactiveValue<float> PlayeYPosition = new () { Value = 5.0f };
    public ReactiveValue<int> PlayerScore = new () { Value = 0 };
    
}