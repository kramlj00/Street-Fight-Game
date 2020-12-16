using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float radius = 1f;
    public float damage = 2f;

    public bool is_Player; 
    public bool is_Enemy;

    public GameObject hit_FX_Prefab;

    void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
        //overlapsphere stvara sferu oko pozicije u zagradama 
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);

        //ukoliko imamo koliziju (hit)
        if(hit.Length > 0) {

            //ako igrac napada neprijatelja
            if (is_Player)
            {
                //dohvaćamo poziciju neprijatelja
                Vector3 hitFX_Pos = hit[0].transform.position;
                //pomicemo efekt prema gore
                hitFX_Pos.y += 1.3f; 

                //ako je neprijatelj okrenut udesno
                if(hit[0].transform.forward.x > 0){
                    hitFX_Pos.x += 0.3f;
                }
                //ako je neprijatelj okrenut ulijevo
                else if (hit[0].transform.forward.x < 0)
                {
                    hitFX_Pos.x -= 0.3f;
                }

                Instantiate(hit_FX_Prefab, hitFX_Pos, Quaternion.identity);

                //mozemo ga nokautirati samo s kick2 i punch3
                if (gameObject.CompareTag(Tags.LEFT_ARM_TAG) ||
                    gameObject.CompareTag(Tags.LEFT_LEG_TAG))
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, true);
                }
                else
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
                }


            }

            if (is_Enemy)
            {
                hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
            }

            //deaktiviramo gameobject nakon sto dotakne neprijatelja
            //zelimo da 1 udarac detektira kao 1 udarac, a ne 100 njih
            gameObject.SetActive(false);
        }

    } //detect collision

}
