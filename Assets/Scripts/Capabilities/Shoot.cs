using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    Camera mainCam;
    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 _rotation = mousePos - transform.position;
        float _rotZ = Mathf.Atan2(_rotation.y, _rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, _rotZ -90);

        if(Input.GetMouseButtonDown(1))
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
