using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBubbleDrop : MonoBehaviour
{
    private float accDeltaTick;
    private float nextGenTick;

    [SerializeField]
    GameObject DropBubble;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        accDeltaTick += Time.deltaTime;
        if (nextGenTick < accDeltaTick)
        {
            nextGenTick += Random.Range(0.2f, 1.8f);
            GameObject gameObject = Instantiate(DropBubble);
            Vector3 localPosition = gameObject.transform.localPosition;
            localPosition.x += Random.Range(-10f, 10f);
            localPosition.z += Random.Range(-10f, 10f);
            gameObject.transform.localPosition = localPosition;
            gameObject.SetActive(true);
        }
    }
}
