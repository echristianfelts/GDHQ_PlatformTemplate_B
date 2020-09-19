using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    private Vector3 _offset;
    [SerializeField]
    private float _cameraResponsiveness = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        _offset = _target.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, _target.transform.position - _offset, Time.deltaTime * _cameraResponsiveness);
        //this.transform.position = _target.transform.position - _offset;
    }
}
