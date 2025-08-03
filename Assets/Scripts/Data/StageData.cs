using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] string m_correctWord = string.Empty;

    [Header("SD ĳ���� ��������Ʈ")]
    [SerializeField] Sprite m_leftSp = null;
    [SerializeField] Sprite m_rightSp = null;

    [Header("�������� �߻� Ÿ��")]
    [SerializeField] ShooterType.TYPE m_stageShooterType = ShooterType.TYPE.None;

    [Header("�������� ��ȭ ������")]
    [SerializeField] DialogueData m_startDialogueData = null;
    [SerializeField] DialogueData m_endDialogueData = null;

    [Header("��Ʋ���� �����ϴ� �ܾ� ����Ʈ")]
    [SerializeField] List<WordData> m_wordDataList = null;

    /// <summary>
    /// Left NPC ��������Ʈ
    /// </summary>
    public Sprite IsLeftSp { get { return m_leftSp; } }

    /// <summary>
    /// Right NPC ��������Ʈ
    /// </summary>
    public Sprite IsRightSp { get { return m_rightSp; } }

    /// <summary>
    /// �ܾ�
    /// </summary>
    public string IsCorrectWord { get { return m_correctWord; } }

    /// <summary>
    /// �������� �߻� Ÿ��
    /// </summary>
    public ShooterType.TYPE IsStageShooterType { get { return m_stageShooterType; } }

    /// <summary>
    /// ���� ��ȭ ������
    /// </summary>
    public DialogueData IsStartDialogueData { get { return m_startDialogueData; } }

    /// <summary>
    /// �� ��ȭ ������
    /// </summary>
    public DialogueData IsEndDialogueData { get { return m_endDialogueData; } }

    /// <summary>
    /// �ܾ� ������
    /// </summary>
    public List<WordData> IsWordDataList { get { return m_wordDataList; } }
}