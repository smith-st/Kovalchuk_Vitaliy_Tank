using UnityEngine;

namespace TestGame{
	public class EvilTank : SkinableTank {
		enum Status{
			MOVE,
			ROTATE			
		}

		float rotateDirection = 0f;// каким боком поворачиваться
		float rotateTo = -1f;//до какго угла
		float gap = 1f;//зазор поворота
		int movingTime = 0;
		Status status;

		protected override void Start (){
			base.Start ();
			gap = rotationSpeed*0.1f;
			StartMove ();
		}
		protected override void CollisionWithBullet (){
			base.CollisionWithBullet ();
			DestroyMe ();
		}

		protected override void CollisionWithWall (){
			base.CollisionWithWall ();
			Obstacle ();
		}

		protected override void CollisionWithTank (){
			base.CollisionWithTank ();
			Obstacle ();
		}
		/// <summary>
		/// движение танка вперед
		/// </summary>
		void StartMove(){
			status = Status.MOVE;
			rotateDirection = 0f;
			rotateTo = -1f;
			movingTime = Random.Range (50, 100);
		}
		/// <summary>
		/// поворот танка
		/// </summary>
		void StartRotate(){
			status = Status.ROTATE;
			movingTime = 0;
			float tankAngle = normalizedAngle(this.body.rotation);
			tankAngle -= tankAngle % 90f;

			int dir = Random.Range (1, 4);
			switch(dir){
			case 1:	//вправо
				rotateTo = tankAngle -= 90f;
				rotateDirection = 1f;
				break;
			case 2:	//влево
				rotateTo = tankAngle += 90f;
				rotateDirection = -1f;
				break;
			case 3:	//влево
				rotateTo = tankAngle += 180f;
				rotateDirection = 1f;
				break;
			}
			rotateTo = normalizedAngle (rotateTo);
		}

		void FixedUpdate(){
			if (status == Status.MOVE) {
				//если танк двигается
				movingTime--;
				if (movingTime > 0)
					this.Direction (1f);
				else
					StartRotate ();
			} else {
				//если разворачивается
				if (rotateTo != -1f) {
					this.Rotate (rotateDirection);				
					float angl = normalizedAngle (this.body.rotation);
					if (
							angl >= rotateTo - gap
						&&	angl <= rotateTo + gap
					) {
						//если повернулся до нужного угла, выравниваем танк и продожаем движение
						this.RotateImmediately (rotateTo);
						StartMove ();
					}
				}
			}
		}
		/// <summary>
		/// столковение с преградой
		/// </summary>
		void Obstacle(){
			if (status == Status.MOVE) {//если танк сейчас движется
				StartRotate ();
			}
		}
		/// <summary>
		/// преобразует угол от 0 до 360
		/// </summary>
		/// <returns>The angle.</returns>
		/// <param name="angle">Angle.</param>
		float normalizedAngle(float angle){
			float norm = angle % 360f;
			if (norm < 0f)
				norm += 360f;
			return norm;
		}
	}
}
