using UnityEngine;

public class CircularProjectileShooter : MonoBehaviour
{
    [SerializeField] float m_maxShootTime = 0.0f;
    [SerializeField] GameObject m_circularProjectileBase = null;
    [SerializeField] Vector3 m_spawnPos = Vector3.zero;

    float m_curShootTime = 0.0f;

    void Update()
    {
        Tick();
    }

    void Tick()
    {
        m_curShootTime -= Time.deltaTime;
        if (m_curShootTime <= 0.0f)
        {
            m_curShootTime = m_maxShootTime;
            PoolManager.Instance.Get(m_circularProjectileBase).transform.position = m_spawnPos;
        }
    }
}