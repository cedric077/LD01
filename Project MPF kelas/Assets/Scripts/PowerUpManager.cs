using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

    private bool doublePoints;
    private bool safeMode;

    private bool powerupActive;

    private float powerupLengthCounter;

    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;
    private GameManager theGameManager;

    private float normalPoints;

    private float trashRate;

    private PlatformDestroyer[] trashList;

    

    // Use this for initialization
    void Start () {

        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {

        if (powerupActive)
        {
            powerupLengthCounter -= Time.deltaTime;

            if (theGameManager.powerupReset)
            {
                powerupLengthCounter = 0;
                theGameManager.powerupReset = false;
            }

            if(doublePoints)
            {
                theScoreManager.pointsPerSecond = normalPoints * 2f;
                theScoreManager.shouldDouble = true;
            }

            if (safeMode)
            {
                thePlatformGenerator.randomTrashNumber = 0f;
            }

            if(powerupLengthCounter <= 0)
            {
                theScoreManager.shouldDouble = false;
                theScoreManager.pointsPerSecond = normalPoints;
                thePlatformGenerator.randomTrashNumber = trashRate;
                powerupActive = false;
            }
        }

	
	}

    public void ActivatePowerup(bool points,bool safe,float time)
    {
        doublePoints = points;
        safeMode = safe;
        powerupLengthCounter = time;
             
        normalPoints = theScoreManager.pointsPerSecond;
        trashRate = thePlatformGenerator.randomTrashNumber;

        powerupActive = true;
        if (safeMode)
        {
            trashList = FindObjectsOfType<PlatformDestroyer>();
            for (int i = 0; i < trashList.Length; i++)
            {
                if(trashList[i].gameObject.name.Contains("trashCan"))
                trashList[i].gameObject.SetActive(false);
            }
        }
    }

}
