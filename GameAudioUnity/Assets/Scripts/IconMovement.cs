using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconMovement : MonoBehaviour
{
    public float speed = 0.5f;

    public float rotSpeed = 50f;

    int count;

    int direction = 1;

    int moveFrames = 50;

    // Start is called before the first frame update
    void Start()
    {
        count = moveFrames / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(count <= 0)
        {
            count = moveFrames;

            direction *= -1;
        }

        transform.Translate(Vector3.up * direction * Time.fixedDeltaTime  * speed);

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                              transform.eulerAngles.y + (rotSpeed * Time.fixedDeltaTime),
                                              transform.eulerAngles.z);

        count--;
    }
}
