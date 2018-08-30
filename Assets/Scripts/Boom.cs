using UnityEngine;

namespace TestGame{
	[RequireComponent(typeof(Animator))]
	public class Boom : MonoBehaviour {
		Animator animator;
		readonly int boomHash = Animator.StringToHash("boom");

		void Awake (){
			animator = gameObject.GetComponent<Animator> ();
		}
		public void Play(Vector2 vec){
			gameObject.transform.position = vec;
			animator.Play (boomHash, -1,0f);
		}
	}
}
