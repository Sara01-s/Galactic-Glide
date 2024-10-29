using UnityEngine;

public class PlayerGraphics : MonoBehaviour {
    
    [SerializeField] private Data _data;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private ParticleSystem _particles;

    private void OnEnable() {
        _data.OnPlayerDeath += OnDeath;
    }
    private void OnDisable() {
        _data.OnPlayerDeath -= OnDeath;
    }

    private void OnDeath() {
        _sprite.color = Color.clear;
        _particles.Stop();
    }

}
