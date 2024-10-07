using UnityEngine;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}

// 데이터 폴더처럼 사용할 수 있게 만들어주는 Attribute
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
