using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseButtonSwitch : MonoBehaviour
{
    Toggle toggle;
    public InputActionAsset InputMap;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        if (PlayerPrefs.HasKey("SwitchMouseButtons"))
        {
            OnChange(PlayerPrefs.GetInt("SwitchMouseButtons") == 1);
        }
        else
        {
            PlayerPrefs.SetInt("SwitchMouseButtons", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnChange(bool value)
    {
        PlayerPrefs.SetInt("SwitchMouseButtons", value ? 1 : 0);
        InputMap.FindAction("Fire", true).ApplyBindingOverride(value ? "<Mouse>/rightButton" : "<Mouse>/leftButton");
        InputMap.FindAction("Move", true).ApplyBindingOverride(value ? "<Mouse>/leftButton" : "<Mouse>/rightButton");
    }
}
