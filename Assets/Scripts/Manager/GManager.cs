using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; private set; } = null;

    [SerializeField] GameObject m_playerObj = null;

    [SerializeField] GameObject m_boundaryBox = null;
    [SerializeField] GameObject m_boundaryPlayerObj = null;

    /// <summary>
    /// ���ͷ��� �÷���
    /// </summary>
    public bool IsBoundaryBattleFlag { get; set; } = false;

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
    public void BoundaryBattleStart()
    {
        IsBoundaryBattleFlag = true;

        m_playerObj.SetActive(false);

        m_boundaryPlayerObj.transform.localPosition = Vector3.zero;
        m_boundaryBox.SetActive(true);
    }

    /// <summary>
    /// ���� ��Ʋ ����
    /// </summary>
    public void BoundaryBattleEnd()
    {
        IsBoundaryBattleFlag = false;

        m_boundaryPlayerObj.transform.localPosition = Vector3.zero;
        m_boundaryBox.SetActive(false);

        m_playerObj.SetActive(true);

    }
}