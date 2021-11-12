
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class SwitchVCAM : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int boostAmount;
    [SerializeField] private Canvas AimCanvas;

    private InputAction aimAction;
    private CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }
    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void StartAim()
    {
        virtualCamera.Priority += boostAmount;
        AimCanvas.enabled = true;
    }
    private void CancelAim()
    {
        virtualCamera.Priority -= boostAmount;
        AimCanvas.enabled = false;
    }
}

