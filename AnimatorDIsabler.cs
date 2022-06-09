using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDIsabler : MonoBehaviour
{
    [SerializeField] GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Animator element in parent.GetComponentsInChildren<Animator>())
        {
            element.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
