using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchLights : MonoBehaviour
{
    public static bool lightsOff = false;
    public float lowIntensitylvl = 0;

    private bool lastLightState = false;
    private float defaultIntensitylvl = 0;
    Light2D torchLight;

    // Start is called before the first frame update
    void Start()
    {
        torchLight = gameObject.GetComponent<Light2D>();
        defaultIntensitylvl = torchLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            lightsOff = true;
        }
        if (Input.GetKey(KeyCode.O))
        {
            lightsOff = false;
        }
        if (lightsOff != lastLightState)
        {
            lastLightState = lightsOff;
            if (lightsOff)
                StartCoroutine(LightsOut());
            else
                StartCoroutine(LightsOn());
        }
    }

    private IEnumerator LightsOut()
    {
        var intensity = torchLight.intensity;
        while(intensity > lowIntensitylvl)
        {
            intensity -= 0.1f;
            torchLight.intensity = intensity;
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    private IEnumerator LightsOn()
    {
        var intensity = torchLight.intensity;
        while (intensity < defaultIntensitylvl)
        {
            intensity += 0.1f;
            torchLight.intensity = intensity;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
//add to door in the doorvalue if statement incase it gets removed:                 TorchLights.lightsOff = true;