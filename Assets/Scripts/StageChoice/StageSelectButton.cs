using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void IsRight()
    {
        animator.SetBool("IsRight", true);

        animator.SetBool("IsRight", false);
    }
    public void IsLeft()
    {
        animator.SetBool("IsLeft", true);
        animator.SetBool("IsLeft", false);

    }
    public void test()
    {
        if (animator != null)
        {
            bool isOpen = animator.GetBool("IsRight");
            animator.SetBool("IsRight", !isOpen);
        }
    }
}
