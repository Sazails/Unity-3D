using System.Collections;
using UnityEngine;

public class ToggleAnimation : MonoBehaviour
{
	Animator _Anim;
	bool _Toggle;
	[SerializeField]
	bool _Locked;
	[SerializeField]
	Collider[] _Colliders;

	bool _DoorCooldown = false;
	bool _DrawerCooldown = false;

	public enum ToggleType
	{
		Door,
		Drawer
	}

	[SerializeField]
	ToggleType _Type = new ToggleType();

	private void Awake()
	{
		_Anim = GetComponentInParent<Animator>();

	}

	public void Toggle()
	{
		if (_Type == ToggleType.Door)
		{
			if (!_DoorCooldown && Input.GetKeyDown(KeyCode.E))
			{
				_DoorCooldown = true;
				StartCoroutine(Door());
			}
		}
		if (_Type == ToggleType.Drawer)
		{
			if (!_DrawerCooldown && Input.GetKeyDown(KeyCode.E))
			{
				_DrawerCooldown = true;
				StartCoroutine(Drawer());
			}
		}
	}

	IEnumerator Door()
	{
		if (!_Locked)
		{
			_Toggle = !_Toggle;

			if (_Toggle)
			{
				_Anim.SetBool("Opened", true);
				foreach(Collider col in _Colliders)
				{
					col.enabled = false;
				}
				yield return new WaitForSeconds(1.1f);
				_DoorCooldown = false;
				foreach (Collider col in _Colliders)
				{
					col.enabled = true;
				}
			}
			else
			{
				_Anim.SetBool("Opened", false);
				foreach (Collider col in _Colliders)
				{
					col.enabled = false;
				}
				yield return new WaitForSeconds(1.1f);
				_DoorCooldown = false;
				foreach (Collider col in _Colliders)
				{
					col.enabled = true;
				}
			}
		}
		else
		{
			_Anim.Play("Locked");
			yield return new WaitForSeconds(.6f);
			_DoorCooldown = false;
		}
	}

	IEnumerator Drawer()
	{
		if (!_Locked)
		{
			_Toggle = !_Toggle;

			if (_Toggle)
			{
				_Anim.SetBool("Opened", true);
				foreach (Collider col in _Colliders)
				{
					col.enabled = false;
				}
				yield return new WaitForSeconds(1.1f);
				_DrawerCooldown = false;
				foreach (Collider col in _Colliders)
				{
					col.enabled = true;
				}
			}
			else
			{
				_Anim.SetBool("Opened", false);
				foreach (Collider col in _Colliders)
				{
					col.enabled = false;
				}
				yield return new WaitForSeconds(1.1f);
				_DrawerCooldown = false;
				foreach (Collider col in _Colliders)
				{
					col.enabled = true;
				}
			}
		}
		else
		{
			yield return new WaitForSeconds(.6f);
			_DrawerCooldown = false;
		}
	}
}
