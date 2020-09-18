using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private Vector3 _handPos, _standPos;
    [SerializeField]
    private GameObject _handPosTarget;

    // Start is called before the first frame update
    void Start()
    {
        _handPos = _handPosTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Sensor"))
        {
            //try and get player component
            var player = other.transform.parent.GetComponent<Player>();
            if (player != null)
            {
                // call ledge grab method
                player.GrabLedge(_handPos,this);
            }
        }
    }

    public Vector3 GetStandPos()
    {
        return _standPos;
    }
}
