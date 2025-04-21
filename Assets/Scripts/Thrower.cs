using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    public ObjectPooler pooler;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject harpoon = pooler.GetObject();
            if (harpoon != null)
            {
                harpoon.transform.position = transform.position;
                harpoon.transform.rotation = transform.rotation;
            }
        }
    }
}
