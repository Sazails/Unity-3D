using System.Collections;
using UnityEngine;
public class DrawerSystem : MonoBehaviour
{
    Vector3 StartPos;
    Vector3 EndPos;
    float ZOffset;
    Rigidbody RB;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        StartPos = this.transform.position;
        EndPos = new Vector3(this.transform.position.x, this.transform.position.y,StartPos.z + ZOffset);
    }
    private void Update()
    {
        var DrawerPos = this.transform.position;
        if(DrawerPos.z <= StartPos.z && DrawerPos.z >= EndPos.z)
        {
            RB.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            RB.constraints = RigidbodyConstraints.None;
        }
    }
}
