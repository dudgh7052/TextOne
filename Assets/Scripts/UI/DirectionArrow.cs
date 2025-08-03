using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject arrowObject;

    void Update()
    {
        transform.position = GManager.Instance.IsPlayerT.position;

        float _dist = Vector3.Distance(transform.position, target.position);

        if (_dist < 5.0f)
            arrowObject.SetActive(false);
        else
            arrowObject.SetActive(true);


        Vector2 _direction = target.position - transform.position;
        float _angle = -90.0f - Mathf.Atan2(-_direction.y, _direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, _angle);
    }
}