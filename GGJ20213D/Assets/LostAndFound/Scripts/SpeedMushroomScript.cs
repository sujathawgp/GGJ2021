using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMushroomScript : MonoBehaviour
{
    private Vector3 scaleChange = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= scaleChange;
        if(transform.localScale.y < 0.1f)
        {
            Destroy(this);
        }
    }

    public void Eat()
    {
        scaleChange = new Vector3(0.02f, 0.02f, 0.02f);
    }

    public bool CanEat()
    {
        return scaleChange == Vector3.zero;
    }
}
