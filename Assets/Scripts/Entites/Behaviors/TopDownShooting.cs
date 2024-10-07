using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController _controller;

    [SerializeField]
    private Transform _projectileSpawnPos;
    private Vector2 _aimDirection = Vector2.right;

    public GameObject testPrefab;

    private void Awake()
    {
        _controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        _controller.OnAttackEvent += OnShoot;

        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        _aimDirection = direction;
    }

    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO RangedAttackSO = attackSO as RangedAttackSO;
        float projectilesAngleSpace = RangedAttackSO.multipleProjectilesAngel;
        int numberOfProjectilesPerShot = RangedAttackSO.numberofProjectilesPerShot;

        // 중간부터 펼쳐지는게 아니라 minangle부터 커지면서 쏘는 것으로 설계했어요! 
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * RangedAttackSO.multipleProjectilesAngel;

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            // 그냥 올라가면 재미없으니 랜덤으로 변하는 randomSpread를 넣었어요!
            float randomSpread = UnityEngine.Random.Range(-RangedAttackSO.spread, RangedAttackSO.spread);
            angle += randomSpread;
            CreateProjectile(RangedAttackSO, angle);
        }
    }

    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle)
    {
        // 화살 생성 -> 다음강에서 구조개선을 위해 잠시 흉물스러운 이름 참아주세요!
        GameObject obj = Instantiate(testPrefab);

        // 발사체 기본 세팅
        obj.transform.position = _projectileSpawnPos.position;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(_aimDirection, angle), rangedAttackSO);

        // 다음강에서 개선 시 활용할 코드
        // obj.SetActive(true);
    }

    private static Vector2 RotateVector2(Vector2 vec, float angle)
    {
        // 벡터 회전하기 : 쿼터니언 * 벡터 순
        return Quaternion.Euler(0, 0, angle) * vec;
    }
}