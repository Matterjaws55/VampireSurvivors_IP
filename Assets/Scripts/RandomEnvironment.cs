using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class RandomEnvironment : MonoBehaviour
{

    public int numberOfBush;
    public GameObject[] bushPrefabs;

    private void Start()
    {
        //Bushes();
        SpawnBushes();
    }

    /*private void Bushes()
    {
        GameObject bushParent = new GameObject("Bush");

        for (int i = 0; i < numberOfBush; i++)
        {
            int xz = Random.Range(1, 1);
            int y = Random.Range(1, 1);

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = bushParent.transform;
            sphere.transform.position = new Vector3(Random.Range(-100, 100), 0.5f, Random.Range(-100, 100));
            sphere.transform.localScale = new Vector3(xz, y, xz);

            var render = sphere.GetComponent<Renderer>();
            Color greenShade = new Color(0, Random.Range(0.2f, 1), 0);
            render.material.SetColor("_BaseColor", greenShade);
        }
    }
*/

    private void SpawnBushes()
    {
        GameObject bushParent = new GameObject("Bushes");
        

        for (int i = 0; i < numberOfBush; i++)
        {
            
            int randomIndex = Random.Range(0, bushPrefabs.Length);
            GameObject selectedBush = bushPrefabs[randomIndex];

            GameObject bushInstance = Instantiate(selectedBush);
            bushInstance.transform.parent = bushParent.transform;

            bushInstance.transform.position = new Vector3(Random.Range(-100, 100), 0.5f, Random.Range(-100, 100));

            float randomYRotation = Random.Range(0f, 360f);
            bushInstance.transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);

            float xz = Random.Range(0.8f, 1.2f);
            float y = Random.Range(0.8f, 1.5f);
            bushInstance.transform.localScale = new Vector3(xz, y, xz);
        }
        
        bushParent.transform.position = new Vector3(0, 4.15f, 0);
    }
}
