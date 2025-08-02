using UnityEngine;

public class CircularProjectileBase : MonoBehaviour
{
    [SerializeField] float m_rotateSpeed = 0.0f;
    [SerializeField] Vector2Int m_spawnRadius = Vector2Int.zero;

    [Header("Projectile 설정")]
    [SerializeField] int m_projectileCount = 36;
    [SerializeField] float m_projectileSpeed = 0.0f;
    [SerializeField] GameObject m_projectilePrefab = null;
    [SerializeField] GameObject m_wordProjectilePrefab = null;

    void Start()
    {
        CreateProjectile(true);
    }

    void Update()
    {
        Debug.Log("asd");
        transform.Rotate(0.0f, 0.0f, m_rotateSpeed * Time.deltaTime);
    }

    void CreateProjectile(bool argCreateWordFlag)
    {
        int _wordIndex = Random.Range(0, m_projectileCount);

        float _angleStep = 360.0f / m_projectileCount;
        float _spawnRadius = Random.Range(m_spawnRadius.x, m_spawnRadius.y);
        for (int i = 0; i < m_projectileCount; i++)
        {
            float _angle = _angleStep * i;

            Vector3 _spawnPos = Quaternion.Euler(0.0f, 0.0f, _angle) * Vector3.right * _spawnRadius;
            Vector2 _moveDir = -_spawnPos.normalized;

            // 정답 단어를 알아놔야함
            if (_wordIndex == i)
            {
                GameObject _obj = PoolManager.Instance.Get(m_wordProjectilePrefab);
                _obj.transform.SetLocalPositionAndRotation(transform.position + _spawnPos, Quaternion.identity);
                _obj.GetComponent<TargetWordProjectile>().Setting(transform.position, m_projectileSpeed, "Test");
                _obj.transform.SetParent(transform);
            }
            else
            {
                GameObject _obj = PoolManager.Instance.Get(m_projectilePrefab);
                _obj.transform.SetLocalPositionAndRotation(transform.position + _spawnPos, Quaternion.identity);
                _obj.GetComponent<TargetProjectile>().Setting(transform.position, m_projectileSpeed);
                _obj.transform.SetParent(transform);
            }
        }
    }
}