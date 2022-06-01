using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCon : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log("Start Run");
        //anim.Play("Base Layer.Run");
        //foreach (var par in anim.parameters)
        //{
        //    Debug.Log(par.name);
        //}
        anim.SetBool("IsRun", true);
        //anim.GetParameter(0).defaultBool = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                Debug.Log("Start Idle");
                //anim.Play("Base Layer.Idle");
                //anim.GetParameter(0).defaultBool = false;
                anim.SetBool("IsRun", false);
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                Debug.Log("Start Run");
                //anim.Play("Base Layer.Run");
                //anim.GetParameter(0).defaultBool = true;
                anim.SetBool("IsRun", true);
            }
        }
    }
}
