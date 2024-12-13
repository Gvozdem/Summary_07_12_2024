using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private Transform[] Positions;
    [SerializeField] private float _floatSpeed;

    int NextPosIndex;
    Transform NextPos;

    private void Start() {
        NextPos= Positions[0];
    }

    // Update is called once per frame
    private void Update()
    {
        ObjMoving();
        Debug.Log(Positions.Length);
        Debug.Log(Positions);
    }

    private void ObjMoving() {

        var step = _floatSpeed * Time.deltaTime;

        if (transform.position == NextPos.position){
            NextPosIndex++;
            if (NextPosIndex >= Positions.Length) {
                NextPosIndex = 0;
            }
            NextPos = Positions[NextPosIndex];
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, NextPos.position, step);
        }
    }
}
