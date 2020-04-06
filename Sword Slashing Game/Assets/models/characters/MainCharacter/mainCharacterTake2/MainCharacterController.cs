using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour {

   private Vector3 moveDirection = Vector3.zero;

   private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }
    
    void Update() {
        Movement();
        Attacking();
    }

    void Movement ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (anim.GetBool("Attacking"))
            {
                return;
            }
            anim.SetBool("Running", true);
            anim.SetInteger("Condition", 1);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Running", false);
            anim.SetInteger("Condition", 0);
        }
    }

    void Attacking()
    {
        if (Input.GetMouseButtonDown(0))
        { 

            if (anim.GetBool("Running") == true)
            {
                anim.SetBool("Running", false);
                anim.SetInteger("Condition", 0);
            } 
            
            if(anim.GetBool("Running") == false)
            {
                Debug.Log("Attack");
 
                StartCoroutine(AttackRoutine());
           
            }

        }
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("Attacking", true);
        anim.SetInteger("Condition", 2);
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition", 0);
        anim.SetBool("Attacking", false);
    }

}
