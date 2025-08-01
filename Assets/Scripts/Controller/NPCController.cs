using UnityEngine;

public class NPCController : MonoBehaviour
{
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