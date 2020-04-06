using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //This identifier is a place holder until Austin makes a character
        if (other.name.Contains("Player"))
        {
            GameObject.FindObjectOfType<ScoreSystem>().AddToScore(value);
            Remove();
        }
    }
    public void Remove()
    {
        //just in case you want to have temporary coins which will be removed after time is up
        Destroy(this.gameObject, 0.8f);
        anim.SetTrigger("Collect");
    }
}
