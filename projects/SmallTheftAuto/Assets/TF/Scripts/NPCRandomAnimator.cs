using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomAnimator : MonoBehaviour
{
    Animator animator;
    int num = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        num = Random.Range(0, 5);
        animator.SetInteger("Mode", num);
    }
}
