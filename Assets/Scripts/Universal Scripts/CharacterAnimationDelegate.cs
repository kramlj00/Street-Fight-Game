using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public GameObject left_arm_attack_point, right_arm_attack_point, left_leg_attack_point, right_leg_attack_point;

    public float stand_Up_Timer = 2f;

    private CharacterAnimation animationScript;

    private AudioSource audioSource;
    public AudioClip whoosh_Sound, fall_Sound, ground_Hit_Sound, dead_Sound;

    private EnemyMovement enemy_Movement;

    private ShakeCamera shakeCamera;

    void Awake()
    {
        animationScript = GetComponent<CharacterAnimation>();

        audioSource = GetComponent<AudioSource>();

        if (gameObject.CompareTag(Tags.ENEMY_TAG))
        {
            enemy_Movement = GetComponentInParent<EnemyMovement>();
        }

        shakeCamera = GameObject.FindWithTag(Tags.MAIN_CAMERA_TAG).GetComponent<ShakeCamera>();

    }

    void Left_Arm_Attack_On()
    {
        left_arm_attack_point.SetActive(true);
    }

    void Left_Arm_Attack_Off()
    {
        if (left_arm_attack_point.activeInHierarchy)
        {
            left_arm_attack_point.SetActive(false);
        }
        
    }

    void Right_Arm_Attack_On()
    {
        right_arm_attack_point.SetActive(true);
    }

    void Right_Arm_Attack_Off()
    {
        if (right_arm_attack_point.activeInHierarchy)
        {
            right_arm_attack_point.SetActive(false);
        }

    }

    void Left_Leg_Attack_On()
    {
        left_leg_attack_point.SetActive(true);
    }

    void Left_Leg_Attack_Off()
    {
        if (left_leg_attack_point.activeInHierarchy)
        {
            left_leg_attack_point.SetActive(false);
        }

    }

    void Right_Leg_Attack_On()
    {
        right_leg_attack_point.SetActive(true);
    }

    void Right_Leg_Attack_Off()
    {
        if (right_leg_attack_point.activeInHierarchy)
        {
            right_leg_attack_point.SetActive(false);
        }

    }

    void TagLeft_Arm()
    {
        left_arm_attack_point.tag = Tags.LEFT_ARM_TAG;
    }

    void UnTagLeft_Arm()
    {
        left_arm_attack_point.tag = Tags.UNTAGGED_TAG;
    }

    void TagLeft_Leg()
    {
        left_leg_attack_point.tag = Tags.LEFT_LEG_TAG;
    }

    void UnTagLeft_Leg()
    {
        left_leg_attack_point.tag = Tags.UNTAGGED_TAG;
    }

    void Enemy_StandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(stand_Up_Timer);
        animationScript.StandUp();
    }

    public void Attack_FX_Sound()
    {
        audioSource.volume = 0.2f;
        audioSource.clip = whoosh_Sound;
        audioSource.Play();
    }

    void CharacterDiedSound()
    {
        audioSource.volume = 1f;
        audioSource.clip = dead_Sound;
        audioSource.Play();
    }

    void Enemy_KnockDown()
    {
        audioSource.clip = fall_Sound;
        audioSource.Play();
    }

    void Enemy_HitGround()
    {
        audioSource.clip = ground_Hit_Sound;
        audioSource.Play();
    }

    void DisableMovement()
    {
        enemy_Movement.enabled = false;

        //set the enemy parent to default layer
        transform.parent.gameObject.layer = 0;
    }

    void EnableMovement()
    {
        enemy_Movement.enabled = true;

        //set the enemy parent to enemy layer
        transform.parent.gameObject.layer = 9;
    }

    void ShakeCameraOnFall()
    {
        shakeCamera.ShouldShake = true;
    }

    void CharacterDied()
    {
        Invoke("DeactivateGameObject", 2f);
    }

    void DeactivateGameObject()
    {
        EnemyManager.instance.SpawnEnemy();

        gameObject.SetActive(false);
    }

} //class
