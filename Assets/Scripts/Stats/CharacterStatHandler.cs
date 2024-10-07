using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    // �⺻ ���Ȱ� ���� ���ȵ��� �ɷ�ġ�� �����ؼ� ������ ����ϴ� ������Ʈ
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
        // statModifier�� �ݿ��ϱ� ���� baseStat�� ���� �޾ƿ�
        AttackSO attackSO = null;
        if (_baseStat._attackSO != null)
        {
            attackSO = Instantiate(_baseStat._attackSO);
        }

        CurrentStat = new CharacterStat { _attackSO = attackSO };
        // TODO : ������ �⺻ �ɷ�ġ�� ����ǰ� ������, ���� �ɷ�ġ ��ȭ ��ɵ��� �߰��� ����!
        CurrentStat._statsChangeType = _baseStat._statsChangeType;
        CurrentStat._maxHealth = _baseStat._maxHealth;
        CurrentStat._speed = _baseStat._speed;
    }
}
