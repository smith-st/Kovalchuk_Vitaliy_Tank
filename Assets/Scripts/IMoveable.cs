using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame{
	public interface IMoveable {
		void Direction (float value);
		void Rotate (float value);
	}
}