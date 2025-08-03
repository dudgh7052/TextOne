using UnityEngine;

public class InteractNPC : MonoBehaviour
{
    [Header("스테이지 데이터")]
    [SerializeField] StageData m_stageData;

    [Header("스프라이트 렌더러")]
    [SerializeField] SpriteRenderer m_leftNPCSp = null;
    [SerializeField] SpriteRenderer m_rightNPCSp = null;

    [Header("상호작용 아이콘")]
    [SerializeField] GameObject m_interactIcon = null;

    /// <summary>
    /// 스테이지 데이터
    /// </summary>
    public StageData IsStageData { get { return m_stageData; } }

    void Start()
    {
        m_leftNPCSp.sprite = m_stageData.IsLeftSp;
        m_rightNPCSp.sprite = m_stageData.IsRightSp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_interactIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_interactIcon.SetActive(false);
        }
    }
}