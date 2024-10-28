using UnityEngine;
using TMPro;

public class Points : Triggereable {

    [SerializeField] private TextMeshProUGUI m_textMeshPro;
    [SerializeField] private Data m_data;

	[SerializeField] private VVTAudioPlayer _scoreSoundPlayer;

    private void OnEnable() {
        m_data.PlayerScore.Subscribe(UpdatePoints);
    }

    private void OnDisable() {
        m_data.PlayerScore.Dispose();
    }

    private void UpdatePoints(int a_points) {
        m_textMeshPro.text = a_points.ToString(format: "#0000") + "pts";
    }

	public override void Trigger() {
		_scoreSoundPlayer.Play();
		m_data.PlayerScore.Value++;
	}
}