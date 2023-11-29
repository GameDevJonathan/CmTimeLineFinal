using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineControl : MonoBehaviour
{

    [SerializeField] private CinematicCameraCycle cinematicCameraCycle;
    [SerializeField] private PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {        

        if(other.tag == "Player")
        {
            playableDirector.Play();
        }
    }

    public void Activate()
    {
        cinematicCameraCycle.enabled = true;
    }
}
