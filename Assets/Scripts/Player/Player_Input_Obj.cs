using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input_Obj : MonoBehaviour
{
    [SerializeField]
    private InputActionReference rotateInput, lengthenInput, shortenInput, widenInput, narrowInput;
    private RaycastHit hit;

    void Start()
    {
        rotateInput.action.performed += Rotate_Obj;
        lengthenInput.action.performed += Lengthen_Obj;
        shortenInput.action.performed += Shorten_Obj;
        widenInput.action.performed += Widen_Obj;
        narrowInput.action.performed += Narrow_Obj;
        Debug.Log("Inputs activados");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Rotate_Obj(InputAction.CallbackContext obj)
    {
        //Debug.Log("R presionada");
        if (Physics.Raycast(this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.position, this.gameObject.GetComponent<Player_PickUp>().playerCameraTransform.forward, out hit, this.gameObject.GetComponent<Player_PickUp>().hitRange, this.gameObject.GetComponent<Player_PickUp>().pickable_Layer)) //(hit.collider != null)
        {
            hit.collider.GetComponent<ConfigObject>()?.Rotation();
            //Debug.Log("Intentando Rotar");
        }
    }

    private void Lengthen_Obj(InputAction.CallbackContext obj)
    {

    }

    private void Shorten_Obj(InputAction.CallbackContext obj)
    {

    }

    private void Widen_Obj(InputAction.CallbackContext obj)
    {

    }

    private void Narrow_Obj(InputAction.CallbackContext obj)
    {

    }
}
