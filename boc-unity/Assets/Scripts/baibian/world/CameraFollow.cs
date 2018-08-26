using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public class CameraFollow : MonoBehaviour {

		public float min_x;
		public float min_y;
		public float max_x;
		public float max_y;

		public Transform follow1;
		public Transform follow2;

		void LateUpdate () {
			if (follow1 && follow2) {
//				if (_smoothTime < _smoothTimeMax) {
//					cachedTrans.localPosition = Vector3.Lerp(_smoothLastPos, followPos, _smoothTime / _smoothTimeMax);
//					_smoothTime += Time.deltaTime;
//				} else {
//					cachedTrans.localPosition = Vector3.SmoothDamp(cachedTrans.localPosition, followPos, ref _tmpV3, 0.01f);
//				}
				var pos = follow1.position * 0.5f + follow2.position * 0.5f;
				var x = Mathf.Clamp(pos.x, min_x, max_x);
				var y = Mathf.Clamp(pos.y, min_y, max_y);
				var z = pos.z;
				transform.position = new Vector3(x, y, z);
			}
		}
	}


}
