using UnityEngine;

namespace TestGame {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Tank : MonoBehaviour, IMoveable {

		Rigidbody2D rb2d;
		[Range(1.0f,100.0f)]
		[SerializeField]
		protected float movingSpeed;

		[Range(1.0f,100.0f)]
		[SerializeField]
		protected float rotationSpeed;

		[SerializeField]
		GameObject boomPrefab;

		Boom boomAnim;

		protected Rigidbody2D body{ get { return rb2d; } }
		protected bool isLive = true;

		#region IMoveable implementation
		/// <summary>
		/// движение вперед
		/// </summary>
		/// <param name="value">Value.</param>
		public void Direction (float value){
			if (value != 0.0f && isLive) {
				rb2d.AddForce (gameObject.transform.right * movingSpeed * value	);
			}
		}
		/// <summary>
		/// поворот в указаную сторону
		/// </summary>
		/// <param name="value">Value.</param>
		public void Rotate (float value){
			if(isLive)
				rb2d.AddTorque(rotationSpeed/1000.0f * -value , ForceMode2D.Impulse);
		}
		/// <summary>
		/// поворот танка на нужный угол немеделнно, без анимации
		/// </summary>
		/// <param name="value">Value.</param>
		public void RotateImmediately (float angle){
			if (isLive) {
				rb2d.angularVelocity = 0f;
				rb2d.MoveRotation (angle);
			}
		}
		#endregion

		protected virtual void Start(){
		
		}

		protected virtual void Awake(){
			rb2d = GetComponent<Rigidbody2D> ();
		}
		/// <summary>
		/// перерождение
		/// </summary>
		public virtual void Respawn(){
			isLive = true;
		}
		/// <summary>
		/// показывает анимацю взрыва
		/// </summary>
		protected void ShowBoom(){
			if (boomAnim != null) {
				boomAnim.Play (gameObject.transform.position);
			} else if (boomPrefab != null) {
				GameObject go = Instantiate (boomPrefab, transform.position, Quaternion.Euler (0f, 0f, Random.Range (0f, 359f)));
				boomAnim = go.GetComponent<Boom>();
			} else {
				Debug.LogError ("Отсутсвует префаб взрыва");
			}
		}

		/// <summary>
		/// столкновение с другим танком
		/// </summary>
		protected virtual void CollisionWithTank(){	}
		/// <summary>
		/// столкновение со стеной
		/// </summary>
		protected virtual void CollisionWithWall(){	}
		/// <summary>
		/// столкновение со снярядом
		/// </summary>
		protected virtual void CollisionWithBullet(){ }
		/// <summary>
		/// уничтожение танка
		/// </summary>
		protected void DestroyMe(){
			isLive = false;
			GameEvent.TankDestroyed (this);
			ShowBoom ();
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
	}
}
