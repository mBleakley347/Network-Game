using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text ipAddress;
    [SerializeField] private NetManager netManager;
    
    // Start is called before the first frame update
    void Start()
    {
        netManager = FindObjectOfType<NetManager>();
        ipAddress.text = "Your IP address is " + netManager.address;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
