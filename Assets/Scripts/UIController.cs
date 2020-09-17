using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text _displayText;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);        
    }
    public void SetText(string inputString)
    {
        _displayText.text = inputString;
    }
}
