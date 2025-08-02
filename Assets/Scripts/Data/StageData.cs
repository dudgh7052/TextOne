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
    [SerializeField] List<WordData> m_wordDatas = null;
}