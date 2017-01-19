using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAppearDrop : MonoBehaviour
{
    [SerializeField]
    public TargetGenerator targetGenerator;

    private float accDeltaTick;
    private float nextGenTick;

    private GameObject sceneDynamicContainer;
    
    // Use this for initialization
    void Start()
    {
        sceneDynamicContainer = GameObject.Find("SceneDynamic");

        // TODO: remove this code when test ended
        GenerateBubble(5, -15, 0).GetComponent<Rigidbody>().isKinematic = true;
        GenerateBubble(0, -15, 0).GetComponent<Rigidbody>().isKinematic = true;
        GenerateBubble(-5, -15, 0).GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        accDeltaTick += Time.deltaTime;
        if (nextGenTick < accDeltaTick)
        {
            nextGenTick += Random.Range(0.2f, 1.2f);

            Vector3 dx = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            Vector3 vx = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            Vector3 va = new Vector3(Random.Range(-.5f * Mathf.PI, .5f * Mathf.PI), Random.Range(-.5f * Mathf.PI, .5f * Mathf.PI), Random.Range(-.5f * Mathf.PI, .5f * Mathf.PI));
            GenerateBubble(dx, vx, va);
        }
    }

    GameObject GenerateBubble(float dxx, float dxy, float dxz)
    {
        return GenerateBubble(new Vector3(dxx, dxy, dxz), new Vector3(), new Vector3());
    }

    GameObject GenerateBubble(Vector3 dx, Vector3 vx, Vector3 va)
    {
        UnityEngine.Object dropTarget = ((IEnumerator<UnityEngine.Object>)(targetGenerator)).Current;
        targetGenerator.MoveNext();
        GameObject instGameObject = (GameObject)Instantiate(dropTarget, new Vector3(0f, 15f, 0f), Quaternion.identity);
        Vector3 localPosition = instGameObject.transform.localPosition;

        localPosition += dx;

        Rigidbody rb = instGameObject.GetComponent<Rigidbody>();
        rb.velocity += vx;
        rb.angularVelocity += va;

        instGameObject.transform.parent = sceneDynamicContainer.transform;
        instGameObject.transform.localPosition = localPosition;
        instGameObject.SetActive(true);

        return instGameObject;
    }
}
