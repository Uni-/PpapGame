using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDisappear : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 view = Camera.main.WorldToScreenPoint(transform.position);
        if(view.y < -250)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
