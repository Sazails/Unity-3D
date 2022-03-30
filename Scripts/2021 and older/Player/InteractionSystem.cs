using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    UISystem _UISystem;
    [SerializeField] Image InteractableIcon;
    [SerializeField] Sprite DefaultIcon;
    [SerializeField] Sprite InteractIcon;
    [SerializeField] Sprite GrabIcon;

    Camera _PlayerCam;
    const float ThrowForce = 200f;
    const float k_Spring = 50.0f;
    const float k_Damper = 10.0f;
    const float k_Drag = 20.0f;
    const float k_AngularDrag = 5.0f;
    const float k_Distance = 0.000001f;
    const bool k_AttachToCenterOfMass = false;

    private SpringJoint m_SpringJoint;

    private void Start()
    {
        _PlayerCam = GetComponent<Camera>();
        _UISystem = FindObjectOfType<UISystem>();
    }

    private void Update()
    {
        RaycastHit IconHit = new RaycastHit();
        if (Physics.Raycast(_PlayerCam.ScreenPointToRay(Input.mousePosition).origin,
                             _PlayerCam.ScreenPointToRay(Input.mousePosition).direction, out IconHit, 3,
                             Physics.DefaultRaycastLayers))
        {
            if ((IconHit.rigidbody) && (IconHit.rigidbody.isKinematic == false) || IconHit.collider.tag == "Note")
            {
                InteractableIcon.sprite = InteractIcon;
            }
            if(IconHit.collider.tag == "Note" && Input.GetMouseButtonDown(0))
            {
                _UISystem.AddNewNote(IconHit.collider.GetComponent<NoteSystem>().NoteName, IconHit.collider.GetComponent<NoteSystem>().NoteText);
                _UISystem.NoteTitleText.text = IconHit.collider.GetComponent<NoteSystem>().NoteName;
                _UISystem.NoteReadText.text = IconHit.collider.GetComponent<NoteSystem>().NoteText;
                _UISystem.OpenNoteRead();
                Destroy(IconHit.collider.gameObject);
                StartCoroutine(UpdateText("Note added to notes"));
            }
        }
        else
        {
                InteractableIcon.sprite = DefaultIcon;
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
    
        RaycastHit hit = new RaycastHit();
        if (
            !Physics.Raycast(_PlayerCam.ScreenPointToRay(Input.mousePosition).origin,
                             _PlayerCam.ScreenPointToRay(Input.mousePosition).direction, out hit, 3,
                             Physics.DefaultRaycastLayers))
        {
            return;
        }
        
        if (!hit.rigidbody || hit.rigidbody.isKinematic)
        {
            return;
        }

        if (!m_SpringJoint)
        {
            var go = new GameObject("Rigidbody dragger");
            Rigidbody body = go.AddComponent<Rigidbody>();
            m_SpringJoint = go.AddComponent<SpringJoint>();
            body.isKinematic = true;
        }

        m_SpringJoint.transform.position = hit.point;
        m_SpringJoint.anchor = Vector3.zero;

        m_SpringJoint.spring = k_Spring;
        m_SpringJoint.damper = k_Damper;
        m_SpringJoint.maxDistance = k_Distance;
        m_SpringJoint.connectedBody = hit.rigidbody;

        StartCoroutine("DragObject", hit.distance);
    }

    private IEnumerator DragObject(float distance)
    {
        var oldDrag = m_SpringJoint.connectedBody.drag;
        var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
        m_SpringJoint.connectedBody.drag = k_Drag;
        m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
        while (Input.GetMouseButton(0))
        {
            var ray = _PlayerCam.ScreenPointToRay(Input.mousePosition);
            m_SpringJoint.transform.position = ray.GetPoint(distance);
            InteractableIcon.sprite = GrabIcon;
            if (Input.GetMouseButtonDown(1))
            {
                m_SpringJoint.connectedBody.AddForce(_PlayerCam.transform.forward * ThrowForce);
                m_SpringJoint.connectedBody.drag = oldDrag;
                m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
                m_SpringJoint.connectedBody = null;
            }
            yield return null;
        }
        if (m_SpringJoint.connectedBody)
        {
            m_SpringJoint.connectedBody.drag = oldDrag;
            m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
            m_SpringJoint.connectedBody = null;
        }
    }
        IEnumerator UpdateText(string Text)
        {
            _UISystem.UpdateText.text = Text;
            yield return new WaitForSeconds(4);
            _UISystem.UpdateText.text = "";
        }
}
