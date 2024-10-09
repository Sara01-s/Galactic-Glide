using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_gameplay;

    private void OnEnable() {
        Tinkle.OnKeyPressed += OnAnyKeyPressed;
    }

    private void OnDisable() {
        Tinkle.OnKeyPressed -= OnAnyKeyPressed;
    }

    private void OnAnyKeyPressed() {
        m_mainMenu.SetActive(false);
        m_gameplay.SetActive(true);
    }

}