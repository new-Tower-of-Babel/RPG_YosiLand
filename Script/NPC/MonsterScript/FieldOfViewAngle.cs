using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfViewAngle : MonoBehaviour
{
    [SerializeField]
    private float viewAngle;
    [SerializeField]
    private float viewDistance;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private GameObject thePlayer;
    private NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    public Vector3 GetTargetPos()
    {
        return thePlayer.transform.position;
    }
    public bool View()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);
        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if (_targetTf.name == "Character")
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);
                if (_angle < viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if(Physics.Raycast(transform.position,_direction,out _hit, viewDistance))
                    {
                        if (_hit.transform.name == "Character")
                        {
                            //Debug.Log("player hakken");
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
