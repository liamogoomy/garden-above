using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMachineDoor : MonoBehaviour
{

    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        anim.Play("Base Layer.DoorOpen", 0, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.Play("Base Layer.DoorClose", 0, 0);
    }
}
