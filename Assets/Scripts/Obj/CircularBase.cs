using UnityEngine;

public class CircularBase : MonoBehaviour
{
    [SerializeField] float m_rotateSpeed = 0.0f;
    [SerializeField] Vector2Int m_spawnRadius = Vector2Int.zero;

    [Header("Projectile 설정")]
    [SerializeField] int m_projectileCount = 36;
    [SerializeField] float m_moveSpeed = 5.0f;
    [SerializeField] GameObject m_projectilePrefab = null;

    void Start()
    {
        CreateProjectile();
    }

    void Update()
    {
        transform.Rotate(0f, 0f, m_rotateSpeed * Time.deltaTime);
    }

    void CreateProjectile()
    {
        float _angleStep = 360f / m_projectileCount;
        float _spawnRadius = Random.Range(m_spawnRadius.x, m_spawnRadius.y);
        Vector3 _centerPos = transform.position;

        for (int i = 0; i < m_projectileCount; i++)
        {
            float _angle = _angleStep * i;

            Vector3 _spawnPos = _centerPos + Quaternion.Euler(0, 0, _angle) * Vector3.right * _spawnRadius;

            // 발사 방향: 원 중심에서 바깥쪽으로 향하는 방향 (spawnPos → spawnPos + 방향)
            Vector2 moveDir = (_spawnPos - _centerPos).normalized;

            GameObject _obj = PoolManager.Instance.Get(m_projectilePrefab);
            _obj.transform.position = _spawnPos;
            _obj.transform.rotation = Quaternion.Euler(0, 0, _angle);
            _obj.GetComponent<TargetProjectile>().Setting(_centerPos, m_moveSpeed);
            _obj.transform.SetParent(transform);
        }
    }
}