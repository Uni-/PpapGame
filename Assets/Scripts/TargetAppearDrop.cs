using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAppearDrop : MonoBehaviour
{
    private float accDeltaTick;
    private float nextGenTick;

    [SerializeField]
    UnityEngine.Object DropBubble;

    // Use this for initialization
    void Start()
    {
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
        GameObject gameObject = (GameObject)Instantiate(DropBubble, new Vector3(0f, 15f, 0f), Quaternion.identity);
        Vector3 localPosition = gameObject.transform.localPosition;

        localPosition.x += dx;
        localPosition.y += dy;
        localPosition.z += dz;
        gameObject.transform.localPosition = localPosition;
        gameObject.SetActive(true);

        return gameObject;
    }
}
