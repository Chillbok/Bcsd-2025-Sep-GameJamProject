using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMeleeData", menuName = "Scriptable Objects/PlayerMeleeData")]
public class PlayerMeleeData : ScriptableObject
{
    [Header("애니메이션")]
    public string animationTrigger; //이 공격에 사용할 애니메이션 트리거 이름
    [SerializeField]
    public AnimationClip beforeHitboxOnAnimClip; //히트박스 켜지기 전 애니메이션
    public AnimationClip whileHitboxOnAnimClip; //히트박스 켜진 도중에 사용할 애니메이션
    public AnimationClip afterHitboxOnAnimClip; //히트박스 꺼진 후 애니메이션

    [Header("공격 정보")]
    public float damage = 10f;
    public float speedMultiplier = 1f; //이 공격 애니메이션 재생 속도

    [Header("콤보 정보")]
    public float BeforeHitboxActivate = 0f;
    public float timeBeforeHitboxActivate = 0f; 
    public float comboTimeAfterAnimationEnd = 0.2f; //애니메이션 끝난 후 콤보 입력 가능 시간
}
