using UnityEngine;
using System.Collections;

public class PickupPoints : MonoBehaviour
{

    public int pointToGive;
    private ScoreManager theScoreManager;
    private AudioSource coin;
    // Use this for initialization
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        coin = GameObject.Find("coin").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            theScoreManager.AddScore(pointToGive);
            gameObject.SetActive(false);
            coin.Play();
        }
    }
}