using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    void Awake()
    {
        if (DialogueManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    [Header("NPC ¼³Á¤")]
    [SerializeField] SpriteRenderer m_leftNPCPortraitSp = null;
    [SerializeField] SpriteRenderer m_rightNPCPortraitSp = null;
    [Space(10.0f)]
    [SerializeField] GameObject m_leftNPCDialoguePanel = null;
    [SerializeField] GameObject m_rightNPCDialoguePanel = null;
    [Space(10.0f)]
    [SerializeField] TextMeshProUGUI m_leftNPCText = null;
    [SerializeField] TextMeshProUGUI m_rightNPCText = null;

    bool m_typingFlag = false;
    Queue<Dialogue> m_dialogueQueue = new Queue<Dialogue>();

    public void StartDialogue(DialogueData argDialogueData)
    {

    }

    public void DisplayNextDialogue()
    {

    }
}