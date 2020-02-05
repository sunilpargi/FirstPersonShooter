using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinPickupSound;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.coins = 100;
                    AudioSource.PlayClipAtPoint(coinPickupSound, transform.position, 1f);
                    Destroy(this.gameObject);
                }

            }
          
        }
    }
}
