using UnityEngine;

public class CircularProjectileShooter : MonoBehaviour
{
    [SerializeField] float m_maxShootTime = 0.0f;
    [SerializeField] GameObject m_circularProjectileBase = null;

    float m_curShootTime = 0.0f;

    public void Tick()
    {
        m_curShootTime -= Time.deltaTime;
        if (m_curShootTime <= 0.0f)
        {
            m_curShootTime = m_maxShootTime;
            PoolManager.Instance.Get(m_circularProjectileBase).transform.position = GManager.Instance.IsBoundaryPlayerT.position;
        }
    }
}