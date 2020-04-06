using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMove>())
        {
            return;
        }

        if (other.GetComponent<Health>())
        {
            Health target = other.GetComponent<Health>();
            target.ModifyHealth(-1); // deal one damage
        }
    }
}
