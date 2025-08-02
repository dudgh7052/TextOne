using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    [SerializeField] bool m_rightNPCFlag = false;
    [SerializeField] string m_dialogue = string.Empty;

    /// <summary>
    /// 오른쪽 NPC 플래그
    /// </summary>
    public bool IsRightNPCFlag { get { return m_rightNPCFlag; } }

    /// <summary>
    /// 대화 문자열
    /// </summary>
    public string IsDialogue { get { return m_dialogue; } }
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptable Objects/DialogueData")]
public class DialogueData : ScriptableObject
{
    [SerializeField] Sprite m_leftNPCSprite = null;
    [SerializeField] Sprite m_rightNPCSprite = null;

    [SerializeField] List<Dialogue> m_dialogueList = null;

    /// <summary>
    /// 대화 리스트
    /// </summary>
    public List<Dialogue> IsDialogueList { get { return m_dialogueList; } }


    /// <summary>
    /// 왼쪽 NPC 스프라이트
    /// </summary>
    public Sprite IsLeftNPCSprite { get { return m_leftNPCSprite; } }

    /// <summary>
    /// 오른쪽 NPC 스프라이트
    /// </summary>
    public Sprite IsRightNPCSprite { get { return m_rightNPCSprite; } }
}