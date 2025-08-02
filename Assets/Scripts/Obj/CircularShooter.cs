using System.Collections;
using UnityEngine;

public class CircularShooter : MonoBehaviour
{
    [SerializeField] Vector2Int m_randomCount = Vector2Int.one;
    [SerializeField] GameObject m_circularBase = null;

    [SerializeField] float m_spawnDelay = 0.0f;

    Coroutine m_shootRoutine;

    public void StartShooting()
    {
        if (m_shootRoutine == null)
            m_shootRoutine = StartCoroutine(ShootingRoutine());
    }

    public void StopShooting()
    {
        if (m_shootRoutine != null)
        {
            StopCoroutine(m_shootRoutine);
            m_shootRoutine = null;
        }
    }

    IEnumerator ShootingRoutine()
    {
        int _randomCount = Random.Range(m_randomCount.x, m_randomCount.y + 1);

        for (int i = 0; i < _randomCount; i++)
        {
            GameObject _obj = Instantiate(m_circularBase, GManager.Instance.IsBoundaryPlayerT.position, Quaternion.identity);
            yield return new WaitForSeconds(m_spawnDelay);
        }
    }
}