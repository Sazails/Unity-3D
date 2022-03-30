namespace Mechanics
{
	using System.Collections;
	using UnityEngine;
	using UnityEngine.UI;
	using TMPro;
	public class PlayerInteraction : MonoBehaviour
	{
		RaycastHit _Hit;
		Vector3 _Fwd;
		public TextMeshProUGUI _MainText;
		string InteractText = "'E' to interact.";
		bool Cooldown;

		public Image _FadeIn;

		private void Awake()
		{
			_FadeIn.gameObject.SetActive(true);
			StartCoroutine(FadeInNow());
			StartCoroutine(StartText());
		}

		void OnTriggerStay(Collider other)
		{
			if (other.CompareTag("Door"))
			{
				if (!Cooldown)
				{
					Cooldown = true;
					StartCoroutine(AnimateText(InteractText));
				}
				if (Input.GetKeyDown(KeyCode.E))
				{
					//other.GetComponent<ToggleAnimation>().Toggle();
				}
			}
			if (other.CompareTag("Button"))
			{
				if (!Cooldown)
				{
					Cooldown = true;
					StartCoroutine(AnimateText(InteractText));
				}
				if (Input.GetKeyDown(KeyCode.E))
				{
					other.GetComponent<ElevatorButton>().Toggle();
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			Cooldown = false;
			_MainText.text = "";
		}

		IEnumerator AnimateText(string strComplete)
		{
			int i = 0;
			_MainText.text = "";
			while (i < strComplete.Length)
			{
				_MainText.text += strComplete[i++];
				yield return new WaitForSeconds(0.05f);
			}
		}

		IEnumerator FadeInNow()
		{
			float alpha = _FadeIn.color.a;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 3)
			{
				Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, 0, t));
				_FadeIn.color = newColor;
				yield return null;
			}
		}

		IEnumerator StartText()
		{
			StartCoroutine(AnimateText("Just, walk."));
			yield return new WaitForSeconds(4);
		}
	}
}