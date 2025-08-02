using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] string m_correctWord = string.Empty;

    [Header("�������� �߻� Ÿ��")]
    [SerializeField] ShooterType.TYPE m_stageShooterType = ShooterType.TYPE.None;

    [Header("�������� ��ȭ ������")]
    [SerializeField] DialogueData m_startDialogueData = null;
    [SerializeField] DialogueData m_endDialogueData = null;

    [Header("��Ʋ���� �����ϴ� �ܾ� ����Ʈ")]
    [SerializeField] List<WordData> m_wordDataList = null;

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