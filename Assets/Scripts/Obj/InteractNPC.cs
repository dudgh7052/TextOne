using UnityEngine;

public class InteractNPC : MonoBehaviour
{
    [Header("스테이지 데이터")]
    [SerializeField] StageData m_stageData;

    [Header("상호작용 아이콘")]
    [SerializeField] GameObject m_interactIcon = null;

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