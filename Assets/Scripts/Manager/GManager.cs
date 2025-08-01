using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; private set; } = null;

    /// <summary>
    /// 인터렉팅 플래그
    /// </summary>
    public bool IsInteractFlag { get; set; } = false;

    void Awake()
    {
        if (GManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneMoveManager.Instance.ChangeScene("TestEnding");
        }
    }
}