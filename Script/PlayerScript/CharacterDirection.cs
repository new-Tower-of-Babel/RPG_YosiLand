using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDirection : MonoBehaviour
{
    private Rigidbody myRigid;

    Vector3 originalrot;

    //Rigidbody rigidbody;
    //Vector3 EulerAngleVelocity;
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        //EulerAngleVelocity = new Vector3(0, 100, 0);
        myRigid = GetComponent<Rigidbody>();
        originalrot = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Time.fixedDeltaTime);
        //rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        directionChange();
    }
    private void directionChange()
    {
        Vector3 _characterRotationY;
        _characterRotationY = originalrot;
        if(Input.GetKey(KeyCode.D)&&Input.GetKey(KeyCode.W))
            _characterRotationY = new Vector3(0f, 45f, 0f);
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            _characterRotationY = new Vector3(0f, 135f, 0f);
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            _characterRotationY = new Vector3(0f, 315f, 0f);
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            _characterRotationY = new Vector3(0f, 225f, 0f);
        else if (Input.GetKey(KeyCode.D))
            _characterRotationY = new Vector3(0f, 90f, 0f);
        else if (Input.GetKey(KeyCode.S))
            _characterRotationY = new Vector3(0f, 180f, 0f);
        else if (Input.GetKey(KeyCode.A))
            _characterRotationY = new Vector3(0f, 270f, 0f);
        else if (Input.GetKey(KeyCode.W))
            _characterRotationY = new Vector3(0f, 0f, 0f);

        //float _rotationY1 = Input.GetAxisRaw("Horizontal"); 
        //float _rotationY2 = Input.GetAxisRaw("Vertical");
        //Vector3 _characterRotationY = new Vector3(0f, _rotationY1+_rotationY2, 0f);
        //Vector3 _moveHorizontal = transform.right * _moveDirX;    `       
        //Vector3 _moveVertical = transform.forward * _moveDirZ;
        //Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized;
        myRigid.MoveRotation(Quaternion.Euler(_characterRotationY));
        originalrot = _characterRotationY;
       
    }
}

