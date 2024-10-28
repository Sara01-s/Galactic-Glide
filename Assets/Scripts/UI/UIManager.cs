using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

	[SerializeField] private Data m_data;
	[SerializeField] private GameEvent m_onAnyKeyPressed;
	[SerializeField] private GameLoop m_gameloop;
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_gameplay;
	[SerializeField] private TextMeshProUGUI m_tinkleText;
    [SerializeField] private float m_tinkleInterval;

    private void FixedUpdate() {
		if (!m_data.GameStarted) {
        	m_tinkleText.color = Time.time % m_tinkleInterval > 0.5f ? Color.white : Color.clear;
		}
    }

    private void Update() {
        if (Input.anyKeyDown && !m_data.GameStarted) {
            m_onAnyKeyPressed.Raise();
        }
    }

    public void OnAnyKeyPressed() {
		m_mainMenu.SetActive(false);
		m_gameplay.SetActive(true);
		m_gameloop.StartGame();
    }

}