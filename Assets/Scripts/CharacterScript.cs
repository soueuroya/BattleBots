using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class CharacterScript : Controllable
{
    [SerializeField] float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    private Camera cam;
    [SerializeField] float speedMult = 1;

    private void Start()
    {
        LoadComponents();
    }

    private void OnValidate()
    {
        LoadComponents();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMult = 2.5f;
        }
        else
        {
            speedMult = 1;
        }
        animator.SetFloat("Speed", (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z)) * speedMult);
    }

    private void LoadComponents()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    public override void Move(MovementDirection dir)
    {
        base.Move(dir);
        switch (dir)
        {
            case MovementDirection.LEFT:
                MoveLeft();
                break;
            case MovementDirection.RIGHT:
                MoveRight();
                break;
            case MovementDirection.FORWARD:
                MoveForward();
                break;
            case MovementDirection.DOWN:
                MoveBack();
                break;
            case MovementDirection.TLEFT:
                MoveForwardLeft();
                break;
            case MovementDirection.TRIGHT:
                MoveForwardRight();
                break;
            case MovementDirection.BLEFT:
                MoveBackLeft();
                break;
            case MovementDirection.BRIGHT:
                MoveBackRight();
                break;
            default:
                break;
        }
        rb.velocity = (transform.forward * speed * speedMult) + (Vector3.up * rb.velocity.y);
    }

    public override void Stop(MovementDirection dir)
    {
        Debug.Log("Character trying to stop");
        if (currentDirection == dir)
        {
            Debug.Log("Character stop");
            rb.velocity = Vector3.zero;
        }
    }

    public void MoveForward()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public void MoveBack()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
    }

    public void MoveLeft()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y + 270, transform.rotation.eulerAngles.z);
    }

    public void MoveForwardLeft()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y + 315, transform.rotation.eulerAngles.z);
    }

    public void MoveBackLeft()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y + 225, transform.rotation.eulerAngles.z);
    }

    public void MoveRight()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
    }

    public void MoveForwardRight()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y + 45, transform.rotation.eulerAngles.z);
    }

    public void MoveBackRight()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y + 135, transform.rotation.eulerAngles.z);
    }
}
