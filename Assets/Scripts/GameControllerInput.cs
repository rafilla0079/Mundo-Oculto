using UnityEngine;
using TMPro;

public class GameControllerInput : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI activeDevice;


    private string ultimoDeviceDetectado = "";
    private float timer = 0f;
    private float timerTime = 2f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeDevice.isActiveAndEnabled)
        {
            if ((timer -= Time.deltaTime) <= 0)
            {
                activeDevice.enabled = false;
            }
        }
    }


    public void ControlDetectado(string control)
    {
        if (ultimoDeviceDetectado.Equals(control))
        {
            return;
        }
        else
        {
            ultimoDeviceDetectado = control;
        }

        activeDevice.text = "Control detectado" + control;
        activeDevice.enabled = true;
        timer = timerTime;



    }







}
