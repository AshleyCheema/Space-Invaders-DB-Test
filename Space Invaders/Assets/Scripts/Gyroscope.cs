using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    private bool gyroEnabled;
    private UnityEngine.Gyroscope gyro;

    private GameObject cameraContain;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        cameraContain = new GameObject("Camera Container");
        cameraContain.transform.position = transform.position;
        transform.SetParent(cameraContain.transform);

        gyroEnabled = EnableGyro();
    }

    private void Update()
    {
        if(gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rotation;
        }
    }

    private bool EnableGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            cameraContain.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rotation = new Quaternion(0, 0, 1, 0);

            return true;
        }

        return false;
    }
}
