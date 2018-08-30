using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGame;
using System.Threading;
using System.Linq;

namespace TestGame{
	public class GameController : MonoBehaviour {

		public PlayerController playerController;
		public GameObject playerTankPrefab;
		public GameObject evilTankPrefab;
		public List<Transform> respawnPoints = new List<Transform> ();
		List<Tank> forRespawn = new List<Tank> ();


		void Start(){
			bool addPlayerTank = false;
			GameObject go;
			//добавляем танки на точки появления
			foreach (Transform item in respawnPoints) {
				if (!addPlayerTank) {
					addPlayerTank = true;
					go = AddTank (playerTankPrefab, item.position);
					if (playerController)
						playerController.SetPlayer (go);
				} else {
					AddTank (evilTankPrefab, item.position);
				}
			}

			GameEvent.OnTankDestroyed += GameEvent_OnTankDestroyed;
			GameEvent.OnBulletDestroyed += GameEvent_OnBulletDestroyed;
		}

		void OnDestroy(){
			GameEvent.OnTankDestroyed -= GameEvent_OnTankDestroyed;
			GameEvent.OnBulletDestroyed -= GameEvent_OnBulletDestroyed;
		}
		/// <summary>
		/// Снярд уничтожился, возвращаем его игроку
		/// </summary>
		void GameEvent_OnBulletDestroyed (Bullet bullet){
			playerController.SetBullet (bullet);
		}
		/// <summary>
		/// танк уничтожен, перерождаем его через 1 сек
		/// </summary>
		void GameEvent_OnTankDestroyed (Tank tank){
			forRespawn.Add (tank);
			tank.gameObject.SetActive (false);
			if (!IsInvoking ("RespawnTank"))
				Invoke ("RespawnTank", 1f);
		}

		/// <summary>
		/// добавляет танк на игровое поле
		/// </summary>
		/// <returns>The tank.</returns>
		/// <param name="prefab">префаб танка</param>
		/// <param name="point">точка появления</param>
		GameObject AddTank(GameObject prefab, Vector2 point){
			return Instantiate(prefab, point, Quaternion.Euler (0f, 0f, (float)Random.Range (1, 4) * 90f));
		}
		/// <summary>
		/// перерождение танка
		/// </summary>
		void RespawnTank(){
			if (forRespawn [0] != null) {
				forRespawn [0].gameObject.SetActive (true);
				forRespawn [0].gameObject.transform.position = respawnPoints [Random.Range (0, respawnPoints.Count)].position;
				forRespawn [0].Respawn ();
				forRespawn.RemoveAt (0);
				if (forRespawn.Count > 0)
					Invoke ("RespawnTank", 1f);
			}
		}
	}
}
