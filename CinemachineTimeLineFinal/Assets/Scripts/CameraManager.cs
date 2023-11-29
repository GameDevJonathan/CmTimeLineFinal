using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Cameras;
    [SerializeField] private GameObject CockPit;
    public int _currentCamera;
    public bool _canCycleCamera = false;
    [SerializeField] private CinematicCameraCycle CinematicCameraCycle;
    private Vector2 _mousePos;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Cursor.lockState);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        _mousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (_mousePos != Vector2.zero || Input.anyKey)
        {
            CinematicCameraCycle.Reset();

            foreach (var cam in Cameras)
            {
                cam.SetActive(true);
            }
        }


        if (CinematicCameraCycle._canCycleCamera)
        {
            foreach (var cam in Cameras)
            {
                cam.SetActive(false);
            }
            return;

        }

        if (Input.GetKeyUp(KeyCode.C) && _canCycleCamera)
        {
            //check to see if our current Camera variable is greater then
            //our cameras array length if so set it to 1
            if (_currentCamera >= Cameras.Length - 1)
            {
                _currentCamera = 0;
            }
            else
            //set increase the variable by one.
            {
                _currentCamera++;
            }

            SetLowCamPriorites();
            SetCurrentCamera();
        }

        if (_currentCamera == 1)
            CockPit.SetActive(true);
        else
            CockPit.SetActive(false);


    }

    public void SetLowCamPriorites()
    {
        foreach (var cam in Cameras)
        {
            if (cam.GetComponent<CinemachineVirtualCamera>())
                cam.GetComponent<CinemachineVirtualCamera>().Priority = 10;

            if (cam.GetComponent<CinemachineBlendListCamera>())
                cam.GetComponent<CinemachineBlendListCamera>().Priority = 10;
        }
    }

    public void SetCurrentCamera()
    {
        if (Cameras[_currentCamera].GetComponent<CinemachineVirtualCamera>())
        {
            Cameras[_currentCamera].GetComponent<CinemachineVirtualCamera>().Priority = 15;
        }

        if (Cameras[_currentCamera].GetComponent<CinemachineBlendListCamera>())
        {
            Cameras[_currentCamera].GetComponent<CinemachineBlendListCamera>().Priority = 15;
        }
    }  
}
