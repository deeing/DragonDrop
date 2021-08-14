using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Position to which the movinng platform will float")]
    private Vector3 targetPosition = Vector3.zero;
    [SerializeField]
    [Tooltip("Time in seconds it takes to move bewteen startging position and target position (one way)")]
    private float totalMoveTime = 2f;

    private Transform thisTransform;
    private Vector3 originalPosition;

    private float moveInterval = .05f;
    private WaitForSeconds moveIntervalWait;
    private float timeMoved = 0f;

    private Vector3 movingTowards;
    private Vector3 movingFrom;

    private bool gameWon = false;


    private void Awake()
    {
        thisTransform = transform;
        originalPosition = thisTransform.position;
        moveIntervalWait = new WaitForSeconds(moveInterval);

        movingFrom = transform.position;
        movingTowards = targetPosition;

        StartCoroutine(Move());
    }

    private void SwitchTargets()
    {
        Vector3 friend = movingTowards;
        movingTowards = movingFrom;
        movingFrom = friend;
    }

    private IEnumerator Move()
    {
        while(timeMoved <= totalMoveTime)
        {
            yield return moveIntervalWait;
            float lerpTimeInterval = timeMoved / totalMoveTime;
            thisTransform.position = Vector3.Lerp(movingFrom, movingTowards, lerpTimeInterval);
            timeMoved += moveInterval;
            if (gameWon)
            {
                break;
            }
        }
        SwitchTargets();
        timeMoved = 0f;
        if (!gameWon)
        {
            StartCoroutine(Move());
        }

    }

    public void StopPlatform()
    {
        gameWon = true;
    }




}
