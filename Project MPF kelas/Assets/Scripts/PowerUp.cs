using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public bool doublePoints;
    public bool safeMode;

    public float powerupLength;

    private PowerUpManager thePowerupManager;

    public Sprite[] powerupSprite;

	// Use this for initialization
	void Start () {

        thePowerupManager = FindObjectOfType<PowerUpManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        int powerupSelector = Random.Range(0, 2);

        switch (powerupSelector)
        {
            case 0: doublePoints = true;
                break;

            case 1: safeMode = true;
                break;

            //case 2: doublePoints = true;
              //  break;
        }

        GetComponent<SpriteRenderer>().sprite = powerupSprite[powerupSelector];

    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.name == "Player")
        {
            thePowerupManager.ActivatePowerup(doublePoints,safeMode,powerupLength);
        }

        gameObject.SetActive(false);

    }
}
