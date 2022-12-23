using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed =100;
    [SerializeField] float platformDist = 10;
    [SerializeField] float dropSpeed;
    [SerializeField] LayerMask platformMask;
    [SerializeField] AudioSource dropSfx;
    Rigidbody rb;
    Vector3 initialPos;
    float distanceDropped;
    bool dropping;
    bool isRotating;
    private void Start()
    {
        platformDist = PlatformGeneration.platformDist;
        rb = GetComponent<Rigidbody>();
    }
    public IEnumerator Roll(Vector3 direction)
    {
        if (!(isRotating || dropping))
        {
            isRotating = true;
            float angleLeft = 90;
            Vector3 centre = transform.position + direction / CheckGoingVertical(direction) + Vector3.down / CheckVertical();
            Vector3 axis = Vector3.Cross(Vector3.up, direction);
            while (angleLeft > 0)
            {
                float currentAngle = Mathf.Min(Time.deltaTime * speed, angleLeft);
                transform.RotateAround(centre, axis, currentAngle);
                angleLeft -= currentAngle;
                yield return null;
            }
            dropSfx.Play();
            CheckForDrop();
            isRotating = false;
        }
    }
    float CheckVertical()
    {
        if (transform.up== Vector3.up || transform.up == Vector3.down)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
    float CheckGoingVertical(Vector3 dir)
    {
        if (transform.up == dir||transform.up == -dir)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
    void CheckForDrop()
    {
        if(Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, 100, platformMask))
        {
            if ((transform.position.y - hit.point.y) > 3)
            {
                print("drops the block");
                StartCoroutine(Drop());
            }
        }
        else
        {
            GameEvents.instance.GameOver();
            rb.isKinematic = false;
            rb.AddForce(Vector3.down * 100);
            rb.AddTorque(Vector3.forward * 50);
        }
    }
    IEnumerator Drop()
    {
        //Handheld.Vibrate();
        GameEvents.instance.PlatformPassed();
        dropping = true;
        initialPos = transform.position;
        while (dropping)
        {
            Vector3 dec = new Vector3(0, dropSpeed * Time.deltaTime, 0);
            transform.position -= dec;
            //cam.position -= dec;
            distanceDropped += dropSpeed * Time.deltaTime;
            if (distanceDropped >= platformDist)
            {
                transform.position = initialPos -= new Vector3(0, platformDist, 0);
                dropping = false;
                distanceDropped = 0;
            }
            yield return null;
        }
        CheckForDrop();
    }
}
