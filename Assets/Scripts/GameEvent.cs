using UnityEngine;
using System.Collections;
using System;

namespace TestGame{
	public class GameEvent {

		public static event Action<Tank> 		OnTankDestroyed;
		public static event Action<Bullet> 		OnBulletDestroyed;

		public static void TankDestroyed(Tank tank) {
			if (OnTankDestroyed != null)
				OnTankDestroyed (tank);
		}

		public static void BullteDestroyed(Bullet bullet) {
			if (OnBulletDestroyed != null)
				OnBulletDestroyed (bullet);
		}
	}
}