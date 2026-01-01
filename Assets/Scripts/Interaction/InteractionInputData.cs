using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Interaction System/Interaction Input Data", fileName = "Interaction Input Data")]
public class InteractionInputData : ScriptableObject
{
    public InputAction InteractInput;
    private bool _interactPressed;
    private bool _interactableReleased;
    public bool InteractPressed
    {
        get { return _interactPressed; }
        set { _interactPressed = value; }
    }
    public bool InteractableReleased
    {
        get { return _interactableReleased; }
        set { _interactableReleased = value; }
    }
    public void ResetInput()
    {
        _interactPressed = false;
        _interactableReleased = false;
    }
}