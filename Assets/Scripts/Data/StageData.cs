using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [Header("�������� �߻� Ÿ��")]
    [SerializeField] ShooterType.TYPE m_stageShooterType = ShooterType.TYPE.None;

    [Header("�������� ��ȭ ������")]
    [SerializeField] DialogueData m_dialogueData = null;

    [Header("��Ʋ���� �����ϴ� �ܾ� ����Ʈ")]
    [SerializeField] List<WordData> m_wordDataList = null;

    /// <summary>
    /// �������� �߻� Ÿ��
    /// </summary>
    public ShooterType.TYPE IsStageShooterType { get { return m_stageShooterType; } }

    /// <summary>
    /// ��ȭ ������
    /// </summary>
    public DialogueData IsDialogueData { get { return m_dialogueData; } }

    /// <summary>
    /// �ܾ� ������
    /// </summary>
    public List<WordData> IsWordDataList { get { return m_wordDataList; } }
}