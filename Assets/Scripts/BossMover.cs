using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BossMover : MonoBehaviour
{
    [SerializeField] List<Transform> MoveToPoints = new List<Transform>();
    [SerializeField] float TurnRate = 1;
    
    Transform CurrentMoveToPoint;
    int CurrentMoveToPointIndex = 0; 

    private void Update()
    {
        for (int i = 0; i < MoveToPoints.Count; i++)
        {
            if (i + 1 < MoveToPoints.Count)
            {
                Debug.DrawLine(MoveToPoints[i].position, MoveToPoints[i + 1].position);
            }
            else
            {
                Debug.DrawLine(MoveToPoints[i].position, MoveToPoints[0].position);
            }
        }
        CurrentMoveToPoint = MoveToPoints[CurrentMoveToPointIndex];
        TargetMoveToPoint();
        MoveToPoint();
    }
    void TargetMoveToPoint()
    {
        if (gameObject.transform.position == MoveToPoints[CurrentMoveToPointIndex].position)
        {
            if (CurrentMoveToPointIndex < MoveToPoints.Count - 1)
            {
                CurrentMoveToPointIndex++;
            }
            else
            {
                CurrentMoveToPointIndex = 0;
            }
        }
    }
    void MoveToPoint()
    {
        if (CurrentMoveToPoint != null)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, CurrentMoveToPoint.position, 0.1f);
        }
    }
}
