using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputVisualDraw : MonoBehaviour
{
    [SerializeField]
    ParticleSystem Ps;

    private InputHandResponse inputHandResponse;

    // Use this for initialization
    void Start()
    {
        inputHandResponse = gameObject.GetComponent<InputHandResponse>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandResponse.InputCoordsPrevCount != 0)
        {
            Vector3 tmp = Input.mousePosition;
            tmp.z = Camera.main.transform.localPosition.z * -1f;
            Vector3 v = Camera.main.ScreenToWorldPoint(tmp);
            v.z = 0f;
            Ps.gameObject.transform.localPosition = v;
            Ps.gameObject.SetActive(true);
        }
        else
        {
            Ps.gameObject.SetActive(false);
        }
    }
}
