using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{

    public GameObject prevObject;

    public float speed = 4f;
    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (prevObject != null)
        {
            Vector3 targetPosition = prevObject.gameObject.transform.position;
            Vector3 newPosition = Vector3.Lerp(this.transform.position,targetPosition, speed *  Time.deltaTime); 
            this.gameObject.transform.position = newPosition;
        }
    }
}
