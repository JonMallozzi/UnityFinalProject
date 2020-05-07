using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;
    [SerializeField] public Image image;
    [SerializeField] public Image leftSword;
    [SerializeField] public Image rightSword;
    [SerializeField] public Image startAdv;
    [SerializeField] public Image about;
    [SerializeField] public Image exit;
    [SerializeField] public Image rect1;
    [SerializeField] public Image rect2;
    [SerializeField] public Image rect3;
    [SerializeField] Canvas canvas;

    private void Start() {
        image.enabled = false;
        canvas.enabled = true; ;
    }

    // Update is called once per frame
    void Update() {
        if(menuButtonController.index == thisIndex) {
            animator.SetBool("select", true);

            if(Input.GetAxis("Submit") == 1) {
                animator.SetBool("press", true);
                
            } else if(animator.GetBool("press")) {
                animator.SetBool("press", false);
                animatorFunctions.disableOnce = true;

                if (thisIndex == 0) {
                    if (canvas.enabled) {
                        animator.SetTrigger("Start");
                        StartCoroutine(FadeCanvas(true));
                    }
                    /*else {
                        StartCoroutine(FadeCanvas(false));
                    }*/
                }

                if(thisIndex == 1) {
                    if(image.enabled) { 
                        StartCoroutine(FadeImage(true));
                    } else {
                        StartCoroutine(FadeImage(false));
                    }
                }

                if(thisIndex == 2) {
                    print("Exit");
                    doExitGame();
                }
            }
        } else {
            animator.SetBool("select", false);
        }
    }

    void doExitGame() {
        Application.Quit();
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }

            image.enabled = false;
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                image.color = new Color(1, 1, 1, i);
                image.enabled = true;
                yield return null;
            }
        }
    }
    IEnumerator FadeCanvas(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                leftSword.color = new Color(1, 1, 1, i);
                rightSword.color = new Color(1, 1, 1, i);
                startAdv.color = new Color(1, 1, 1, i);
                rect1.color = new Color(1, 1, 1, i);
                about.color = new Color(1, 1, 1, i);
                rect2.color = new Color(1, 1, 1, i);
                exit.color = new Color(1, 1, 1, i);
                rect3.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        /*// fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                leftSword.color = new Color(1, 1, 1, i);
                rightSword.color = new Color(1, 1, 1, i);
                startAdv.color = new Color(1, 1, 1, i);
                rect1.color = new Color(1, 1, 1, i);
                about.color = new Color(1, 1, 1, i);
                rect2.color = new Color(1, 1, 1, i);
                exit.color = new Color(1, 1, 1, i);
                rect3.color = new Color(1, 1, 1, i);
                canvas.enabled = true;
                yield return null;
            }
        }*/
    }


}
