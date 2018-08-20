using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame{
	public class Boom : MonoBehaviour {
		void Start () {
			Destroy (gameObject, 3.0f);
		}
	}
}
