using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    //Ground
    private GameObject groundGameObject;

    //Roads
    GameObject road1GameObject;
    GameObject road2GameObject;
    GameObject road3GameObject;
    GameObject road4GameObject;
    GameObject road5CityGameObject;

    //RoadCorners
    GameObject roadCorner1GameObject;
    GameObject roadCorner2GameObject;
    GameObject roadCorner3GameObject;

    //Car
    public GameObject car;

    //ChosenLocations
    List<Vector3> locations = new List<Vector3>()
    {
        new Vector3(125, 8, 45),
        new Vector3(125, 8, 70),
        new Vector3(150, 8, 95),
        new Vector3(175, 8, 95),
        new Vector3(200, 8, 70),
        new Vector3(200, 8, 45),
        new Vector3(150, 8, 45),
        new Vector3(150, 8, 70),
        new Vector3(175, 8, 70),
        new Vector3(175, 8, 45),
        new Vector3(125, 8, 90),
        new Vector3(200, 8, 90)



    };

    List<Vector3> locations2 = new List<Vector3>()
    {
        new Vector3(-33, 9, 125),
        new Vector3(-10, 9, 100),
        new Vector3(-33, 9, 75),
        new Vector3(-10, 9, 50),
        new Vector3(-33, 9, 25),
        new Vector3(-10, 9, 0),
        new Vector3(-33, 9, -25),
        new Vector3(-10, 9, -50),
        new Vector3(-33, 9, -75)
    };


    //House
    private GameObject house1GameObject;
    private GameObject house2GameObject;



    // Start is called before the first frame update
    void Start()
    {
        Road1();
        Road2();
        Road3();
        Road4CityConnection();
        Road5City();
        RoadCorner1();
        RoadCorner2();
        RoadCorner3();
        Ground();
        SpawnCar();
        HouseGenerator(locations, locations2);

    }



    public void HouseGenerator(List<Vector3> loc, List<Vector3> loc2)
    {
        loc.Shuffle();
        loc2.Shuffle();
        for (int i = 0; i < 5; i++)
        {
            //House 1
            house1GameObject = new GameObject();
            house1GameObject.name = "House1";
            house1GameObject.transform.position = loc[i];
            house1GameObject.AddComponent<Cube2>();
            house1GameObject.transform.localScale = new Vector3(3,3,3);

            //House 2
            house2GameObject = new GameObject();
            house2GameObject.name = "House2";
            house2GameObject.transform.position = loc2[i];
            house2GameObject.AddComponent<cube3>();
            house2GameObject.transform.localScale = new Vector3(4, 4, 4);

        }

    }



    void SpawnCar()
    {
        var randomSpawn = Random.Range(1, 3);

        if (randomSpawn == 1 )
        {
            Instantiate(car, new Vector3(135, 5, 30), Quaternion.identity);
        }
        else { Instantiate(car, new Vector3(185, 5, 30), Quaternion.identity); }
        
    }


    public void Ground()
    {
        //Ground
        house2GameObject = new GameObject();
        house2GameObject.name = "Ground";
        house2GameObject.transform.position = new Vector3(0, 3.99f, 0);
        house2GameObject.AddComponent<Ground>();
        house2GameObject.transform.localScale = new Vector3(500, 1, 500);

    }



    public void Road1()
    {
        road1GameObject = new GameObject();
        road1GameObject.name = "Road1";
        road1GameObject.transform.position = new Vector3(138, 4, 56);
        road1GameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        road1GameObject.transform.localScale = new Vector3(5, 1, 20);
        road1GameObject.AddComponent<Road>();
    }
    public void Road2()
    {
        road2GameObject = new GameObject();
        road2GameObject.name = "Road2";
        road2GameObject.transform.position = new Vector3(163, 4, 81);
        road2GameObject.transform.rotation = new Quaternion(0, 1, 0, 1);
        road2GameObject.transform.localScale = new Vector3(5, 1, 20);
        road2GameObject.AddComponent<Road>();
    }
    public void Road3()
    {
        road3GameObject = new GameObject();
        road3GameObject.name = "Road3";
        road3GameObject.transform.position = new Vector3(188, 4, 56);
        road3GameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        road3GameObject.transform.localScale = new Vector3(5, 1, 20);
        road3GameObject.AddComponent<Road>();
    }
    public void Road4CityConnection()
    {
        road4GameObject = new GameObject();
        road4GameObject.name = "Road4";
        road4GameObject.transform.position = new Vector3(83, 4, 31);
        road4GameObject.transform.rotation = new Quaternion(0, 1, 0, 1);
        road4GameObject.transform.localScale = new Vector3(5, 1, 100);
        road4GameObject.AddComponent<CityRoad>();
    }
    public void Road5City()
    {
        road5CityGameObject = new GameObject();
        road5CityGameObject.name = "Road5";
        road5CityGameObject.transform.position = new Vector3(-22, 4, 31);
        road5CityGameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        road5CityGameObject.transform.localScale = new Vector3(5, 1, 125);
        road5CityGameObject.AddComponent<CityRoad>();
    }



    public void RoadCorner1()
    {
        roadCorner1GameObject = new GameObject();
        roadCorner1GameObject.name = "RoadCorner1";
        roadCorner1GameObject.transform.position = new Vector3(138, 4, 81);
        roadCorner1GameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        roadCorner1GameObject.transform.localScale = new Vector3(5, 1, 5);
        roadCorner1GameObject.AddComponent<CornerRoad>();
    }
    public void RoadCorner2()
    {
        roadCorner2GameObject = new GameObject();
        roadCorner2GameObject.name = "RoadCorner2";
        roadCorner2GameObject.transform.position = new Vector3(188, 4, 81);
        roadCorner2GameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        roadCorner2GameObject.transform.localScale = new Vector3(5, 1, 5);
        roadCorner2GameObject.AddComponent<CornerRoad>();
    }
    public void RoadCorner3()
    {
        roadCorner3GameObject = new GameObject();
        roadCorner3GameObject.name = "RoadCorner3";
        roadCorner3GameObject.transform.position = new Vector3(188, 4, 31);
        roadCorner3GameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        roadCorner3GameObject.transform.localScale = new Vector3(5, 1, 5);
        roadCorner3GameObject.AddComponent<CornerRoad>();
    }

}
