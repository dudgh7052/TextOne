using System.Collections;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; private set; } = null;

    [Header("�ִ� ü��")]
    [SerializeField] int m_maxHp = 4;

    StageData m_curStageData = null;

    [SerializeField] GameObject m_nonBattleObjs = null;
    [SerializeField] GameObject m_battleObjs = null;

    [Header("�ܾ� ����")]
    [SerializeField] Transform m_spawnT = null;
    [SerializeField] GameObject m_wordPrefab = null;

    int m_curHp = 0;

    #region Properties
    /// <summary>
    /// ���ͷ��� �÷���
    /// </summary>
    public bool IsBoundaryBattleFlag { get; set; } = false;

    /// <summary>
    /// ���ӿ��� �÷���
    /// </summary>
    public bool IsGameOverFlag { get; set; } = false;

    /// <summary>
    /// ���� �������� ������
    /// </summary>
    public StageData IsCurStageData { get { return m_curStageData; } }

    /// <summary>
    /// �÷��̾� Ʈ������
    /// </summary>
    public Transform IsPlayerT { get; set; }

    /// <summary>
    /// �ٿ���� �÷��̾� Ʈ������
    /// </summary>
    public Transform IsBoundaryPlayerT { get; set; }

    /// <summary>
    /// �÷��̾� ��ũ��Ʈ
    /// </summary>
    public PlayerController IsPlayerSc { get; set; }

    /// <summary>
    /// �ٿ���� �÷��̾� ��ũ��Ʈ
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
    /// ���� ��Ʋ ����
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
    /// ��Ʋ ���� ��ȭ ����
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
    /// ���� ��Ʋ ����
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
        //GManager.Instance.CreateWord(); �̰� �߻� ������ ������ �ҷ�����
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