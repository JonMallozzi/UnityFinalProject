using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Slime : MonoBehaviour
{
    public Transform target;
    Transform looker;
    [SerializeField] LayerMask groundMask;
    Rigidbody rb;
    Collider col;

    [SerializeField] float jumpAttemptInterval = 1;
    public Vector2 jumpVelVectors;
    bool grounded = false;

    private void Start()
    {
        col = gameObject.GetComponent<Collider>();
        rb = gameObject.GetComponent<Rigidbody>();
        looker = new GameObject().transform;
        looker.SetParent(this.transform);
        if(!target){
             target = GameObject.FindObjectOfType<PlayerMove>().transform;
        }
              
        StartCoroutine(MovementLoop());

    }
    void Jump()
    {
        //look at
        looker.LookAt(target);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, looker.eulerAngles.y, transform.eulerAngles.z);
        //slime is now looking at player. Add force to forward and upward axis
        rb.AddForce(transform.forward * jumpVelVectors.x);
        rb.AddForce(transform.up * jumpVelVectors.y);

    }

    IEnumerator MovementLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpAttemptInterval);
            grounded = Physics.Raycast(transform.position, Vector3.down, col.bounds.size.y + 0.01f, groundMask);
            if (grounded)
            {
                Jump();
            }
            else
            {
                while (!grounded)
                {
                    yield return new WaitForEndOfFrame();
                    grounded = Physics.Raycast(transform.position, Vector3.down, col.bounds.size.y + 0.01f, groundMask);
                }
            }
        }
    }
    public void Die()
    {
        GameObject.FindObjectOfType<ScoreSystem>().AddToScore(2);
        Destroy(this.gameObject);
    }
}
