using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> platforms;
    public GameObject plat;
    public int poolSize = 10;

    public float intervalMin = 1.5f;
    public float intervalMax = 2f;

    public float YMin = -1f;
    public float YMax = 1f;

    private GameObject[] platform;
    private int activeCount = 0;

    public float interval;
    private float timer;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        platform = new GameObject[poolSize];
        for(int i = 0; i < platform.Length; i++)
        {
            platform[i] = Instantiate(plat);
            platform[i].SetActive(false);
        }
    }

    private void Start()
    {
        activeCount = 0;

        interval = Random.Range(intervalMin, intervalMax);
        timer = 0f;

        Spawn();
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }

        timer += Time.deltaTime;
        if(timer > interval)
        {
            timer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector3 pos = transform.position;
        pos.y = Random.Range(YMin, YMax);

        platform[activeCount].transform.position = transform.position + pos;
        platform[activeCount].SetActive(true);
        activeCount = (activeCount + 1) % platform.Length; //마지막값까지 반복

        interval = Random.Range(intervalMin, intervalMax);
    }

    /*
    public void CreatePlatform()
    {
        if(activeCount > 20)
        {
            return;
        }
        Vector3 newPos = new Vector3(Random.Range(8, 20), Random.Range(-4, 2), 0f);
        platforms.Add(Instantiate(Random.value < 0.5 ? platform[0] : platform[1], newPos, Quaternion.identity));
        activeCount++;
    }

    public void CheckPlatform()
    {
        foreach(GameObject gm in platforms)
        {
            if(gm.activeSelf)
            {
                var sr = gm.GetComponent<SpriteRenderer>();
                float width = sr.sprite.rect.width / sr.sprite.pixelsPerUnit;
                if (gm.transform.position.x < -width)
                {
                    gm.SetActive(false);
                }
            }
        }
    }
    */
}
