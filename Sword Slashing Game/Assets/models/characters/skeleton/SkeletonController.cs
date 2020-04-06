using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour {

   private Vector3 moveDirection = Vector3.zero;

   private Animator anim;

   // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update() {

		bool isWalking = EnemyController.isWalking;
				
		if(isWalking){
			anim.SetInteger("Condition", 1);
        }
        else
        {
            anim.SetInteger("Condition", 0);
        }
    }

    public void Die()
    {
        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        anim.SetInteger("Condition", 2);
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
