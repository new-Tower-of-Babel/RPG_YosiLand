using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private float applySpeed;

    private bool isWalk = false;
    private bool isRun = false;


    private Vector3 lastPos;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;
    [SerializeField]
    private CharacterAnimator animator;
    [SerializeField]
    private StatusController statusController;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        theCamera = GetComponent<Camera>();
        animator = GetComponent<CharacterAnimator>();
        statusController = GetComponent<StatusController>();
        //playAnimation = FindObjectOfType<PlayAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        TryRun();
        Move();
    }
    private void FixedUpdate()
    {
        MoveCheck();

    }
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && statusController.GetCurrentSP() > 0)
            Running();
        if (Input.GetKeyUp(KeyCode.LeftShift) || statusController.GetCurrentSP() <= 0)
            RunningCancel();
    }
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
        animator.RunningAnimation(isRun);
        statusController.DecreaseSP(1);
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
        animator.RunningAnimation(isRun);
        isWalk = true;
    }
    private void Move()
    {
        //Quaternion _camera = theCamera.transform.rotation;
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = Vector3.right * _moveDirX;
        Vector3 _moveVertical = Vector3.forward * _moveDirZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        //theCamera.transform.rotation = _camera;
    }
    private void MoveCheck()
    {
        if (!isRun)
        {
            if (Vector3.Distance(lastPos, transform.position) >= 0.01f)
                isWalk = true;
            else
                isWalk = false;
            animator.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }

    }
}
