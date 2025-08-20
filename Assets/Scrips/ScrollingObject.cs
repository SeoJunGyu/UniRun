using UnityEditor.PackageManager.UI;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;

    private SpriteRenderer sprite;
    private GameManager gameManager;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(gameManager.IsGameOver)
        {
            return;
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
