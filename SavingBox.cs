using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingBox : MonoBehaviour
{
    //uses a serialized saving box
    [SerializeField] private BoxCollider savingBox = null;
    Transform transform = null;
    [SerializeField] private GameObject spawn = null;
    [SerializeField] private GameObject spawn2 = null;
    public GameObject Ship;
    public GameObject ShipPos1;
    public GameObject ShipPos2;
    [SerializeField] private GameObject cam = null;
    [SerializeField] public bool isOnIsland1 = true;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == savingBox)
        {
            transform.position = spawn.transform.position;
            cam.GetComponent<GCUWebGame.Player.playerCamera>().SetRotation();
            transform.rotation = spawn.transform.rotation;
            Ship.transform.position = ShipPos1.transform.position;
            Ship.transform.rotation = ShipPos1.transform.rotation;
        }
    }
}
