using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace net.wuyy {

	public class CameraFollow : MonoBehaviour {

		public Transform follow;

		void LateUpdate () {
			if (follow) {
				transform.position = follow.position;
			}
		}
	}


}
