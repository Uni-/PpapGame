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
            nextGenTick += Random.Range(0.2f, 1.8f);
            GenerateBubble(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        }
    }

    GameObject GenerateBubble(float dx, float dy, float dz)
    {
        UnityEngine.Object dropTarget = ((IEnumerator<UnityEngine.Object>)(targetGenerator)).Current;
        targetGenerator.MoveNext();
        GameObject instGameObject = (GameObject)Instantiate(dropTarget, new Vector3(0f, 15f, 0f), Quaternion.identity);
        Vector3 localPosition = instGameObject.transform.localPosition;

        localPosition.x += dx;
        localPosition.y += dy;
        localPosition.z += dz;
        instGameObject.transform.parent = sceneDynamicContainer.transform;
        instGameObject.transform.localPosition = localPosition;
        instGameObject.SetActive(true);

        return instGameObject;
    }
}
