using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtTestUITemp : MonoBehaviour
{
    
    [SerializeField]  private SliderWithText CameraRotationXSlider;
    [SerializeField]  private SliderWithText CameraRotationYSlider;
    [SerializeField]  private SliderWithText CameraPositionXSlider;
    [SerializeField]  private SliderWithText CameraPositionYSlider;
    [SerializeField]  private SliderWithText CameraPositionZSlider;
    
    [SerializeField]  private SliderWithText CameraSizeSlider;


    [SerializeField] private GameObject Content;
    private bool open = true;

    private Camera camera;
    private void Awake()
    {
        camera = Camera.main;
        CameraPositionXSlider.Init(-20f, 0f,-14,(x)=>
            camera.transform.position = new Vector3(x,camera.transform.position.y,camera.transform.position.z));
        CameraPositionYSlider.Init(0f, 20f,15,(y)=>
            camera.transform.position = new Vector3(camera.transform.position.x,y,camera.transform.position.z));
        CameraPositionZSlider.Init(0f, -40f,-22,(z)=>
            camera.transform.position = new Vector3(camera.transform.position.x,camera.transform.position.y,z));
        CameraRotationXSlider.Init(0f,90f,30f, (rota) => {
            Vector3 ela= camera.transform.eulerAngles;
            ela.x = rota;
            camera.transform.eulerAngles = ela;
        });
        CameraRotationYSlider.Init(0f,90f,45, (rota) => {
            Vector3 ela= camera.transform.eulerAngles;
            ela.y = rota;
            camera.transform.eulerAngles = ela;
        });
        CameraSizeSlider.Init(2f,14f,8f,(size)=>
            camera.orthographicSize = size);
    }
    public void Click()
    {
        open = !open;
        Content.SetActive(open);
    }
    public void ResetAll()
    {
        CameraRotationXSlider.ResetValue();
        CameraRotationYSlider.ResetValue();
        CameraPositionXSlider.ResetValue();
        CameraPositionYSlider.ResetValue();
        CameraPositionZSlider.ResetValue();
        CameraSizeSlider.ResetValue();
    }
}
