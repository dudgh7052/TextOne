using UnityEngine;

[CreateAssetMenu(fileName = "NewWordData", menuName = "Scriptable Objects/WordData", order = 1)]
public class WordData : ScriptableObject
{
    [Header("�ܾ� ���ڿ�")]
    [SerializeField] string m_word = string.Empty;

    [Header("�������� �ε��� - ���������� �°� ����")]
    [SerializeField] int m_stageIndex = -1;

    /// <summary>
    /// �ܾ� ���ڿ�
    /// </summary>
    public string IsWord { get { return m_word; } }

    /// <summary>
    /// �������� �ε���
    /// </summary>
    public int IsStageIndex { get { return m_stageIndex; } }
}