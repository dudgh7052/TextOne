using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI.Core;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] GameObject m_dialogueCanvasObj = null;

    [Header("TypeWriter")]
    [SerializeField] TypewriterCore m_leftNPCTypeWriter = null;
    [SerializeField] TypewriterCore m_rightNPCTypeWriter = null;

    [Header("NPC 설정")]
    [SerializeField] SpriteRenderer m_leftNPCPortraitSp = null;
    [SerializeField] SpriteRenderer m_rightNPCPortraitSp = null;
    [Space(10.0f)]
    [SerializeField] GameObject m_leftNPCDialoguePanel = null;
    [SerializeField] GameObject m_rightNPCDialoguePanel = null;

    bool m_typingFlag = false;
    Action m_endEvent = null;
    Queue<Dialogue> m_dialogueQueue = new Queue<Dialogue>();

    void Awake()
    {
        if (DialogueManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        if (!m_typingFlag && m_dialogueQueue.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.F)) DisplayNextDialogue();
        }
    }

    public void StartDialogue(DialogueData argDialogueData, Action argEndEvent = null)
    {
        // Dialogue 설정 및 이벤트 등록
        m_dialogueQueue.Clear();
        m_endEvent = argEndEvent;

        // Sprite 설정
        m_leftNPCPortraitSp.sprite = argDialogueData.IsLeftNPCSprite;
        m_rightNPCPortraitSp.sprite = argDialogueData.IsRightNPCSprite;

        foreach (Dialogue _dialogue in argDialogueData.IsDialogueList)
        {
            m_dialogueQueue.Enqueue(_dialogue);
        }

        m_dialogueCanvasObj.SetActive(true);

        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (m_typingFlag) return;

        if (m_dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue _curDialogue = m_dialogueQueue.Dequeue();
        StartCoroutine(TypeDialogue(_curDialogue));
    }

    IEnumerator TypeDialogue(Dialogue argDialogue)
    {
        m_typingFlag = true;

        if (!argDialogue.IsRightNPCFlag)
        {
            m_leftNPCDialoguePanel.SetActive(true);
            m_rightNPCDialoguePanel.SetActive(false);

            m_leftNPCTypeWriter.ShowText(argDialogue.IsDialogue);
            yield return new WaitUntil(() => !m_leftNPCTypeWriter.isShowingText);
        }
        else
        {
            m_leftNPCDialoguePanel.SetActive(false);
            m_rightNPCDialoguePanel.SetActive(true);

            m_rightNPCTypeWriter.ShowText(argDialogue.IsDialogue);
            yield return new WaitUntil(() => !m_rightNPCTypeWriter.isShowingText);
        }

        m_typingFlag = false;
    }

    void EndDialogue()
    {
        Debug.Log("End Dialogue");

        m_leftNPCDialoguePanel.SetActive(false);
        m_rightNPCDialoguePanel.SetActive(false);

        m_dialogueCanvasObj.SetActive(false);

        m_endEvent?.Invoke();
        m_endEvent = null;
    }
}