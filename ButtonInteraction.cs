using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] Transform camera = null;
    [SerializeField] FlowerMixerMachine machine = null;
    [SerializeField] float pressRange = 4;
    [SerializeField] LayerMask lm;
    RaycastHit hit;
    [SerializeField] GameObject genButton = null;
    [SerializeField] GameObject printButton = null;
    [SerializeField] GameObject chair1 = null;
    [SerializeField] GameObject chair2 = null;
    [SerializeField] GameObject chair2Pos = null;
    [SerializeField] GameObject chairPos = null;
    [SerializeField] Animator genButtonAnimation = null;
    [SerializeField] Animator printButtonAnimation = null;
    [SerializeField] GameObject player = null;
    [SerializeField] GCUWebGame.Player.playerCamera cam = null;
    [SerializeField] GCUWebGame.Player.playerMovement movement = null;
    [SerializeField] TimeManagement timeManager = null;
    [SerializeField] Rigidbody playerRB = null;
    [SerializeField] Material mat1 = null;
    [SerializeField] Material mat2 = null;
    [SerializeField] private Pot first, second;



    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (movement.seated)
        {
            timeManager.TimeOfDay += Time.deltaTime * 2;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, pressRange, lm))
        {
            if (hit.transform.gameObject == genButton && mat1.GetFloat("RimPower") > 2)
            {
                mat1.SetFloat("RimPower", 2);
            }
            else if (hit.transform.gameObject == printButton && mat2.GetFloat("RimPower") > 2)
            {
                mat2.SetFloat("RimPower", 2);
            }
            else if (hit.transform.gameObject == chair1 && mat1.GetFloat("RimPower") > 2)
            {
                mat1.SetFloat("RimPower", 2);
            }
            else if (hit.transform.gameObject == chair2 && mat2.GetFloat("RimPower") > 2)
            {
                mat2.SetFloat("RimPower", 2);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (hit.transform.gameObject == genButton)
                {
                    if(first.plant != null && second.plant != null)
                    {
                        machine.genButton = true;
                        genButtonAnimation.SetTrigger("Pressed");
                    }
                }
                else if (hit.transform.gameObject == printButton)
                {
                    if (first.plant != null && second.plant != null)
                    {
                        machine.printButton = true;
                        printButtonAnimation.SetTrigger("Pressed");
                    }
                }
                else if (hit.transform.gameObject == chair1)
                {
                    movement.seated = true;
                    player.transform.position = chairPos.transform.position;
                    playerRB.constraints = RigidbodyConstraints.FreezeAll;
                    cam.clampX1 = 100;
                    cam.clampX2 = 230;
                    cam.SetRotation();
                    cam.xClamp = true;
                    player.transform.rotation = chairPos.transform.rotation;
                }
                else if (hit.transform.gameObject == chair2)
                {
                    movement.seated = true;
                    playerRB.constraints = RigidbodyConstraints.FreezeAll;
                    cam.xClamp = true;
                    cam.clampX1 = -130;
                    cam.clampX2 = 50;
                    player.transform.position = chair2Pos.transform.position;
                    cam.SetRotation();
                    player.transform.rotation = chair2Pos.transform.rotation;
                }
            }
        }
        else if (mat2.GetFloat("RimPower") < 20)
        {
            mat2.SetFloat("RimPower", 20);
        }
        else if (mat1.GetFloat("RimPower") < 20)
        {
            mat1.SetFloat("RimPower", 20);
        }
        if (movement.seated)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                movement.seated = false;
                cam.xClamp = false;
                playerRB.constraints = RigidbodyConstraints.None;
                playerRB.constraints = RigidbodyConstraints.FreezeRotation;
                player.transform.position = (player.transform.position + new Vector3(0, 0.5f, 0));
            }
        }
    }
}
