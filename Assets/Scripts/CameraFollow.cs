using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform LookAt;
    private Vector3 StartOffset;
    private Vector3 MoveVector;
   
    void Start()
    {
        LookAt = GameObject.FindGameObjectWithTag("Player").transform;
        StartOffset = transform.position - LookAt.position;
    }

    // Update is called once per frame
    void Update()
    {
      

        transform.position = LookAt.position+StartOffset;//camera follow for this line only and void start 2 lines only.

    }
}
