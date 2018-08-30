using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestGame{
	public class SkinableTank : Tank {
		public List<Sprite> skin = new List<Sprite>(); 
		SpriteRenderer spriteRenderer;

		public override void Respawn (){
			base.Respawn ();
			SetSkin ();
		}

		protected override void Awake (){
			base.Awake ();
			spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer> ();
			SetSkin ();
		}
		/// <summary>
		/// задает новый скин
		/// </summary>
		void SetSkin(){
			if (spriteRenderer != null) 
				spriteRenderer.sprite = skin [Random.Range (0, skin.Count)];
		}
	}
}
