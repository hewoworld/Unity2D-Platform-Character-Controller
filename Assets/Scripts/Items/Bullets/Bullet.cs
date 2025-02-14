using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    [Tooltip("Don't touch this. Is automated. Needs to be false by default")]
    public bool needToDie = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(needToDie)
            Destroy(gameObject);

        transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
