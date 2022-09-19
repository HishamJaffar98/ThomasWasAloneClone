using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintSelectedCanvas : MonoBehaviour
{
    [SerializeField] Transform constraintPoint;
    private float distance;
    void Start()
    {
        distance = transform.position.y - constraintPoint.position.y;
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x, constraintPoint.position.y + distance);
    }
}
