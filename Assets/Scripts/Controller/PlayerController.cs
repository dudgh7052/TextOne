using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�̵��ӵ�")]
    [SerializeField] float m_moveSpeed = 0.0f;

    Vector2 m_input = Vector2.zero;

    void Update()
    {
        if (GManager.Instance.IsInteractFlag) return;

        HandleInput();
        Move();
        HandleInteract();
    }

    void HandleInput()
    {
        m_input.x = Input.GetAxisRaw("Horizontal");
        m_input.y = Input.GetAxisRaw("Vertical");
        m_input.Normalize();
    }

    void Move()
    {
        if (m_input == Vector2.zero)
        {
            // Idle Animation;
            return;
        }

        transform.Translate(m_moveSpeed * Time.deltaTime * m_input);
    }

    void HandleInteract()
    {
        if (GManager.Instance.IsInteractFlag) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            // ���̾� üũ�ؼ� npc�� Interact ����

            Debug.Log("Interact Start");
            GManager.Instance.IsInteractFlag = true;
        }
    }
}