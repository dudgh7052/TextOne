using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; private set; } = null;

    StageData m_curStageData = null;

    [SerializeField] GameObject m_nonBattleObjs = null;
    [SerializeField] GameObject m_battleObjs = null;

    /// <summary>
    /// ���ͷ��� �÷���
    /// </summary>
    [field:SerializeField] public bool IsBoundaryBattleFlag { get; set; } = false;

    void Awake()
    {
        if (GManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// ���� ��Ʋ ����
    /// </summary>
    public void BoundaryBattleStart(StageData argStageData)
    {
        if (IsBoundaryBattleFlag) return;

        IsBoundaryBattleFlag = true;
        m_curStageData = argStageData;

        m_nonBattleObjs.SetActive(false);
        m_battleObjs.SetActive(true);

        if (m_curStageData != null) DialogueManager.Instance.StartDialogue(m_curStageData.IsDialogueData);
    }

    void StartBattle()
    {
        if (m_curStageData == null) return;
        BattleManager.Instance.SettingBattle(m_curStageData.IsWordDataList, m_curStageData.IsStageShooterType);
    }

    /// <summary>
    /// ���� ��Ʋ ����
    /// </summary>
    public void BoundaryBattleEnd()
    {
        IsBoundaryBattleFlag = false;

        m_battleObjs.SetActive(false);
        m_nonBattleObjs.SetActive(true);

        BattleManager.Instance.InitBattle();
    }
}