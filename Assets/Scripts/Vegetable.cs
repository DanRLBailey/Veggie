using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    public GameManager manager;
    public Collider collider;
    public GameObject leftHalf;
    public GameObject rightHalf;
    public Vector3 force = new Vector3(150.0f, 150.0f, 150.0f);
    public float threshold = 50f;
    public float delay = 3f;
    public bool isActivated;
    public bool isSplit;

    //Private Variables
    Rigidbody body;
    Vector3 rotation;
    Vector3 leftHalfStartingPosition;
    Vector3 rightHalfStartingPosition;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        rotation = transform.rotation.eulerAngles;
        leftHalfStartingPosition = leftHalf.transform.localPosition;
        rightHalfStartingPosition = rightHalf.transform.localPosition;
    }

    private void Update()
    {
        if (isActivated)
        {
            //Clamp Velocity
        }
    }

    public void Split()
    {
        GetComponent<MeshRenderer>().enabled = false;
        collider.enabled = false;
        leftHalf.SetActive(true);
        rightHalf.SetActive(true);

        float randX = Random.Range(force.x - threshold, force.z + threshold);
        float randY = Random.Range(force.y - threshold, force.y + threshold);
        float randZ = Random.Range(force.z - threshold, force.z + threshold);

        leftHalf.GetComponent<Rigidbody>().AddForce(new Vector3(-randX, randY, randZ));

        randX = Random.Range(force.x - threshold, force.x + threshold);
        randY = Random.Range(force.y - threshold, force.y + threshold);
        randZ = Random.Range((force.z - threshold) * -1, force.z + threshold);

        rightHalf.GetComponent<Rigidbody>().AddForce(new Vector3(randX, randY, randZ));

        isSplit = true;

        StartCoroutine(WaitForReset(delay));
    }

    IEnumerator WaitForReset(float delay)
    {
        yield return new WaitForSeconds(delay);

        Reset();
    }

    private void Reset()
    {
        GetComponent<MeshRenderer>().enabled = true;
        collider.enabled = true;
        leftHalf.SetActive(false);
        rightHalf.SetActive(false);

        transform.rotation = Quaternion.Euler(rotation);
        leftHalf.transform.localPosition = leftHalfStartingPosition;
        rightHalf.transform.localPosition = rightHalfStartingPosition;

        transform.position = GameObject.Find("SpawnPoint").transform.position;
        body.velocity = Vector3.zero;
        leftHalf.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rightHalf.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Deactivate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        isActivated = true;
    }

    public void Deactivate()
    {
        isSplit = false;
        isActivated = false;
        gameObject.SetActive(false);
    }
}
