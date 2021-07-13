using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Wander,
    Follow,
    Death,
    Attack,
}

public class EnemyController : MonoBehaviour
{
    GameObject player;

    public float range;
    public float speed = 1f;
    public float attackRange;
    private bool wandering = false;
    private bool dead = false;
    private bool attackCooldown = false;
    public float attackCooldownTimer;

    public EnemyState currState = EnemyState.Wander;

    Rigidbody enemyRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState)
        {
            case(EnemyState.Wander):
            Wander();
            break;

            case(EnemyState.Follow):
            Follow();
            break;

            case (EnemyState.Attack):
            Attack();
            break;

            case(EnemyState.Death):
            Death();
            break; 
                
        }

        if (playerInRange(range) && currState != EnemyState.Death)
        {
            currState = EnemyState.Follow;
        }
        else if (playerInRange(range) == false && currState != EnemyState.Death)
        {
            currState = EnemyState.Wander;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currState = EnemyState.Attack;
        }
    }

    private bool playerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    void Wander()
    {
        if (!wandering)
        {
            StartCoroutine(KeepWandering());
        }
        //TODO: Add proper wander script
        // This works but at the moment it isn't programmed to move anywhere
        enemyRigidbody.velocity = new Vector3(Random.Range(-2,2) * speed, 0, Random.Range(-2, 2) * speed);
    }

    private IEnumerator KeepWandering()
    {
        wandering = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        wandering = false;
    }

    void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void Attack()
    {
        if (!attackCooldown)
        {
            //TODO Make sure we aren't hardcoding values in here
            GameController.DamagePlayer(1);
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        attackCooldown = true;
        yield return new WaitForSeconds(attackCooldownTimer);
        attackCooldown = false;
    }
}
