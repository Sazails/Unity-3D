using System.Collections.Generic;
using _Project.Scripts._Interfaces;
using UnityEngine;

namespace _Project.Scripts._Interactable
{
    public class Button : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<GameObject> interactables;
        
        public void Interact()
        {
            Debug.Log("Interacted with button!");
            
            foreach (GameObject interactable in interactables)
                interactable.GetComponent<IInteractable>().Interact();
        }

        public void Interact(bool state)
        {
            Debug.Log("Interacted with button!");
            
            foreach (GameObject interactable in interactables)
                interactable.GetComponent<IInteractable>().Interact(state);
        }

        public void AddInteractable(GameObject interactable)
        {
            interactables.Add(interactable);
        }

        public void RemoveInteractable(GameObject interactable)
        {
            interactables.Remove(interactable);
        }
    }
}