using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCaractorScript : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Transform target;
    private void Awake()
    {
        _camera =Camera.main;
    }
    private void Update()
    {
        Vector3 rotation= _camera.transform.rotation.eulerAngles;
        target.rotation =  Quaternion.Euler(0,rotation.y,0);
    }
}
