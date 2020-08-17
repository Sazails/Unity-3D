using System;
using System.Collections.Generic;
using _Project.Scripts._Interfaces;
using UnityEngine;

namespace _Project.Scripts._Interactable
{
    public class ButtonToggable : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<ButtonToggable> toggableButtons;
        [SerializeField] private List<GameObject> interactables;
        [SerializeField] private ButtonVisuals visuals;
        [SerializeField] private bool isOn = false;

        public void SetState(bool state)
        {
            isOn = state;
            visuals.Toggle(isOn);
        }
        
        public void Interact()
        {
            isOn = !isOn;
            visuals.Toggle(isOn);            
            
            foreach (GameObject interactable in interactables)
                interactable.GetComponent<IInteractable>().Interact(isOn);
            
            foreach (ButtonToggable button in toggableButtons)
                button.GetComponent<ButtonToggable>().SetState(isOn);
        }

        public void Interact(bool state)
        {
            isOn = state;
            visuals.Toggle(isOn);            
            
            foreach (GameObject interactable in interactables)
                interactable.GetComponent<IInteractable>().Interact(isOn);
            
            foreach (ButtonToggable button in toggableButtons)
                button.GetComponent<ButtonToggable>().SetState(isOn);
        }
    }
}