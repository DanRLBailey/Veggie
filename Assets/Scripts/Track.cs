using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    List<GameObject> _objList = new List<GameObject>();
    public Vector3 force;

    // Update is called once per frame
    void Update()
    {
        if (_objList.Count > 0)
        {
            foreach(GameObject obj in _objList)
            {
                if (obj.GetComponent<Vegetable>())
                {
                    if (obj.GetComponent<Vegetable>().isSplit)
                    {
                        _objList.Remove(obj);
                        break;
                    }
                }

                if (obj.GetComponent<Rigidbody>())
                    obj.GetComponent<Rigidbody>().AddForce(force);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _objList.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_objList.Contains(other.gameObject))
            _objList.Remove(other.gameObject);
    }
}
