using UnityEngine;

public class Tinkle : MonoBehaviour {

    [SerializeField] private GameObject m_textMeshPro;
    [SerializeField] private float m_tinkleTime;

    private float m_timer;

    private void Awake() {
        m_tinkleTime = m_timer;
    }

    private void FixedUpdate() {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0.0f) {
            m_textMeshPro.SetActive(!m_textMeshPro.activeInHierarchy);
            m_timer = m_tinkleTime;
        }
    }

}