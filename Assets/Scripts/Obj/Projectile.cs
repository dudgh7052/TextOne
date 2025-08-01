using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] TextMeshPro m_text = null;
    [SerializeField] float m_destroyTime = 3.0f;

    float m_moveSpeed = 0.0f;
    Vector3 m_moveDir = Vector3.zero;

    /// <summary>
    /// ������Ÿ�� ����
    /// </summary>
    /// <param name="argDir"></param>
    public void Setting(Vector3 argDir, float argSpeed,string argStr)
    {
        m_moveDir = argDir;
        m_moveSpeed = argSpeed;
        m_text.text = argStr;
    }

    void Update()
    {
        transform.position += m_moveSpeed * Time.deltaTime * m_moveDir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GManager.Instance.BoundaryBattleEnd();
            PoolManager.Instance.Return(gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {
            PoolManager.Instance.Return(gameObject);
        }
    }
}