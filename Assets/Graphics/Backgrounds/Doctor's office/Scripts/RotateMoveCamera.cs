using System;
using UnityEngine;

public class RotateMoveCamera : MonoBehaviour
{
    public GameObject Camera = null;
    public GameObject ScrollView;
    public GameObject InputFiled;
    public GameObject Button;
    public GameObject TextHint;
    public GameObject CloseHint;
    public GameObject SaveButton;
    public GameObject Image;


    public float minX = -360.0f;
    public float maxX = 360.0f;

    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    float rotationY = 0.0f;
    float rotationX = 0.0f;

    float MouseX;
    float MouseY;
    bool SceneSwitch = false; //SceneSwitch = 0, View Scene; SceneSwitch = 1, Dialog Scene

    public void ClosetheHint()
    {
        TextHint.SetActive(false);
        CloseHint.SetActive(false);
        Image.SetActive(false);
    }
    public void ChangeToDialog()
    { 
        SceneSwitch = true;
        ScrollView.SetActive(true);
        InputFiled.SetActive(true);
        Button.SetActive(true);
        SaveButton.SetActive(true);
        Camera.transform.localEulerAngles = new Vector3(0, -36, 0);
        transform.position = new Vector3(9.85f, 1.41f, 10.6f);
    }
    public void ChangeToView()
    {
        SceneSwitch = false;
        ScrollView.SetActive(false);
        InputFiled.SetActive(false);
        Button.SetActive(false);
        SaveButton.SetActive(false);
        transform.position =new Vector3(10.8f, 1.41f, 8f);
    }
    void Update()
    {
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");
        if (!SceneSwitch)
        {
            if (x != MouseX || y != MouseY)
            {
                rotationX += x * sensX * Time.deltaTime;
                rotationY += y * sensY * Time.deltaTime;
                rotationY = Mathf.Clamp(rotationY, minY, maxY);
                MouseX = x;
                MouseY = y;
                Camera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneSwitch = false;
            ScrollView.SetActive(false);
            InputFiled.SetActive(false);
            Button.SetActive(false);
            SaveButton.SetActive(false);
            transform.position = new Vector3(10.8f, 1.41f, 8f);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneSwitch = true;
            ScrollView.SetActive(true);
            InputFiled.SetActive(true);
            Button.SetActive(true);
            SaveButton.SetActive(true);
            Camera.transform.localEulerAngles = new Vector3(0, -36, 0);
            transform.position = new Vector3(9.85f, 1.41f, 10.6f);
        }

        //if (Input.GetKey(KeyCode.W))
        //{ // al precionar la tecla.W))
        //    transform.Translate(new Vector3(0, 0, 0.1f)); //cambiar posision.trasladar (aun nuevo vector(usando estas codenadas)
        //}
        //else {
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        transform.Translate(new Vector3(0, 0, -0.1f)); //cambiar posision.trasladar (aun nuevo vector(usando estas codenadas)
        //    }
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(new Vector3(0.1f, 0, 0)); //cambiar posision.trasladar (aun nuevo vector(usando estas codenadas)
        //}
        //else {
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        transform.Translate(new Vector3(-0.1f, 0, 0)); //cambiar posision.trasladar (aun nuevo vector(usando estas codenadas)
        //    }
        //}
    }
}