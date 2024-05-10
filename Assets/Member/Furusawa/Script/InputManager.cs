using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

  public InputActions inputActions;

  private void Awake()
  {
    inputActions = new InputActions();
  }

  private void OnEnable()
  {
    inputActions.Enable();
  }

  private void OnDiseble()
  {
    inputActions.Disable();
  }
}
