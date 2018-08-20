using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        Rigidbody2D rb2d;
        [SerializeField] float movingSpeed;
        void Awake(){
            rb2d = GetComponent<Rigidbody2D>();
        }

		public void Shot(Vector2 direction, Vector2 from){
			transform.position = from;
			Shot (direction);
        }

		public void Shot(Vector2 direction){
			gameObject.SetActive (true);
			rb2d.AddForce (direction * movingSpeed,	ForceMode2D.Force);
        }

        void OnTriggerEnter2D(Collider2D col){
			if (col.CompareTag("wall") || col.CompareTag("tank")){
				rb2d.velocity = Vector2.zero;
				GameEvent.BullteDestroyed (this);
				gameObject.SetActive (false);
            }
        }
    }
}
