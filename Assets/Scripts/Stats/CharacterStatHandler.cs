using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    // 기본 스탯과 버프 스탯들의 능력치를 종합해서 스탯을 계산하는 컴포넌트
    [SerializeField] 
    private CharacterStat _baseStat;
    public CharacterStat CurrentStat { get; private set; }
    public List<CharacterStat> _statsModifiers = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        // statModifier를 반영하기 위해 baseStat을 먼저 받아옴
        AttackSO attackSO = null;
        if (_baseStat._attackSO != null)
        {
            attackSO = Instantiate(_baseStat._attackSO);
        }

        CurrentStat = new CharacterStat { _attackSO = attackSO };
        // TODO : 지금은 기본 능력치만 적용되고 있지만, 향후 능력치 강화 기능등이 추가될 것임!
        CurrentStat._statsChangeType = _baseStat._statsChangeType;
        CurrentStat._maxHealth = _baseStat._maxHealth;
        CurrentStat._speed = _baseStat._speed;
    }
}
