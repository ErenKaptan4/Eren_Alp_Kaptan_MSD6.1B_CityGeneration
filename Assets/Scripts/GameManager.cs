using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    //Ground
    GameObject groundGameObject;
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

    public GameObject car;

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
        //Ground();
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
        else { Instantiate(car, new Vector3(185, 5, 30), Quaternion.identity); }
        
    }
    public void Ground()
    {
        groundGameObject = new GameObject();
        groundGameObject.name = "Ground";
        groundGameObject.transform.position = new Vector3(0, 3.99f, 0);
        groundGameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
        groundGameObject.transform.localScale = new Vector3(500, 1, 500);
        groundGameObject.AddComponent<Ground>();
        groundGameObject.AddComponent<BoxCollider>();
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
        road5CityGameObject.transform.localScale = new Vector3(5, 1, 100);
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
