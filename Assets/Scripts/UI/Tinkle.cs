using UnityEngine;
using TMPro;

public class Tinkle : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI m_textMeshPro;
    [SerializeField] private float m_tinkleInterval;

    private void FixedUpdate() {
        m_textMeshPro.color = Time.time % m_tinkleInterval > 0.5f ? Color.white : Color.clear;
    }

}