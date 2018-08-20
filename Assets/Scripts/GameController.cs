using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGame;
using System.Threading;
using System.Linq;

namespace TestGame{
	public class GameController : MonoBehaviour {

		public GameObject playerTankPrefab;
		public GameObject evilTankPrefab;
		public List<Transform> respawnPoints = new List<Transform> ();
		private List<Tank> forRespawn = new List<Tank> ();

		private IMoveable tankMove;
		private IShotable tankShot;

		void Start(){

			bool addPlayerTank = false;
			GameObject go;
			foreach (Transform item in respawnPoints) {
				if (!addPlayerTank) {
					addPlayerTank = true;
					go = AddTank (playerTankPrefab, item.position);
					tankMove = go.GetComponent<IMoveable> ();
					tankShot = go.GetComponent<IShotable> ();
				} else {
					go = AddTank (evilTankPrefab, item.position);
				}
			}

			GameEvent.OnTankDestroyed += GameEvent_OnTankDestroyed;
			GameEvent.OnBulletDestroyed += GameEvent_OnBulletDestroyed;
		}

		void GameEvent_OnBulletDestroyed (Bullet bullet){
			if (tankShot != null) {
				tankShot.AddBullet (bullet);
			}
		}

		void OnDestroy(){
			GameEvent.OnTankDestroyed -= GameEvent_OnTankDestroyed;
			GameEvent.OnBulletDestroyed -= GameEvent_OnBulletDestroyed;
		}

		void GameEvent_OnTankDestroyed (Tank tank){
			forRespawn.Add (tank);
			tank.gameObject.SetActive (false);
			if (!IsInvoking ("RespawnTank"))
				Invoke ("RespawnTank", 1f);
		}

		GameObject AddTank(GameObject prefab, Vector2 point){
			return Instantiate(prefab, point, Quaternion.Euler (0f, 0f, (float)Random.Range (1, 4) * 90f));
		}

		void RespawnTank(){
			if (forRespawn [0] != null) {
				int ind = Random.Range (0, respawnPoints.Count);
				forRespawn [0].gameObject.SetActive (true);
				forRespawn [0].gameObject.transform.position = new Vector3 (
					respawnPoints [ind].position.x,
					respawnPoints [ind].position.y,
					respawnPoints [ind].position.z
				); 
				forRespawn [0].Respawn ();
				forRespawn.RemoveAt (0);
				if (forRespawn.Count > 0)
					Invoke ("RespawnTank", 1f);
			}
		}

		void FixedUpdate () {
			if (tankMove != null) {
				tankMove.Direction (Input.GetAxis ("Vertical"));
				tankMove.Rotate (Input.GetAxis ("Horizontal"));
			}

		}
		void Update () {
			if (tankShot != null) {
				if (Input.GetButton ("Fire1"))
					tankShot.Shot ();
			}

		}
	}
}
