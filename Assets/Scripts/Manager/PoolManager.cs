using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; } = null;

    /// <summary>
    /// 풀 딕셔너리
    /// 오브젝트 이름, 해당 오브젝트 큐 
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
    /// 오브젝트 딕셔너리 큐에서 가져오기
    /// </summary>
    /// <param name="argObj">가져 올 오브젝트</param>
    /// <returns>생성하거나 가져온 오브젝트</returns>
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
    /// 해당 오브젝트 딕셔너리 큐로 복귀
    /// </summary>
    /// <param name="argObj">리턴 할 오브젝트</param>
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
    /// 오브젝트 새로 생성
    /// </summary>
    /// <param name="argObj">생성 할 오브젝트</param>
    /// <returns>생성 한 오브젝트</returns>
    GameObject CreateObj(GameObject argObj)
    {
        GameObject _obj = Instantiate(argObj);
        _obj.name = argObj.name;

        return _obj;
    }
}