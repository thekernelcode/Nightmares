using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Wander,
    Follow,
    Death,
}

public class EnemyController : MonoBehaviour
{
    GameObject player;

    public float range;
    public float speed = 1f;

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

    }

    private bool playerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    void Wander()
    {
        //TODO: Add proper wander script
        // This works but at the moment it isn't programmed to move anywhere
        enemyRigidbody.velocity = new Vector3(Random.Range(-2,2) * speed, 0, Random.Range(-2, 2) * speed);
    }

    void Follow()
    {

    }

    void Death()
    {

    }
}
