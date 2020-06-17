using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vegetable"))
        {
            if (other.GetComponent<Vegetable>())
                other.GetComponent<Vegetable>().Split();
        }
    }
}
