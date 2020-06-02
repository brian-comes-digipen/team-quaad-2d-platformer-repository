using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] Backgrounds;     //array of backgrounds
    private float[] parallaxAmount;     //proportion of camera movement vs background movement
    public float smoothing = 1f;        //how smooth the parallax will be

    private Transform cam;              //reference to the main camera
    private Vector3 previousCamPos;     //store pos of camera from the last frame

    private void Awake()
    {
        //set up cam reference
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;

        parallaxAmount = new float[Backgrounds.Length];

        for (int i = 0; i < Backgrounds.Length; i++)
        {
            parallaxAmount[i] = Backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxAmount[i];

            float backgroundTargetPosX = Backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);

            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }
}
