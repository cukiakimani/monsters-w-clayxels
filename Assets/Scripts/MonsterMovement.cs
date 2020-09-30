using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 25f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float closeEnough = 3f;

    [Space, SerializeField] private Vector3 planeInPoint;

    private Rigidbody rigidbody;

    private bool moving;
    private Vector3 movePosition;
    private Vector3 moveDirection;
    private Quaternion facingRotation;

    private Plane xzPlane;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        xzPlane = new Plane(Vector3.up, planeInPoint);

        movePosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (xzPlane.Raycast(mouseRay, out float enter))
            {
                Vector3 hitPoint = mouseRay.GetPoint(enter);
                movePosition = hitPoint;
            }
        }

        Vector3 closeEnoughPosition = transform.position;
        closeEnoughPosition.y = planeInPoint.y;
        moving = Vector3.Distance(closeEnoughPosition, movePosition) > closeEnough;


    }

    private void FixedUpdate()
    {
        if (moving)
        {
            var tempMovePos = movePosition;
            tempMovePos.y = transform.position.y;
            moveDirection = tempMovePos - transform.position;

            moveDirection.Normalize();

            float rotationAngle = Vector3.SignedAngle(Vector3.forward, moveDirection, Vector3.up);
            facingRotation = Quaternion.AngleAxis(rotationAngle, Vector3.up);

            var position = transform.position + moveDirection * (movementSpeed * Time.deltaTime);
            rigidbody.MovePosition(position);

            rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, facingRotation,
                rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
