using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    Rigidbody rb;
    public Transform weapon;

    Vector3 defaultWeaponTransform;
    public bool attacking;
    public float attackCooldown;
    float defaultAttackCooldown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //TODO - THIS IS WRONG!! 
        defaultWeaponTransform = new Vector3(weapon.transform.position.x, weapon.transform.position.y, weapon.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCooldown <= 0)
        {
            attackCooldown = defaultAttackCooldown;
            weapon.transform.position = defaultWeaponTransform;
            attacking = false;
        }

        if (attacking == true)
        {
            attackCooldown -= Time.deltaTime;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.MovePosition(transform.position + (new Vector3(1,0,0) * h + new Vector3 (0,0,1) * v) * movementSpeed);

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);

            if (attacking == false)
            {
                weapon.transform.position = weapon.transform.position + Vector3.right;
                attacking = true;
                return;
            }            
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }



    }
}
