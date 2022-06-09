using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCodeManager : MonoBehaviour
{
    public List<string> codes;
    public List<GameObject> flowers;
    // Start is called before the first frame update
    void Start()
    {
        codes.Capacity = 11;
        flowers.Capacity = 11;
    }

    void AddFlower(GameObject flower)
    {
        flowers.Add(flower);
        codes.Add(flower.GetComponent<FlowerCreator>().code);
    }
}
