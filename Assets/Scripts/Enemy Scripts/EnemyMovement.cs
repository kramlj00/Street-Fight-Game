using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private CharacterAnimation enemyAnim;

    private Rigidbody myBody;
    public float speed = 5f;

    private Transform playerTarget;

    public float attack_Distance = 1f;
    public float chase_Player_After_Attack = 1f;

    private float current_Attack_Time;
    private float default_Attack_Time = 2f;

    private bool followPlayer, attackPlayer;

    void Awake()
    {
        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        current_Attack_Time = default_Attack_Time; 
    }

    // Update is called once per frame
    void Update()
    {
        Attack();  
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (!followPlayer)
            return;

        // Vector3.Distance racuna udaljenost izmedju igraca i neprijatelja
        if(Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
        {
            transform.LookAt(playerTarget); //plavi vektor gleda na igraca
            myBody.velocity = transform.forward * speed; //kreci se u smjeru plavog vektora tom brzinom

            //ako ne micemo igraca sqrMagnitude je 0
            if(myBody.velocity.sqrMagnitude != 0) 
            {
                enemyAnim.Walk(true);
            }
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance)
        {
            myBody.velocity = Vector3.zero; //zaustavlja kretanje neprijatelja
            enemyAnim.Walk(false);

            followPlayer = false;
            attackPlayer = true;
        }

    } //follow target

    void Attack()
    {
        //ako neprijatelj ne treba napasti igraca, izlaz iz funkcije
        if (!attackPlayer)
            return;

        current_Attack_Time += Time.deltaTime; //Time.deltaTime je vrijeme izmedju 2 okvira (jako mali broj)

        if(current_Attack_Time > default_Attack_Time)
        {
            enemyAnim.EnemyAttack(Random.Range(0, 3)); //vraca 0, 1, 2 jer je integer

            current_Attack_Time = 0f;
        }

        if(Vector3.Distance(transform.position, playerTarget.position) > attack_Distance + chase_Player_After_Attack)
        {
            attackPlayer = false;
            followPlayer = true;
        }

    } //attack

} //class
