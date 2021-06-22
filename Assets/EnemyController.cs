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

    public EnemyState currState = EnemyState.Wander;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        transform.position = new Vector3(transform.position.x + 1, transform.position.y, -transform.position.z);
    }

    void Follow()
    {

    }

    void Death()
    {

    }
}
