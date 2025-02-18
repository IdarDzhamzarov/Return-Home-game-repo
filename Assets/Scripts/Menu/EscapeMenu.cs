using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject controlsPanel;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Start()
    {
        gameInput.OnEscapeButtonPressed += GameInput_OnEscapeButtonPressed;
    }

    private void GameInput_OnEscapeButtonPressed(object sender, System.EventArgs e)
    {
        menuPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
        Cursor.visible = false;
    }

    public void Controls()
    {
        menuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void Back()
    {
        controlsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
