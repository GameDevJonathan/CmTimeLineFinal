using Cinemachine;
using System.Collections;
using UnityEngine;

public class CinematicCameraCycle : MonoBehaviour
{
    [SerializeField] private GameObject[] Cameras;
    public int _currentCamera;
    public bool _canCycleCamera = false;
    [SerializeField] private float _cinematicTime;
    [SerializeField] private float _cinematicTimeCoolDown = 5f;
    private Coroutine cycleCameras;
    [SerializeField] private float _cycleTime = 7f;
    [SerializeField] private GameObject[] _text;
    // Start is called before the first frame update
    void Start()
    {
        _cinematicTime = _cinematicTimeCoolDown;
        

    }

    // Update is called once per frame
    void Update()
    {
        _canCycleCamera = _cinematicTime <= 0 ? true : false;

        _cinematicTime = Mathf.Max(0f, _cinematicTime -= Time.deltaTime);

        if (_canCycleCamera && cycleCameras == null)
        {
            _text[0].SetActive(false); 
            _text[1].SetActive(false);
            _text[2].SetActive(true);
            cycleCameras = StartCoroutine(CinematicCameraSwitch(_canCycleCamera));
        }

        //if (Input.GetKeyUp(KeyCode.C) && _canCycleCamera)
        //{
        //    //check to see if our current Camera variable is greater then
        //    //our cameras array length if so set it to 1
        //    if (_currentCamera >= Cameras.Length - 1)
        //    {
        //        _currentCamera = 0;
        //    }
        //    else
        //    //set increase the variable by one.
        //    {
        //        _currentCamera++;
        //    }

        //    SetLowCamPriorites();
        //    SetCurrentCamera();
        //}
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

    public IEnumerator CinematicCameraSwitch(bool canCycle)
    {
        while (canCycle)
        {
            //check to see if our current Camera variable is greater then
            //our cameras array length if so set it to 1
            if (_currentCamera >= Cameras.Length)
            {
                _currentCamera = 0;
            }
            SetLowCamPriorites();
            SetCurrentCamera();
            yield return new WaitForSeconds(_cycleTime);
            _currentCamera++;
        }
        
        Debug.Log("Called IEnumarator");
    }

    public void Reset()
    {

        _cinematicTime = _cinematicTimeCoolDown;
        StopAllCoroutines();
        cycleCameras = null;
        _text[2].SetActive(false);
        _text[0].SetActive(true);
        _text[1].SetActive(true);
    }
}
