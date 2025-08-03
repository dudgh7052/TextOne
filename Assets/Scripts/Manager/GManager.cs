using System.Collections;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; private set; } = null;

    [Header("최대 체력")]
    [SerializeField] int m_maxHp = 4;

    StageData m_curStageData = null;

    [SerializeField] GameObject m_nonBattleObjs = null;
    [SerializeField] GameObject m_battleObjs = null;

    [Header("단어 설정")]
    [SerializeField] Transform m_spawnT = null;
    [SerializeField] GameObject m_wordPrefab = null;

    int m_curHp = 0;

    #region Properties
    /// <summary>
    /// 인터렉팅 플래그
    /// </summary>
    public bool IsBoundaryBattleFlag { get; set; } = false;

    /// <summary>
    /// 게임오버 플래그
    /// </summary>
    public bool IsGameOverFlag { get; set; } = false;

    /// <summary>
    /// 현재 스테이지 데이터
    /// </summary>
    public StageData IsCurStageData { get { return m_curStageData; } }

    /// <summary>
    /// 플레이어 트랜스폼
    /// </summary>
    public Transform IsPlayerT { get; set; }

    /// <summary>
    /// 바운더리 플레이어 트랜스폼
    /// </summary>
    public Transform IsBoundaryPlayerT { get; set; }

    /// <summary>
    /// 플레이어 스크립트
    /// </summary>
    public PlayerController IsPlayerSc { get; set; }

    /// <summary>
    /// 바운더리 플레이어 스크립트
    /// </summary>
    public BoundaryPlayerController IsBoundaryPlayerSc { get; set; }
    #endregion

    void Awake()
    {
        if (GManager.Instance == null)
        {
            Instance = this;
            Setting();
        }
        else Destroy(gameObject);
    }

    void Setting()
    {
        m_curHp = m_maxHp;
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

    public void CreateWord()
    {
        //GManager.Instance.CreateWord(); 이거 발사 끝나는 시점에 불러오기
        GameObject _word = Instantiate(m_wordPrefab, m_spawnT.position, Quaternion.identity);
        _word.GetComponent<WordProjectile>().Setting(m_curStageData.IsCorrectWord);
    }

    #region Damaged
    public void TakeDamage(int argDamage)
    {
        m_curHp -= argDamage;

        if (m_curHp <= 0)
        {
            m_curHp = 0;
            GameOver();
        }
        else
        {
            BoundaryBattleEnd();
        }
    }

    public void GameOver()
    {
        IsGameOverFlag = true;
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        yield return null;

        BoundaryBattleEnd();

        yield return new WaitForSeconds(1.0f);

        IsPlayerT.GetComponent<PlayerController>().PlayDeathAnimation();

        yield return new WaitForSeconds(1.0f);

        SceneMoveManager.Instance.ChangeScene("GameOver");
    }
    #endregion
}