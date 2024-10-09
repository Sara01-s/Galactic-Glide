using UnityEngine;
using TMPro;

public class Points : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI m_textMeshPro;
    [SerializeField] private Data m_data;

    private void OnEnable() {
        m_data.PlayerScore.Subscribe(UpdatePoints);
    }

    private void OnDisable() {
        m_data.PlayerScore.Dispose();
    }

    private void UpdatePoints(int a_points) {
        m_textMeshPro.text = a_points.ToString();
    }

}