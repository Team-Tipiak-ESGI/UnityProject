using System;
using UnityEngine;

public class CameraCharacterController : MonoBehaviour
{
    [SerializeField] public float sensitivity = 5.0f;
    [SerializeField] public float smoothing = 2.0f;

    private Vector2 _mouseLook;
    private Vector2 _smoothV;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        // Mouse controls
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        _smoothV.x = Mathf.Lerp(_smoothV.x, md.x, 1f / smoothing);
        _smoothV.y = Mathf.Lerp(_smoothV.y, md.y, 1f / smoothing);
        _mouseLook += _smoothV;
        _mouseLook.y = Math.Min(Math.Max(_mouseLook.y, -90), 90);

        transform.localRotation = Quaternion.Euler(-_mouseLook.y, _mouseLook.x, 0);
    }
}