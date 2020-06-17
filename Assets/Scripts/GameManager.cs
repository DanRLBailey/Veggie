using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 pos;
    public Camera cam;
    public float distanceFromPos;
    public float distanceFromCast;

    public bool startTouch = true;
    public bool isHoldingKnife = false;
    GameObject holding;

    // Start is called before the first frame update
    void Start()
    {
        pos.y = distanceFromPos;
    }

    // Update is called once per frame
    void Update()
    {
        GetTouch();
    }

    void GetTouch()
    {
        if (Input.GetAxisRaw("Fire1") > 0 ||
            Input.touchCount > 0)
        {
            RaycastHit hit;
            Ray ray;
            Touch touch;

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                ray = cam.ScreenPointToRay(touch.position);
            }
            else
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
            }

            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

            if (startTouch) //If beginning of touch
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name == "KnifeHandle")
                    {
                        holding = hit.collider.gameObject;
                        isHoldingKnife = true;
                    }
                }

                startTouch = false;
            }
            else //If holding
            {
                if (isHoldingKnife)
                {
                    float x = (ray.origin + (ray.direction.normalized * distanceFromCast)).x;
                    x = Mathf.Clamp(x, -1.5f, 1.5f);

                    float y = Input.mousePosition.y / 100;

                    if (Input.touchCount > 0)
                    {
                        y = Input.GetTouch(0).position.y / 100;
                    }

                    y = Mathf.Clamp(y, 0.5f, 2.5f);

                    Vector3 position = new Vector3(x, y, pos.z);

                    holding.transform.position = position;
                }
            }
        }
        else
        {
            holding = null;
            isHoldingKnife = false;
            startTouch = true;
        }
    }
}
