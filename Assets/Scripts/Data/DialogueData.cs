using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    [SerializeField] bool m_rightNPCFlag = false;
    [SerializeField] string m_dialogue = string.Empty;

    /// <summary>
    /// ������ NPC �÷���
    /// </summary>
    public bool IsRightNPCFlag { get { return m_rightNPCFlag; } }

    /// <summary>
    /// ��ȭ ���ڿ�
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
    /// ��ȭ ����Ʈ
    /// </summary>
    public List<Dialogue> IsDialogueList { get { return m_dialogueList; } }


    /// <summary>
    /// ���� NPC ��������Ʈ
    /// </summary>
    public Sprite IsLeftNPCSprite { get { return m_leftNPCSprite; } }

    /// <summary>
    /// ������ NPC ��������Ʈ
    /// </summary>
    public Sprite IsRightNPCSprite { get { return m_rightNPCSprite; } }
}