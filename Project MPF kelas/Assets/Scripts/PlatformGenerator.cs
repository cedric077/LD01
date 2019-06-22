using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    public ObjectPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private CoinGenerator theCoinGenerator;

    public float randomCoinNumber;

    public float randomTrashNumber;
    public ObjectPooler trashPool;

    public float powerupHeight;
    public ObjectPooler powerupPool;
    public float powerupThreshold;

	void Start () {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();

	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange>maxHeight )
            {
                heightChange = maxHeight;
            } else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            if(Random.Range(0f,100f)< powerupThreshold)
            {
                GameObject newPowerup = powerupPool.GetPooledObject();

                newPowerup.transform.position = transform.position + new Vector3(distanceBetween / 2f, powerupHeight, 0f);

                newPowerup.SetActive(true);
            }



            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            //Instantiate(/*thePlatform*/thePlatforms[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0, 100) < randomCoinNumber)
            {
                theCoinGenerator.SpawnCoin(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }

            else if (Random.Range(0, 100) < randomTrashNumber)
            {
                GameObject newTrash = trashPool.GetPooledObject();

                float trashXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f);

                Vector3 trashPosition = new Vector3(trashXPosition, 1f, 0f);
                newTrash.transform.position = transform.position + trashPosition;
                newTrash.transform.rotation = transform.rotation;
                newTrash.SetActive(true);
            }

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) , transform.position.y, transform.position.z);

        }
	}
}
