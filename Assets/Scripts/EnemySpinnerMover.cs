using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpinnerMover : MonoBehaviour
{
    // Start is called before the first frame update
    public float spinSpeed = 90f;
    public float direction = 1;
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed * direction);
    }
}
