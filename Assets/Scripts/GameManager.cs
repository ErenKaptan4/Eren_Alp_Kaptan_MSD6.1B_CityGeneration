using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public GameObject car;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCar()
    {
        var randomSpawn = Random.Range(1, 3);

        if (randomSpawn == 1 )
        {
            Instantiate(car, new Vector3(135, 5, 30), Quaternion.identity);
        }
        else { Instantiate(car, new Vector3(180, 5, 30), Quaternion.identity); }
        
    }
}
