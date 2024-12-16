using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_PickUp : MonoBehaviour
{
    //Script que se encarga de la mecánica de agarre del player

    public LayerMask pickable_Layer;

    public Transform playerCameraTransform;
    [SerializeField] private Transform pickUpParent;

    [Min(1)]
    public float hitRange = 3;

    [SerializeField] private GameObject pickUpUI;
    private GameObject inHandItem;

    [Space(32)]
    [SerializeField]
    private InputActionReference interactionInput, dropInput, useInput;

    private RaycastHit hit;

    //[SerializeField]
    //private AudioSource pickUpSource;

    private void Start()
    {
        interactionInput.action.performed += PickUp;
        dropInput.action.performed += Drop;
    }

    private void Drop(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    private void PickUp(InputAction.CallbackContext obj)
    {
        if(hit.collider != null && inHandItem == null)
        {
            IPickable pickableItem = hit.collider.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                //pickUpSource.Play();
                inHandItem = pickableItem.PickUp();
                inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);
            }
        }
    }

    private void Update()
    {
        DeactivateHighlight();

        if (inHandItem != null)
        {
            return;
        }

        ActivateHighlight();
        
    }
    #region Code
    void DeactivateHighlight()
    {
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
        }
    }

    void ActivateHighlight()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickable_Layer))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            if (hit.collider.CompareTag("InstructionCube"))
                pickUpUI.SetActive(true);
        }
    }
    #endregion
}
