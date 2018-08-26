using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public class ClickMove : MonoBehaviour {

		void OnEnable() {
			Baibian.onTouchClick += TouchClick;
		}

		void OnDisable() {
			Baibian.onTouchClick -= TouchClick;
		}

		Role _role;
		Role role {
			get {
				if (_role == null) {
					_role = GetComponent<Role>();
				}
				return _role;
			}
		}

		Camera _camera;
		Camera mainCamera {
			get {
				if (_camera == null) {
					_camera = Baibian.instance.mainCamera;
				}
				return _camera;
			}
		}

		System.Action<bool> _onMoveFinish;

		void TouchClick(Vector3 screenPos) {
			if (_onMoveFinish == null) {
				_onMoveFinish = OnMoveFinish;
			}
			var value = role.agent.SetDestination(mainCamera.ScreenToWorldPoint(screenPos), _onMoveFinish);
			if (value) {
				role.Play(Anim.Run);
			}
		}

		void OnMoveFinish(bool value) {
			role.Play(Anim.Stand);
		}

	}


}
