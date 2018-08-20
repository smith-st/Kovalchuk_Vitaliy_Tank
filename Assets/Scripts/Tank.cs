using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Tank : MonoBehaviour, IMoveable {
		Rigidbody2D rb2d;
		[Range(1.0f,100.0f)]
		[SerializeField] protected float movingSpeed;

		[Range(1.0f,100.0f)]
		[SerializeField] protected float rotationSpeed;

		[SerializeField] GameObject boomPrefab;

		protected Rigidbody2D body{ get { return rb2d; } }

		protected bool isLive = true;

		public virtual void Respawn(){
			isLive = true;
		}

		protected virtual void Start(){
		
		}

		protected virtual void Awake(){
			rb2d = GetComponent<Rigidbody2D> ();
		}

		protected void ShowBoom(){
			if (boomPrefab != null)
				Instantiate (boomPrefab, transform.position, Quaternion.Euler (0f, 0f, Random.Range (0f, 359f)));
			else
				Debug.LogError ("Отсутсвует префаб взрыва");
		}

		void OnCollisionEnter2D(Collision2D col){
			if (col.gameObject.CompareTag ("tank")) {
				CollisionWithTank ();
			}else if (col.gameObject.CompareTag ("wall")) {
				CollisionWithWall ();
			}
		}

		void OnTriggerEnter2D(Collider2D col){
			if (col.gameObject.CompareTag ("bullet")) {
				CollisionWithBullet ();
			}
		}

		protected virtual void CollisionWithTank(){
		
		}
		protected virtual void CollisionWithWall(){
		
		}
		protected virtual void CollisionWithBullet(){
		
		}

		protected void DestroyMe(){
			isLive = false;
			GameEvent.TankDestroyed (this as Tank);
			ShowBoom ();
		}

		#region IMoveable implementation

		public void Direction (float value){
			if (value != 0.0f && isLive) {
				rb2d.AddForce (gameObject.transform.right * movingSpeed * value	);
			}
		}

		public void Rotate (float value){
			if(isLive)
				rb2d.AddTorque(rotationSpeed/1000.0f * -value , ForceMode2D.Impulse);
		}
		public void RotateImmediately (float value){
			if (isLive) {
				rb2d.angularVelocity = 0f;
				rb2d.MoveRotation (value);
			}
		}
		#endregion
	}
}
