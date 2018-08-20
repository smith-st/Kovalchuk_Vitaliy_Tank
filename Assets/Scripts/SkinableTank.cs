using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestGame{
	public class SkinableTank : Tank {
		public List<Sprite> skin = new List<Sprite>(); 
		SpriteRenderer spriteRenderer;

		protected override void Awake (){
			base.Awake ();
			spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer> ();
			SetSkin ();
		}

		private void SetSkin(){
			if (spriteRenderer != null) 
				spriteRenderer.sprite = skin [Random.Range (0, skin.Count)];
		}

		public override void Respawn (){
			base.Respawn ();
			SetSkin ();
		}

	}
}
