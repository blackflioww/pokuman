using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TarodevController
{
    public class SecondCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == 6 && RandomGround.activlyInvis)
            {
                col.gameObject.layer = 0;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.layer == 6 && RandomGround.activlyInvis)
            {
                other.gameObject.layer = 0;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == 0)
            {
                other.gameObject.layer = 6;
            }
        }
    }
}
