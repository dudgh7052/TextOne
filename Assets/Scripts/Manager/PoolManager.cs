using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; } = null;

    /// <summary>
    /// Ǯ ��ųʸ�
    /// ������Ʈ �̸�, �ش� ������Ʈ ť 
    /// </summary>
    public Dictionary<string, Queue<GameObject>> m_poolList = new Dictionary<string, Queue<GameObject>>();

    void Awake()
    {
        if (PoolManager.Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// ������Ʈ ��ųʸ� ť���� ��������
    /// </summary>
    /// <param name="argObj">���� �� ������Ʈ</param>
    /// <returns>�����ϰų� ������ ������Ʈ</returns>
    public GameObject Get(GameObject argObj)
    {
        if (m_poolList.TryGetValue(argObj.name, out Queue<GameObject> _poolList))
        {
            if (_poolList.Count == 0) return CreateObj(argObj);
            else
            {
                GameObject _obj = _poolList.Dequeue();
                _obj.gameObject.SetActive(true);

                return _obj;
            }
        }
        else return CreateObj(argObj);
    }

    /// <summary>
    /// �ش� ������Ʈ ��ųʸ� ť�� ����
    /// </summary>
    /// <param name="argObj">���� �� ������Ʈ</param>
    public void Return(GameObject argObj)
    {
        if (m_poolList.TryGetValue(argObj.name, out Queue<GameObject> _poolList)) _poolList.Enqueue(argObj);
        else
        {
            Queue<GameObject> _queue = new Queue<GameObject>();
            _queue.Enqueue(argObj);
            m_poolList.Add(argObj.name, _queue);
        }

        argObj.transform.parent = transform;
        argObj.SetActive(false);
    }

    /// <summary>
    /// ������Ʈ ���� ����
    /// </summary>
    /// <param name="argObj">���� �� ������Ʈ</param>
    /// <returns>���� �� ������Ʈ</returns>
    GameObject CreateObj(GameObject argObj)
    {
        GameObject _obj = Instantiate(argObj);
        _obj.name = argObj.name;

        return _obj;
    }
}