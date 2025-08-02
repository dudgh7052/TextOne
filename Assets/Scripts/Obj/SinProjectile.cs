using UnityEngine;

public class SinProjectile : MonoBehaviour
{
    [SerializeField] float m_xSpeed = 32.0f;
    [SerializeField] float m_ySpeed = 5.0f;
    [SerializeField] float m_timeSpeed = 8.0f;

    private float m_time = 0.0f;
    private Vector2 m_velocity;

    void Update()
    {
        m_time += Time.deltaTime * m_timeSpeed;

        m_velocity.x = Mathf.Cos(m_time * Mathf.PI) * m_xSpeed;
        m_velocity.y = m_ySpeed;

        transform.position += new Vector3(m_velocity.x, m_velocity.y, 0) * Time.deltaTime;
    }
}