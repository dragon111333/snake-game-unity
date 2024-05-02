using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{

    public float speed = 0.0001f;
    public float increaseSpeedPerPoint = 0.00005f;

    public string currentDirection = "up";

    public GameObject globalScriptObject;
    private GlobalScript globalScript;

    public bool delayControl = false;

    void Start()
    {
        globalScript = globalScriptObject.GetComponent<GlobalScript>(); 
    }

    void Update()
    {
        MoveControl();
        Move();
    }

    IEnumerator DelayedAction(float time)
    {
        yield return new WaitForSeconds(time);
        delayControl = false;
    }

    private void SetDelay()
    {
        StartCoroutine(DelayedAction(.3f));
        delayControl = true;
    }

    private void Move()
    {
        if (currentDirection.Equals("up"))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, speed));
        }
        else if (currentDirection.Equals("down"))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, -speed));
        }
        else if (currentDirection.Equals("left"))
        {
            this.gameObject.transform.Translate(new Vector3(-speed, 0, 0));
        }
        else if (currentDirection.Equals("right"))
        {
            this.gameObject.transform.Translate(new Vector3(speed,0, 0));
        }

    }

    private void MoveControl()
    {
        if (delayControl) return;

        if(Input.GetKeyDown(KeyCode.UpArrow) && !currentDirection.Equals("down"))
        {
            currentDirection = "up";
            SetDelay();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !currentDirection.Equals("up"))
        {
            currentDirection = "down";
            SetDelay();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !currentDirection.Equals("right"))
        {
            currentDirection = "left";
            SetDelay();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !currentDirection.Equals("left"))
        {
            currentDirection = "right";
            SetDelay();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string targetTag = collision.gameObject.tag;

        if (targetTag.Equals("point"))
        {
            print("HIT-->" + collision.gameObject.name);
            globalScript.HitPoint(collision.gameObject);
             
            this.speed += increaseSpeedPerPoint;
        }
        else if (targetTag.Equals("bonus")){
            print("HIT BONUS-->" + collision.gameObject.name);
            globalScript.HitPoint(collision.gameObject,true);
        }
        else if(targetTag.Equals("tail") || targetTag.Equals("wall"))
        {
            print("GAME OVER!!!");
            globalScript.GameOver();
        }
    }
}
