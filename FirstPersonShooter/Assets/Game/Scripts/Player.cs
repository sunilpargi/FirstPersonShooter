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
    [SerializeField] private GameObject _hitMarker_prefab;
    ParticleSystem laser;
    ParticleSystem laserstreak;
    [SerializeField] private AudioSource _weaponAudio;

    [SerializeField]
    private int _currentAmmo;
    private int _maxAmmo = 50;
    private bool _isreloading = false;

    private UIManager _uiManager;
    public int coins = 0;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        laser = _Muzzel_prefab.GetComponent<ParticleSystem>();
       laserstreak = _Muzzelprefab.GetComponent<ParticleSystem>();

        _currentAmmo = _maxAmmo;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.UpdateAmmo(_currentAmmo);
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

        if(Input.GetKeyDown(KeyCode.R) && _isreloading == false)
        {
            _isreloading = true;
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButton(0) && _currentAmmo > 0)
        {
            Shoot();
        }

        else
        {
            _Muzzel_prefab.SetActive(false);
            laser.Stop();
            laserstreak.Stop();
            _weaponAudio.Stop();
        }

   

    }

    void Shoot()
    {
        _Muzzel_prefab.SetActive(true);
        laser.Emit(1);
        laserstreak.Emit(1);

        _currentAmmo--;
        _uiManager.UpdateAmmo(_currentAmmo);

        if (_weaponAudio.isPlaying == false)
        {
            _weaponAudio.Play();
        }

        Ray rayorigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hitInfo;
        if (Physics.Raycast(rayorigin, out hitInfo))
        {
            Debug.Log("hit something" + hitInfo.transform.name);
            GameObject hitMarker = Instantiate(_hitMarker_prefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            Destroy(hitMarker, 1f);
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

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = _maxAmmo;
        _uiManager.UpdateAmmo(_currentAmmo);
        _isreloading = false;

    }
}
