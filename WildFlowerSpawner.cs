using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GCUWebGame.Inventory;

public class WildFlowerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Plant_Item[] flowerPool;
    public float radius, min = 5, max = 10, maxFlowers = 11, updateRate = 5;
    float timer = 0;
    List<GameObject> flowers;
    List<int> index;
    public TimeManagement tm;
    
    void Start()
    {
        flowers = new List<GameObject>();
        index = new List<int>();
        for (int i = 0; i < flowerPool.Length ; i++)
        {
            for (int j = 0; j < Random.Range(min, max); j++)
            {
                SpawnNewFlower(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tm.TimeOfDay >= 2 && tm.TimeOfDay <= 6)
        {
            if (timer > updateRate)
            {
                UpdateList();
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

    }

    void SpawnNewFlower(int i)
    {
        Vector3 pos = Random.insideUnitCircle * radius;
        pos.z = pos.y;
        pos.y = 0;
        RaycastHit hit;
        Ray ray = new Ray(pos + transform.position, new Vector3(0, -1, 0));
        //Debug.DrawRay(pos + transform.position, new Vector3(0, -1, 0), Color.green);
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "Terrain" || objectHit.tag == "Water")
            {
                GameObject flower = flowerPool[i].SpawnAt();
                flower.transform.SetParent(transform);
                //Debug.Log("spawning flower");
                flower.transform.position = hit.point;
                //randomize z axis
                Vector3 euler = flower.transform.eulerAngles;
                euler.y = Random.Range(0.0f, 360.0f);
                flower.transform.eulerAngles = euler;
                flowers.Add(flower);
            }
        }
        
    }

    void UpdateList()
    {
        index.Clear();
        int i = 0;
        foreach (GameObject fl in flowers)
        {
            if (fl == null)
            {
                index.Add(i);
            }
            i++;
        }
        if(index.Count > 0)
        {
            for (int j = index.Count - 1; j >= 0; j--)
            {
                flowers.RemoveAt(index[j]);
            }
        }

        if (flowers.Count < maxFlowers)
        {
            //SpawnNewFlower(Random.Range(0, flowers.Count - 1));
            SpawnNewFlower(0);
        }
    }
}
