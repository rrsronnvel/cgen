using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Setting : MonoBehaviour
{
    public GameObject Panel;

        public void OpenSetting()
    {
        if (Panel !=null)
        {
            Panel.SetActive(true);

        }
    }
}
