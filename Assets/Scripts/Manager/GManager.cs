using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; private set; } = null;

    StageData m_curStageData = null;

    [SerializeField] GameObject m_nonBattleObjs = null;
    [SerializeField] GameObject m_battleObjs = null;

    /// <summary>
    /// 인터렉팅 플래그
    /// </summary>
    [SerializeField] public bool IsBoundaryBattleFlag { get; set; } = false;

    /// <summary>
    /// 현재 스테이지 데이터
    /// </summary>
    public StageData IsCurStageData { get { return m_curStageData; } }

    /// <summary>
    /// 바운더리 플레이어 트랜스폼
    /// </summary>
    public Transform IsBoundaryPlayerT { get; set; }

    void Awake()
    {
        if (GManager.Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// 범위 배틀 시작
    /// </summary>
    public void BoundaryBattleStart(StageData argStageData)
    {
        if (IsBoundaryBattleFlag) return;

        IsBoundaryBattleFlag = true;
        m_curStageData = argStageData;

        m_nonBattleObjs.SetActive(false);
        m_battleObjs.SetActive(true);

        if (m_curStageData != null) DialogueManager.Instance.StartDialogue(m_curStageData.IsStartDialogueData, StartBattle);
    }

    /// <summary>
    /// 배틀 종료 대화 시작
    /// </summary>
    public void StartEndDialogue()
    {
        if (m_curStageData == null || !IsBoundaryBattleFlag) return;

        DialogueManager.Instance.StartDialogue(m_curStageData.IsEndDialogueData, BoundaryBattleEnd);
    }

    void StartBattle()
    {
        if (m_curStageData == null) return;

        IsBoundaryBattleFlag = true;
        BattleManager.Instance.SettingBattle(m_curStageData.IsWordDataList, m_curStageData.IsStageShooterType);
    }

    /// <summary>
    /// 범위 배틀 종료
    /// </summary>
    public void BoundaryBattleEnd()
    {
        IsBoundaryBattleFlag = false;

        m_battleObjs.SetActive(false);
        m_nonBattleObjs.SetActive(true);

        BattleManager.Instance.InitBattle();
    }
}