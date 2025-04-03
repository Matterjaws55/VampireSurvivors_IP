using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public GameObject Coin;
    public int coinNum = 50;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < coinNum; i++)
        {
            GameObject coinParent = new GameObject("Coins");
            float yValue = 1f;
            float randomX = Random.Range(-100f, 100f);
            float randomZ = Random.Range(-100f, 100f);
            Vector3 spawnPosition = new Vector3(randomX, yValue, randomZ);
            Instantiate(Coin, spawnPosition, Quaternion.identity);
            Coin.transform.parent = coinParent.transform;
        }
        
    }
}
