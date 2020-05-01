using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterController : MonoBehaviour {

   private Animator anim;

   private PlayerControls controller;
   private bool attackButton;
   private bool moveButton;

    void Awake() {
        anim = GetComponent<Animator>();
        controller = new PlayerControls();
        controller.Gameplay.SwordSwing.performed += contex => attackButton = true;
        controller.Gameplay.Movement.performed += contex => anim.SetInteger("Condition",1);
        controller.Gameplay.Movement.canceled += contex => anim.SetInteger("Condition", 0);
    }
    
    void Update() {
        Movement();
        Attacking();
    }

    void Movement ()
    {
        if (Input.GetKey(KeyCode.W) || moveButton)
        {
            if (anim.GetBool("Attacking"))
            {
                return;
            }
            anim.SetBool("Running", true);
            anim.SetInteger("Condition", 1);
            Debug.Log(anim.GetInteger("Condition"));
        }

        if (Input.GetKeyUp(KeyCode.W) || moveButton != false)
        {
            anim.SetBool("Running", false);
            anim.SetInteger("Condition", 0);
        }
    }

    void Attacking()
    {
        if (Input.GetMouseButtonDown(0) || attackButton)
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
        attackButton = false;
    }

    //enabling and disabling the controller inputs
    void OnEnable()
    {
        controller.Gameplay.Enable();
    }

    void OnDisable()
    {
        controller.Gameplay.Disable();
    }

}
