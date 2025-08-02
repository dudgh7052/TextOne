using UnityEngine;

public class InteractNPC : MonoBehaviour
{
    [Header("�������� ������")]
    [SerializeField] StageData m_stageData;

    [Header("��ȣ�ۿ� ������")]
    [SerializeField] GameObject m_interactIcon = null;

    /// <summary>
    /// �������� ������
    /// </summary>
    public StageData IsStageData { get { return m_stageData; } }

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