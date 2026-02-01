using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorBehavior : Interactable
{
    public Transform aPoint;
    public Transform bPoint;

    public bool onB = false;
    public float time = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    IEnumerator Move(Vector3 targetPos)
    {
        Vector3 startPos = onB? bPoint.position : aPoint.position;
        float distance = 100;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 3);
        float currTime = 0;
        while (currTime < time)
        {
            Vector3 moveDir = targetPos - transform.position;
            distance = moveDir.magnitude;
            transform.position = Vector3.Lerp(startPos, targetPos, currTime / time);
            currTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        onB = !onB;
    }

    public override void Interact(GameObject interactor)
    {
        Vector3 targetPos = onB ? aPoint.position : bPoint.position;
        StartCoroutine(Move(targetPos));
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
