using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private GameObject _Muzzel_prefab;
    [SerializeField] private GameObject _Muzzelprefab;
    [SerializeField] private GameObject _hitMarkerprefab;
    ParticleSystem laser;
    ParticleSystem laserstreak;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        laser = _Muzzel_prefab.GetComponent<ParticleSystem>();
        laserstreak = _Muzzelprefab.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButton(0))
        {
            _Muzzel_prefab.SetActive(true);
            laser.Emit(1);
            laserstreak.Emit(1); 
            Ray rayorigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hitInfo;
            if (Physics.Raycast(rayorigin, out hitInfo))
            {
                Debug.Log("hit something" + hitInfo.transform.name);
                Instantiate(_hitMarkerprefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal);
            }
        }

        else
        {
            _Muzzel_prefab.SetActive(false);
            laser.Stop();
            laserstreak.Stop();
        }

   

    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(-horizontalInput, 0, -verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }
}
