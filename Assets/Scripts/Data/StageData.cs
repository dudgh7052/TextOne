using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] string m_correctWord = string.Empty;

    [Header("스테이지 발사 타입")]
    [SerializeField] ShooterType.TYPE m_stageShooterType = ShooterType.TYPE.None;

    [Header("스테이지 대화 데이터")]
    [SerializeField] DialogueData m_startDialogueData = null;
    [SerializeField] DialogueData m_endDialogueData = null;

    [Header("배틀에서 등장하는 단어 리스트")]
    [SerializeField] List<WordData> m_wordDataList = null;

    /// <summary>
    /// 단어
    /// </summary>
    public string IsCorrectWord { get { return m_correctWord; } }

    /// <summary>
    /// 스테이지 발사 타입
    /// </summary>
    public ShooterType.TYPE IsStageShooterType { get { return m_stageShooterType; } }

    /// <summary>
    /// 시작 대화 데이터
    /// </summary>
    public DialogueData IsStartDialogueData { get { return m_startDialogueData; } }

    /// <summary>
    /// 끝 대화 데이터
    /// </summary>
    public DialogueData IsEndDialogueData { get { return m_endDialogueData; } }

    /// <summary>
    /// 단어 데이터
    /// </summary>
    public List<WordData> IsWordDataList { get { return m_wordDataList; } }
}