using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDirector : MonoBehaviour
{

    public EventDirector eventDirector;

    // Start is called before the first frame update
    void Start()
    {
        if (eventDirector == null)
        {
            eventDirector = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
