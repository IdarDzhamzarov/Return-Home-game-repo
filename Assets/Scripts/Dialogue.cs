using UnityEngine;
using MeetAndTalk;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueContainerSO dialogueContainerSO;

    private void Start()
    {
        DialogueManager.Instance.SetupDialogue(dialogueContainerSO);
        DialogueManager.Instance.StartDialogue(dialogueContainerSO);
    }
}
