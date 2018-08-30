using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGame;

namespace TestGame{
	public class PlayerController : MonoBehaviour {

		IMoveable tankMove;
		IShotable tankShot;

		/// <summary>
		/// объект игрока
		/// </summary>
		public void SetPlayer(GameObject obj){
			tankMove = obj.GetComponent<IMoveable> ();
			tankShot = obj.GetComponent<IShotable> ();
		}

		/// <summary>
		/// возвращает снярд выпущенный игроком
		/// </summary>
		public void SetBullet(Bullet bullet){
			if (tankShot != null) {
				tankShot.AddBullet (bullet);
			} else {
				Destroy (bullet.gameObject);
			}
		}
		/// <summary>
		////движение танка игрока
		/// </summary>
		void FixedUpdate () {
			if (tankMove != null) {
				tankMove.Direction (Input.GetAxis ("Vertical"));
				tankMove.Rotate (Input.GetAxis ("Horizontal"));
			}

		}

		/// <summary>
		/// выстрел такна игрока
		/// </summary>
		void Update () {
			if (tankShot != null) {
				if (Input.GetButton ("Fire1"))
					tankShot.Shot ();
			}

		}
	}
}
