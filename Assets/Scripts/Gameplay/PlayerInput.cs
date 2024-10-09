public class UnityPlayerInput : IPlayerInput {

    public float GetVerticalInput() {
        return UnityEngine.Input.GetAxis("Vertical");
    }

}