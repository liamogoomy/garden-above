using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GCUWebGame.Inventory;

public class NewPickUp : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField]  float pickupRange = 4;
    [SerializeField]  float trowelDistance = 1;
    [SerializeField]  float lerpTime = 1;
    [SerializeField]  LayerMask lm;
    [SerializeField]  GameObject highlightSpritePF = null;
    [SerializeField]  GameObject trowelPF = null;
    [SerializeField] InventoryBehaviour itemContainer = null;
    [SerializeField] Compendium compendium = null;
    [SerializeField] CompendiumAnimationController compendiumAM = null;
    [SerializeField] GameObject compBits;

    GameObject highlightSprite;
    GameObject trowel;
    RaycastHit hit;
    bool hasTrowel = false;
    bool hasCompendium = false;
    bool HighlightingCompendium = false;
    

    // Start is called before the first frame update
    void Start()
    {
        compBits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, pickupRange, lm))
        {
            if (hit.transform.gameObject.tag == "Trowel" && !hasTrowel)
            {
                HighlightItem(hit.transform);
                if (Input.GetMouseButtonDown(1))
                {
                    hasTrowel = true;
                    Destroy(hit.transform.gameObject);
                }
            }
            else if(hit.transform.gameObject.tag == "Compendium" && !hasCompendium)
            {
                HighlightItem(hit.transform);
                if (!HighlightingCompendium)
                {
                    compendiumAM.Open();
                    HighlightingCompendium = true;
                }
                
                if (Input.GetMouseButtonDown(1))
                {
                    hasCompendium = true;
                    compBits.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Destroy(hit.transform.gameObject);
                }
                
            }
            else if (hasTrowel)
            {
                if (HighlightingCompendium && !hasCompendium)
                {
                    compendiumAM.Close();
                    HighlightingCompendium = false;
                }
                //Debug.DrawRay(camera.position, camera.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log(hit.transform.gameObject.name);
                HighlightPlant(hit.transform);
                if (Input.GetMouseButtonDown(1))
                {
                    var itemSlot = hit.transform.gameObject.GetComponentInParent<Pickup>().itemSlot;


                    if (itemContainer != null)
                    {
                        itemContainer.AddItem(itemSlot);
                    }


                    // pickupSound.Play();

                    if (itemSlot.item.type == "land")
                    {
                        compendium.landFlower(itemSlot.item.itemName);
                    }
                    else if (itemSlot.item.type == "water")
                    {
                        compendium.waterFlower(itemSlot.item.itemName);
                    }
                    else if (itemSlot.item.type == "mixed")
                    {
                        compendium.mixedFlower(itemSlot.item.itemName, itemSlot.item.icon);
                    }

                    Destroy(hit.transform.parent.gameObject);
                }
            }
            
        }
        else
        {
            if (HighlightingCompendium && !hasCompendium)
            {
                compendiumAM.Close();
                HighlightingCompendium = false;
            }
            Destroy(highlightSprite);
            Destroy(trowel);
        }
    }

    void HighlightPlant(Transform tr)
    {
        if (highlightSprite != null)
        {
            if (highlightSprite.transform.position != tr.position + (Vector3.up * 0.05f))
            {
                Destroy(highlightSprite);
                highlightSprite = Instantiate(highlightSpritePF);
                highlightSprite.transform.position = tr.position + (Vector3.up * 0.05f);

                Destroy(trowel);
                trowel = Instantiate(trowelPF);
                trowel.transform.position = transform.position;
                lerpTime = 0;
            }
            else
            {
                lerpTime += 0.04f;
                trowel.transform.localPosition = Vector3.Lerp(transform.position + (Vector3.down)*0.5f, tr.position + (transform.right * trowelDistance) + (transform.up * 2 *trowelDistance),  lerpTime);
                trowel.transform.localRotation = Quaternion.Lerp(transform.localRotation, transform.rotation * Quaternion.Euler(new Vector3(44.676f, -127.224f, -11.672f)),  lerpTime);
            }
        }
        else
        {

            highlightSprite = Instantiate(highlightSpritePF);
            highlightSprite.transform.position = tr.position + (Vector3.up * 0.05f);

            trowel = Instantiate(trowelPF);
            trowel.transform.position = transform.position;
            lerpTime = 0;

        }

    }

    void HighlightItem(Transform tr)
    {
        //print("highlighting!!!!!!!!!!!!!!!!!!!");
        if (highlightSprite != null)
        {
            if (highlightSprite.transform.position != tr.position + (Vector3.up * 0.05f))
            {
                Destroy(highlightSprite);
                highlightSprite = Instantiate(highlightSpritePF);
                highlightSprite.transform.position = tr.position + (Vector3.up * 0.05f);
            }

        }
        else
        {

            highlightSprite = Instantiate(highlightSpritePF);
            highlightSprite.transform.position = tr.position + (Vector3.up * 0.05f);

        }

    }
}
