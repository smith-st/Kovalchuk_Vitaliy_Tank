using System.Collections.Generic;
using UnityEngine;


namespace TestGame{
	public class PlayerTank : Tank, IShotable {
		const float SHOT_DELAY= 0.5f;

		[SerializeField]
		Transform bulletStart;

		[SerializeField]
		GameObject bulletPrefab;
		// пул снярядов
		List <Bullet> pool = new List<Bullet> ();

		bool canShot = true;
		/// <summary>
		/// выстрел танка
		/// </summary>
		public void Shot(){
			if (canShot && isLive) {
				if (pool.Count > 0) {
					//используем снярд из пула
					pool[0].Shot(gameObject.transform.right,bulletStart.position);
					pool.RemoveAt (0);
				} else {
					//создаем новый снаряд
					GameObject go = Instantiate(bulletPrefab, bulletStart.position, Quaternion.identity);
					go.GetComponent<Bullet> ().Shot (gameObject.transform.right);
				}
				canShot = false;
				Invoke ("BullteReload", SHOT_DELAY);
			}
		}
		/// <summary>
		/// возвращаем снярд
		/// </summary>
		/// <param name="bullet">Bullet.</param>
		public void AddBullet(Bullet bullet){
			pool.Add (bullet);
		}
		/// <summary>
		/// столкновение с другим танком
		/// </summary>
		protected override void CollisionWithTank (){
			base.CollisionWithTank ();
			DestroyMe ();
		}
		/// <summary>
		/// перезарядка завершена
		/// </summary>
		void BullteReload(){
			canShot = true;
		}

	}
}