using UnityEngine;

public class SelectedSpaceCapsuleVisual : MonoBehaviour
{
    [SerializeField] private SpaceCapsule spaceCapsule;
    [SerializeField] private Outline outlineEffect;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedSpaceCapsule == spaceCapsule)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        outlineEffect.OnEnable();
    }
    private void Hide()
    {
        outlineEffect.OnDisable();
    }
}
