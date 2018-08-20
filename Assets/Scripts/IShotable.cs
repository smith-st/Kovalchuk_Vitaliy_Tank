using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame{
	public interface IShotable {
		void Shot ();
		void AddBullet (Bullet bullet);
	}
}