using UnityEngine;

[System.Serializable]
public enum CameraState
{
    Follow,
    Fixed
}

public class CameraSystem : MonoBehaviour
{
    const float camZ = -10;
    public CameraState cameraState;
    public Transform followTarget;
    public Transform fixedTarget;
    public float yOffset;
    public float speed;
    public Vector2 distanceBuffer;
    public float distanceStrength;
    public LayerMask layers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FollowCamera()
    {
        Vector3 targetPos;
        RaycastHit2D hit = Physics2D.Raycast(followTarget.position,-Vector2.up,distanceBuffer.y,layers);
        if(hit.collider != null)
        {
            targetPos = new Vector3(followTarget.position.x,hit.point.y+yOffset,camZ);
        }
        else
        {
            targetPos = new Vector3(followTarget.position.x,followTarget.position.y+yOffset,camZ);
        }
        Vector3 moveDir = -(transform.position-targetPos);
        float distance = moveDir.magnitude;
            transform.position += new Vector3(0,moveDir.normalized.y,0) * speed * Mathf.Pow(distance,distanceStrength) * Time.deltaTime;
        if(Mathf.Abs(moveDir.x) > distanceBuffer.x)
            transform.position += new Vector3(moveDir.normalized.x,0,0) * speed * Mathf.Pow(distance-distanceBuffer.x,distanceStrength) * Time.deltaTime;
    }

    void FixedCamera()
    {
        Vector3 targetPos = new Vector3(fixedTarget.position.x,fixedTarget.position.y,camZ);
        Vector3 moveDir = -(transform.position-targetPos);
        float distance = moveDir.magnitude;
        //Y axis
        transform.position += new Vector3(0,moveDir.normalized.y,0) * speed * Mathf.Pow(distance,distanceStrength) * Time.deltaTime;
        //X axis
        transform.position += new Vector3(moveDir.normalized.x,0,0) * speed * Mathf.Pow(distance,distanceStrength) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch(cameraState)
        {
            case CameraState.Follow:
                FollowCamera();
                break;
            case CameraState.Fixed:
                FixedCamera();
                break;
        }
    }
}
