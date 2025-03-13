using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class RandomEnvironment : MonoBehaviour
{

    public int numberOfBush;

    private void Start()
    {
        Bushes();
    }

    private void Bushes()
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
}
