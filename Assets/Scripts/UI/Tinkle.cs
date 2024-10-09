using UnityEngine;
using TMPro;
using System;

public class Tinkle : MonoBehaviour {

    public static Action OnKeyPressed;

    [SerializeField] private TextMeshProUGUI m_textMeshPro;
    [SerializeField] private float m_tinkleInterval;

    private void Update() {
        if (Input.anyKeyDown) {
            OnKeyPressed?.Invoke();
        }
    }

    private void FixedUpdate() {
        m_textMeshPro.color = Time.time % m_tinkleInterval > 0.5f ? Color.white : Color.clear;
    }

}