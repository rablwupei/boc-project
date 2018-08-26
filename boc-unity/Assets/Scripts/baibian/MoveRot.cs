using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace net.wuyy {

	public class MoveRot : MonoBehaviour {

		public bool doFlip = true;

		private Vector2 lastDir;
		private float originalScaleX;

		private PolyNavAgent _agent;
		private PolyNavAgent agent{
			get {return _agent != null? _agent : _agent = GetComponent<PolyNavAgent>();}
		}

		void Awake(){
			originalScaleX = transform.localScale.x;
		}

		void Update() {

			var dir = agent.movingDirection;
			var x = Mathf.Round(dir.x);
			var y = Mathf.Round(dir.y);

			//eliminate diagonals favoring x over y
			y = Mathf.Abs(y) == Mathf.Abs(x)? 0 : y;

			dir = new Vector2(x, y);

			if (dir != lastDir){

				if (dir == Vector2.zero){
					//Debug.Log("IDLE");
				}

				if (dir.x == 1){
					//Debug.Log("RIGHT");
					if (doFlip){
						var scale = transform.localScale;
						scale.x = -originalScaleX;
						transform.localScale = scale;
					}
				}

				if (dir.x == -1){
					//Debug.Log("LEFT");
					if (doFlip){
						var scale = transform.localScale;
						scale.x = originalScaleX;
						transform.localScale = scale;
					}
				}

				if (dir.y == 1){
					//Debug.Log("UP");
				}

				if (dir.y == -1){
					//Debug.Log("DOWN");
				}

				lastDir = dir;
			}
		}
	}

}
