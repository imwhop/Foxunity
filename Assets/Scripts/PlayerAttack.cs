using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;

    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Attack();
        }

        if( attacking ) 
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack )
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            
            }

        }



    }

   

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
    }

}