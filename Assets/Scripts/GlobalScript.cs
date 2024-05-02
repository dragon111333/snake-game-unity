using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalScript : MonoBehaviour
{

    public GameObject tailPrefab;
    public GameObject pointPrefab;
    public GameObject bonusPrefab;

    public Text pointCounter;
    public int point = 0 ;

    public GameObject gameOverPanel;

    public AudioSource audioSource;
    public AudioClip eatPointClip; 

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>(); 
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void HitPoint(GameObject point,bool isBonus = false)
    {
        this.DestroyPoint(point);
        this.AddTailToPlayer();

        if(this.point % 5 == 0 && this.point > 1)
        {
            this.CreateNewPoint(true);
        }
        else
        {
            this.CreateNewPoint();
        }
        

        int pointAdd = isBonus ? 5 : 1;
        this.IncreasePoint(pointAdd);

        this.audioSource.PlayOneShot(eatPointClip);
    }

    public void IncreasePoint(int pointAdd = 1)
    {
        this.point += pointAdd;
        pointCounter.text = $"POINT : {this.point}";
    }

    public void DestroyPoint(GameObject point)
    {
        print("Destroy!");
        Destroy(point);
    }

    public void AddTailToPlayer() {
        GameObject lastTail = GameObject.Find("Game/Player").transform.GetChild(GameObject.Find("Game/Player").transform.childCount - 1).gameObject;

        GameObject newTail = Instantiate(tailPrefab);
        newTail.transform.parent = GameObject.Find("Game/Player").transform;

        newTail.transform.localPosition = new Vector3(lastTail.transform.position.x, lastTail.transform.position.y + 2 , lastTail.transform.position.z);

        TailController tailController = newTail.GetComponent<TailController>();
        tailController.prevObject = lastTail;
    }

    public void CreateNewPoint(bool isBonus = false)
    {
        GameObject newPoint = Instantiate( (isBonus? bonusPrefab  :pointPrefab) );
        newPoint.transform.parent = GameObject.Find("Game/Points").transform;

        // -4.52 - 4.52
        newPoint.transform.localPosition = new Vector3(Random.Range(-4.52f,4.52f), 0.19f , Random.Range(-3.12f, 3.7f));
    }


}
