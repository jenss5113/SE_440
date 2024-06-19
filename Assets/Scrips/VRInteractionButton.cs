using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]

public class VRInteractionButton : MonoBehaviour, IRayItem
{
    [SerializeField] private UnityEvent onButtonPressed;
    private bool _isHover;

    void Start()
    {
        _isHover = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (_isHover && Input.GetMouseButtonDown(0))
        {
            onButtonPressed.Invoke();
        }

        if (_isHover && Gamepad.current != null && Gamepad.current.selectButton.wasPressedThisFrame)
        {
            onButtonPressed.Invoke(); 
        }
    }


    public void OnPointerEnter()
    {
        transform.localScale = Vector3.one * 2.3f;
        _isHover = true;
    }

    public void OnPointerExit()
    {
        transform.localScale = Vector3.one;
        _isHover = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPointerEnter();
    }
    private void OnTriggerExit(Collider other)
    {
        OnPointerExit();
    }

}
