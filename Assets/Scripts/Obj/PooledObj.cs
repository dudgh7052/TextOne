using UnityEngine;

public class PooledObj : MonoBehaviour
{
    [SerializeField] float m_pooledTime = 0.0f;

    void OnEnable()
    {
        Invoke("ReturnToPool", m_pooledTime);
    }

    void OnDisable()
    {
        CancelInvoke("ReturnToPool");
    }

    public void ReturnToPool()
    {
        PoolManager.Instance.Return(gameObject);
    }
}