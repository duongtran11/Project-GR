using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InteractionInputData _interactionInputData;
    void Start()
    {
        _interactionInputData.ResetInput();
    }
    void Update()
    {
        if (_interactionInputData.InteractInput.WasPressedThisFrame())
        {
            _interactionInputData.InteractPressed = true;
        }
        if (_interactionInputData.InteractInput.WasReleasedThisFrame())
        {
            _interactionInputData.InteractableReleased = true;
        }
    }
}