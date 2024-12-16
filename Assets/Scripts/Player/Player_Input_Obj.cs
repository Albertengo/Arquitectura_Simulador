using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input_Obj : MonoBehaviour
{
    //optimizar codigo haciendo un void aparte q chequee con el if y rotate_obj dentro?
    //Script que se encarga de activar la personalizacion de los objetos a través de Inputs
    [Space(32)]
    [SerializeField]
    private InputActionReference rotateInput, lengthenInput, shortenInput, widenInput, narrowInput;
    private RaycastHit hit;

    void Start()
    {
        ActivateInputs();
    }
    void ActivateInputs()
    {
        rotateInput.action.performed += Rotate_Obj;

        lengthenInput.action.performed += Lengthen_Obj;

        shortenInput.action.performed += Shorten_Obj;

        widenInput.action.performed += Widen_Obj;

        narrowInput.action.performed += Narrow_Obj;
    }
    #region Input voids
    private void Rotate_Obj(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.position, this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.forward, out hit, this.gameObject.GetComponent<Player_PickUp>().hitRange, this.gameObject.GetComponent<Player_PickUp>().pickable_Layer)) //(hit.collider != null)
        {
            hit.collider.GetComponent<ConfigObject>()?.Rotation();
        }
    }

    private void Lengthen_Obj(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.position, this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.forward, out hit, this.gameObject.GetComponent<Player_PickUp>().hitRange, this.gameObject.GetComponent<Player_PickUp>().pickable_Layer)) //(hit.collider != null)
        {
            hit.collider.GetComponent<ConfigObject>()?.Length();
        }
    }

    private void Shorten_Obj(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.position, this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.forward, out hit, this.gameObject.GetComponent<Player_PickUp>().hitRange, this.gameObject.GetComponent<Player_PickUp>().pickable_Layer)) //(hit.collider != null)
        {
            hit.collider.GetComponent<ConfigObject>()?.Short();
        }
    }

    private void Widen_Obj(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.position, this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.forward, out hit, this.gameObject.GetComponent<Player_PickUp>().hitRange, this.gameObject.GetComponent<Player_PickUp>().pickable_Layer)) //(hit.collider != null)
        {
            hit.collider.GetComponent<ConfigObject>()?.Wide();
        }
    }

    private void Narrow_Obj(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.position, this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.forward, out hit, this.gameObject.GetComponent<Player_PickUp>().hitRange, this.gameObject.GetComponent<Player_PickUp>().pickable_Layer)) //(hit.collider != null)
        {
            hit.collider.GetComponent<ConfigObject>()?.Narrow();
        }
    }
    #endregion
}
