using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInputTrailer : MonoBehaviour
{
    [SerializeField]
    Texture WhiteCircle, RedCircle, GreenCircle, BlueCircle, CyanCircle, MagentaCircle, YellowCircle, BlackCircle;

    private ParticleSystem Ps;

    // Use this for initialization
    void Start()
    {
        Ps = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TurnOn(Vector3 v)
    {
        gameObject.SetActive(true);
        gameObject.transform.localPosition = v;
        Ps.Play();
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
        Ps.Stop();
    }

    public void SetMaterial(Material material)
    {
        Ps.gameObject.GetComponent<Renderer>().material = material; ;
    }
}
