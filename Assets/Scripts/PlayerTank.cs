using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TestGame{
	public class PlayerTank : Tank, IShotable {
		[SerializeField]
		private Transform bulletStart;

		[SerializeField]
		private GameObject bulletPrefab;

		private List <Bullet> pool = new List<Bullet> ();

		private float shotDelay = 0.5f;
		private bool canShot = true;

		public void Shot(){
			if (canShot && isLive) {
				if (pool.Count > 0) {
					pool[0].Shot(gameObject.transform.right,bulletStart.position);
					pool.RemoveAt (0);
				} else {
					GameObject go = Instantiate(bulletPrefab, bulletStart.position, Quaternion.identity);
					go.GetComponent<Bullet> ().Shot (gameObject.transform.right);
				}
				canShot = false;
				Invoke ("BullteReload", shotDelay);
			}
		}

		public void AddBullet(Bullet bullet){
			pool.Add (bullet);
		}

		void BullteReload(){
			canShot = true;
		}

		protected override void CollisionWithTank (){
			base.CollisionWithTank ();
			DestroyMe ();
		}
	}
}