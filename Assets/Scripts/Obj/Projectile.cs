using UnityEngine;

public class Projectile : MonoBehaviour
{
    float m_moveSpeed = 0.0f;
    Vector3 m_moveDir = Vector3.zero;

    /// <summary>
    /// 프로젝타일 셋팅
    /// </summary>
    /// <param name="argDir"></param>
    public void Setting(Vector3 argDir, float argSpeed)
    {
        m_moveDir = argDir;
        m_moveSpeed = argSpeed;
    }

    void Update()
    {
        transform.position += m_moveSpeed * Time.deltaTime * m_moveDir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PoolManager.Instance.Return(gameObject);
        }
    }
}
