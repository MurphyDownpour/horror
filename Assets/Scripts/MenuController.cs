using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
    }

    private void ToggleMenu()
    {
        var isActive = _mainMenu.activeInHierarchy;
        
        _mainMenu.SetActive(!isActive);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
