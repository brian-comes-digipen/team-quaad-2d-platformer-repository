using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] Backgrounds;     //array of backgrounds
    private float[] parallaxAmount;     //proportion of camera movement vs background movement
    public float smoothing = 1f;        //how smooth the parallax will be
    public float scaleFactor = 1;

    private Transform cam;              //reference to the main camera
    private Vector3 previousCamPos;     //store pos of camera from the last frame

    private void Awake()
    {
        //set up cam reference
        cam = GameObject.Find("CinemachineBrain").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;

        parallaxAmount = new float[Backgrounds.Length];

        float maxDistance = 0f;

        for (int i = 0; i < Backgrounds.Length; i++)
        {
            float distance = Backgrounds[i].position.z - previousCamPos.z; //gets the distance between the camera and all layered backgrounds 

            //parallaxAmount[i] = 100/Backgrounds[i].position.z * -1;
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }

        for (int i = 0; i < Backgrounds.Length; i++)
        {
            float distance = Backgrounds[i].position.z - previousCamPos.z; //gets the distance between the camera and all layered backgrounds 

            parallaxAmount[i] = (maxDistance - distance + 1) / maxDistance;  //+1 makes the last wall move slightly
            print(parallaxAmount[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxAmount[i] * scaleFactor;

            float backgroundTargetPosX = Backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);

            //Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            Backgrounds[i].position = backgroundTargetPos;
        }

        previousCamPos = cam.position;
    }
}
