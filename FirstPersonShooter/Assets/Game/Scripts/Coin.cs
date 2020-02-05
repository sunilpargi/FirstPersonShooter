using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinPickupSound;
    private UIManager _uIManager;

    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.hasCoin = true;
                    _uIManager.collectedCoin();
                    AudioSource.PlayClipAtPoint(coinPickupSound, transform.position, 1f);
                    Destroy(this.gameObject);
                }

            }
          
        }
    }
}
