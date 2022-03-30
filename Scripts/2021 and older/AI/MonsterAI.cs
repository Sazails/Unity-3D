using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class MonsterAI : MonoBehaviour
{
    public Animator _Anim;
    public NavMeshAgent _NavAI;
    public Transform _Target;
    public GameObject _TargetGO;
    public GameObject[] _WaypointsOBJ;
    public Transform[] _Waypoints;
    public Transform[] _SpawnPoints;
    public Transform _CurrentWaypoint;
    public Camera _Cam;

    public Vector3 _LastPos;
    
    public int _ChaseTime;
    public int Rand;
    public int Rest;
    public bool _IsChasing;
    public bool _IsWondering;
    public bool _Investigating;
    public bool _Moving = true;
    public bool _ErrorCheck;

    private void Awake()
    {
        _TargetGO = GameObject.FindGameObjectWithTag("Player");
        _Target = _TargetGO.GetComponent<Transform>();
        _WaypointsOBJ = GameObject.FindGameObjectsWithTag("WP");
        _Anim = GetComponent<Animator>();
        _NavAI = GetComponent<NavMeshAgent>();
        _Waypoints = _WaypointsOBJ.Select(f => f.transform).ToArray();
    }

    private void Update()
    {
        if (_IsChasing)
        {
            _NavAI.SetDestination(_Target.position);
            _Anim.SetBool("Walking", true);
        }
        else if(!_IsChasing && !_Investigating)
        {
            StartCoroutine(Investigate());
        }

        if (SeeGameObject(_TargetGO) && !_IsChasing)
        {
            Debug.Log("Player Seen");
            StartCoroutine(Chase());
        }

        if(Vector3.Distance(_NavAI.transform.position, _LastPos) < .5f)
        {
            _Investigating = false;
        }

        // Error checks
        if(_NavAI.velocity.x == 0 && !_ErrorCheck || _NavAI.velocity.z == 0 && !_ErrorCheck)
        {
            Debug.Log("Error checking!");
            _Anim.SetBool("Walking", false);
            _ErrorCheck = true;
            _Moving = false;
            StartCoroutine(ErrorCheck());
        }
        if(_NavAI.velocity.x > .1f || _NavAI.velocity.z > .1f)
        {
            Debug.Log("Stopping error check");
            StopCoroutine(ErrorCheck());
        }
    }

    public bool SeeGameObject(GameObject go)
    {
        Renderer GORend = go.GetComponent<Renderer>();

        // if object has a renderer and visible by any camera and is in this camera frustum
        if (GORend != null && GORend.isVisible && GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_Cam), go.GetComponent<Renderer>().bounds))
        {
            RaycastHit hitInfo;

            // by default we use the rough renderer bounds
            Vector3 center = GORend.bounds.center;
            Vector3 extents = GORend.bounds.extents;
            float coefreduc = 0.8f;

            Vector3[] gobounds; // points to check for linecast from camera

            MeshFilter meshfilter = go.GetComponent<MeshFilter>();
            if (meshfilter != null) // Almost every interesting game object that is render has a mesh
            {
                center = go.transform.position;
                extents = meshfilter.mesh.bounds.extents;
                extents.Scale(go.transform.lossyScale);

                gobounds = new Vector3[33]{ // We can add more or remove some, it increase precision for not too much time or memory cost
Vector3.zero,
go.transform.TransformDirection(new Vector3(extents.x,extents.y,extents.z)*0.9f),
go.transform.TransformDirection(new Vector3(extents.x,extents.y,-extents.z)*0.9f),
go.transform.TransformDirection(new Vector3(extents.x,-extents.y,extents.z)*0.9f),
go.transform.TransformDirection(new Vector3(extents.x,-extents.y,-extents.z)*0.9f),
go.transform.TransformDirection(new Vector3(-extents.x,extents.y,extents.z)*0.9f),
go.transform.TransformDirection(new Vector3(-extents.x,extents.y,-extents.z)*0.9f),
go.transform.TransformDirection(new Vector3(-extents.x,-extents.y,extents.z)*0.9f),
go.transform.TransformDirection(new Vector3(-extents.x,-extents.y,-extents.z)*0.9f),
go.transform.TransformDirection(new Vector3(extents.x,extents.y,extents.z)*0.5f),
go.transform.TransformDirection(new Vector3(extents.x,extents.y,-extents.z)*0.5f),
go.transform.TransformDirection(new Vector3(extents.x,-extents.y,extents.z)*0.5f),
go.transform.TransformDirection(new Vector3(extents.x,-extents.y,-extents.z)*0.5f),
go.transform.TransformDirection(new Vector3(-extents.x,extents.y,extents.z)*0.5f),
go.transform.TransformDirection(new Vector3(-extents.x,extents.y,-extents.z)*0.5f),
go.transform.TransformDirection(new Vector3(-extents.x,-extents.y,extents.z)*0.5f),
go.transform.TransformDirection(new Vector3(-extents.x,-extents.y,-extents.z)*0.5f),
go.transform.TransformDirection(new Vector3(extents.x,extents.y,extents.z)*0.75f),
go.transform.TransformDirection(new Vector3(extents.x,extents.y,-extents.z)*0.75f),
go.transform.TransformDirection(new Vector3(extents.x,-extents.y,extents.z)*0.75f),
go.transform.TransformDirection(new Vector3(extents.x,-extents.y,-extents.z)*0.75f),
go.transform.TransformDirection(new Vector3(-extents.x,extents.y,extents.z)*0.75f),
go.transform.TransformDirection(new Vector3(-extents.x,extents.y,-extents.z)*0.75f),
go.transform.TransformDirection(new Vector3(-extents.x,-extents.y,extents.z)*0.75f),
go.transform.TransformDirection(new Vector3(-extents.x,-extents.y,-extents.z)*0.75f),
go.transform.TransformDirection(new Vector3(extents.x,extents.y,extents.z)*0.25f),
go.transform.TransformDirection(new Vector3(extents.x,extents.y,-extents.z)*0.25f),
go.transform.TransformDirection(new Vector3(extents.x,-extents.y,extents.z)*0.25f),
go.transform.TransformDirection(new Vector3(extents.x,-extents.y,-extents.z)*0.25f),
go.transform.TransformDirection(new Vector3(-extents.x,extents.y,extents.z)*0.25f),
go.transform.TransformDirection(new Vector3(-extents.x,extents.y,-extents.z)*0.25f),
go.transform.TransformDirection(new Vector3(-extents.x,-extents.y,extents.z)*0.25f),
go.transform.TransformDirection(new Vector3(-extents.x,-extents.y,-extents.z)*0.25f)
                                };
            }
            else // Only if gameobject has no mesh (= almost never) (Very approximately checking points using the renderer bounds and not the mesh bounds)
            {
                gobounds = new Vector3[9]{
                                        Vector3.zero,
                                        new Vector3(extents.x,extents.y,extents.z)*coefreduc,
                                        new Vector3(extents.x,extents.y,-extents.z)*coefreduc,
                                        new Vector3(extents.x,-extents.y,extents.z)*coefreduc,
                                        new Vector3(extents.x,-extents.y,-extents.z)*coefreduc,
                                        new Vector3(-extents.x,extents.y,extents.z)*coefreduc,
                                        new Vector3(-extents.x,extents.y,-extents.z)*coefreduc,
                                        new Vector3(-extents.x,-extents.y,extents.z)*coefreduc,
                                        new Vector3(-extents.x,-extents.y,-extents.z)*coefreduc
                                };
            }

            foreach (Vector3 v in gobounds)
            {
                // test if it can see gameobject
                if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_Cam), new Bounds(v + center, Vector3.zero)) // if point in viewing frustrum
                   && (!Physics.Linecast(transform.position, v + center, out hitInfo) || hitInfo.collider.gameObject == go)) // if nothing between viewing position and point
                {
                    Debug.DrawLine(transform.position, v + center, Color.red, 0.01f, false);

                    return true;
                }
            }

        }

        return false;

    }

    IEnumerator Chase()
    {
        Debug.Log("Chasing");
        _ChaseTime = Random.Range(3, 5);
        _IsChasing = true;
        _IsWondering = false;
        _Investigating = false;
        _NavAI.speed = 3;
        _Anim.speed = 3;
        yield return new WaitForSeconds(_ChaseTime);
        if (SeeGameObject(_TargetGO))
        {
            Debug.Log("Still chasing");
            StartCoroutine(Chase());
        }
        else if (!SeeGameObject(_TargetGO) && !_Investigating)
        {
            _IsChasing = false;
            _LastPos = _Target.position;
            Debug.Log("Lost player, going to investigate last position");
            StartCoroutine(Investigate());
        }
    }

    IEnumerator Investigate()
    {
        Debug.Log("Investigating");
        _Investigating = true;
        _NavAI.SetDestination(_LastPos);
        _NavAI.speed = 1;
        _Anim.speed = 1;
        yield return new WaitForSeconds(1);
    }

    IEnumerator Wonder()
    {
        Debug.Log("Entering Wondering Loop");
        _IsWondering = true;
        _Anim.SetBool("Walking", false);
        Rest = Random.Range(2, 4);
        yield return new WaitForSeconds(Rest);
        Debug.Log("Wondering");
        Rand = Random.Range(0, _Waypoints.Length);
        _NavAI.SetDestination(_Waypoints[Rand].position);
        _CurrentWaypoint = _Waypoints[Rand];
        _Moving = true;
        _Anim.SetBool("Walking", true);
    }

    IEnumerator ErrorCheck()
    {
        yield return new WaitForSeconds(8);
        Debug.Log("Monster error");
        _ErrorCheck = false;
        StartCoroutine(Wonder());
    }
}
