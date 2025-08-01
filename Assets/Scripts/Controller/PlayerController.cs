using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동속도")]
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
            // 레이어 체크해서 npc면 Interact 시작

            Debug.Log("Interact Start");
            GManager.Instance.IsInteractFlag = true;
        }
    }
}