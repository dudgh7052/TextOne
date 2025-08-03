using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] string m_correctWord = string.Empty;

    [Header("SD 캐릭터 스프라이트")]
    [SerializeField] Sprite m_leftSp = null;
    [SerializeField] Sprite m_rightSp = null;

    [Header("스테이지 발사 타입")]
    [SerializeField] ShooterType.TYPE m_stageShooterType = ShooterType.TYPE.None;

    [Header("스테이지 대화 데이터")]
    [SerializeField] DialogueData m_startDialogueData = null;
    [SerializeField] DialogueData m_endDialogueData = null;

    [Header("배틀에서 등장하는 단어 리스트")]
    [SerializeField] List<WordData> m_wordDataList = null;

    /// <summary>
    /// Left NPC 스프라이트
    /// </summary>
    public Sprite IsLeftSp { get { return m_leftSp; } }

    /// <summary>
    /// Right NPC 스프라이트
    /// </summary>
    public Sprite IsRightSp { get { return m_rightSp; } }

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