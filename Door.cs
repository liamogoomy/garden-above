using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            anim.Play("Base Layer.DoorEnter", 0, 0);
        }
    }
    void OnTriggerExit(Collider c)
    {
        if(c.tag == "Player")
        {
            anim.Play("Base Layer.DoorExit", 0, 0);
        }
    }

}
