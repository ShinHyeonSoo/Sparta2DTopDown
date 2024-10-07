using UnityEngine;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}

// ������ ����ó�� ����� �� �ְ� ������ִ� Attribute
[System.Serializable]
public class CharacterStat
{
    public StatsChangeType _statsChangeType;
    [Range(1, 100)]
    public int _maxHealth;
    [Range(1f, 20f)]
    public float _speed;
    public AttackSO _attackSO;
}
