using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public int poolSize;
    public float spawnDelay;
    public GameObject objectToPool;
    List<Vegetable> vegetableList = new List<Vegetable>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject temp = Instantiate(objectToPool);
            Vegetable veg = temp.GetComponent<Vegetable>();
            veg.transform.parent = transform;
            veg.gameObject.SetActive(false);

            vegetableList.Add(veg);
        }

        StartCoroutine(SpawnObjectRecursive(spawnDelay));
    }

    IEnumerator SpawnObjectRecursive(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach(Vegetable veg in vegetableList)
        {
            if (!veg.isActivated)
            {
                veg.Activate();
                break;
            }
        }

        StartCoroutine(SpawnObjectRecursive(spawnDelay));
    }
}
