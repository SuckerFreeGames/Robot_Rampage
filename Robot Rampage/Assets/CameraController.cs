using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    bool CameraSwitch;


    public Camera firstPersonCamera1;
    public Camera firstPersonCamera2;
    public Camera overheadCamera1;
    public Camera overheadCamera2;

    void Start()
    {
        CameraSwitch = false;


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CameraSwitch = !CameraSwitch;
        }

        if (CameraSwitch == false)
        {
            ShowFirstPersonView();
        }

        else
        {
            ShowOverheadView();
        }

    }

    public void ShowOverheadView()
    {
        firstPersonCamera1.enabled = false;
        firstPersonCamera2.enabled = false;
        overheadCamera1.enabled = true;
        overheadCamera2.enabled = true;
    }

    public void ShowFirstPersonView()
    {
        firstPersonCamera1.enabled = true;
        firstPersonCamera2.enabled = true;
        overheadCamera1.enabled = false;
        overheadCamera2.enabled = false;
    }
}
