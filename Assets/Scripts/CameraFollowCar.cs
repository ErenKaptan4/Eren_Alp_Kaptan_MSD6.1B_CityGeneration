using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    [SerializeField] private Transform target; 

    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        StartCoroutine(carFunc());
        
    }

    IEnumerator carFunc()
    {
        yield return new WaitForSeconds(0.01f);
        target = GameObject.FindGameObjectWithTag("car").transform;
        //debug.log(target);
        //debug.log(gameobject.findwithtag("car"));
    }


    private void FixedUpdate()
    {
        HandleRotation();
        HandleTranslation();
    }

    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }


}
