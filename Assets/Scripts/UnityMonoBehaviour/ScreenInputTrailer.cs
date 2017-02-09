using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInputTrailer : MonoBehaviour
{
    private ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TurnOn(Vector3 v)
    {
        gameObject.SetActive(true);
        gameObject.transform.localPosition = v;
        ps.Play();
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
        ps.Stop();
    }

    public void SetMaterial(Material material)
    {
        ps.gameObject.GetComponent<Renderer>().material = material;
    }
}
