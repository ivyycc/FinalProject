using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAnimation : MonoBehaviour
{
    private Animator TreeAnim;
    public int val;

    //public CheckpointSystem checkpoint_script;
    private string tree_tag;

    public float timer;
    void Start()
    {
        TreeAnim = GetComponent<Animator>();
        tree_tag = this.gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        AssignIntensity();

        if(timer>=0)
        {
            TreeAnim.SetInteger("T1Anim", val);
        }
        if (timer >= 2)
        {
            TreeAnim.SetInteger("T2Anim", val);
        }
        if (timer >= 4)
        {
            TreeAnim.SetInteger("T3Anim", val);
        }
        if (timer >= 6)
        {
            TreeAnim.SetInteger("T4Anim", val);
        }
    }


    private void AssignIntensity()
    {
        if(tree_tag == "Part2")
        {
            val = 1;
        }
        if (tree_tag == "Part3")
        {
            val = 2;
        }
        if (tree_tag == "Part4")
        {
            val = 3;
        }
        if (tree_tag == "Part1")
        {
            val = 0;
        }
    }


}
