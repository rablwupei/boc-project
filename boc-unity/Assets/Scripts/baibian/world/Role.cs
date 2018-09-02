using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public abstract class Role : MonoBehaviour {

		public static RoleDidi CreateDidi() {
			var role = Instantiate(Resources.Load<RoleDidi>("baibian/logic/didi"));
			role.transform.SetParent(Baibian.instance.roles, false);
			return role;
		}

		public static RoleJiejie CreateJiejie() {
			var role = Instantiate(Resources.Load<RoleJiejie>("baibian/logic/jiejie"));
			role.transform.SetParent(Baibian.instance.roles, false);
			return role;
		}

		PolyNavAgent _agent;
		bool _findAgent;
		public PolyNavAgent agent {
			get {
				if (_agent == null && !_findAgent) {
					_agent = GetComponent<PolyNavAgent>();
					_findAgent = true;
				}
				return _agent;
			}
		}

		protected abstract string GetBodyPath(BaibianType type);

		Body _curBody;
		public Transform cameraFollow { get { return _curBody.cameraFollow; } }

		Dictionary<BaibianType, Body> _navCache = new Dictionary<BaibianType, Body>(DictionaryBaibianType.Default);
		Dictionary<BaibianType, Vector3> _posCache = new Dictionary<BaibianType, Vector3>(DictionaryBaibianType.Default);
		Dictionary<BaibianType, Quaternion> _rotCache = new Dictionary<BaibianType, Quaternion>(DictionaryBaibianType.Default);
		Dictionary<BaibianType, Vector3> _scaleCache = new Dictionary<BaibianType, Vector3>(DictionaryBaibianType.Default);
		Body GetBody(BaibianType type, out Vector3 pos, out Quaternion rot, out Vector3 scale) {
			Body ins;
			if (!_navCache.TryGetValue(type, out ins)) {
				var prefab = Resources.Load<Body>(GetBodyPath(type));
				ins = Instantiate(prefab);
				ins.transform.SetParent(transform, false);
				_navCache[type] = ins;
				pos = prefab.transform.localPosition;
				_posCache[type] = pos;
				rot = prefab.transform.localRotation;
				_rotCache[type] = rot;
				scale = prefab.transform.localScale;
				_scaleCache[type] = scale;
			} else {
				pos = _posCache[type];
				rot = _rotCache[type];
				scale = _scaleCache[type];
			}
			return ins;
		}

		public void HideOldRoleBody(BaibianType type) {
			if (_curBody) {
				if (agent) {
					agent.Stop();
				}
				_curBody.gameObject.SetActive(false);
				_curBody = null;
			}
		}


		public float alpha {
			set {
				_curBody.alpha = value;
			}
			get {
				return _curBody.alpha;
			}
		}

		public Color color {
			set {
				_curBody.color = value;
			}
			get {
				return _curBody.color;
			}
		}

		public void ShowNewRoleBody(BaibianType type) {
			Vector3 pos;
			Quaternion rot;
			Vector3 scale;
			_curBody = GetBody(type, out pos, out rot, out scale);
			if (_curBody) {
				_curBody.transform.localPosition = Vector3.zero;
				_curBody.transform.localRotation = Quaternion.identity;
				_curBody.transform.localScale = Vector3.one;
				transform.localPosition = pos;
				transform.localRotation = rot;
				transform.localScale = scale;
				_curBody.gameObject.SetActive(true);
				_lastY = float.MinValue;
			}
		}

		// anim

		public void Play(Anim type) {
			_curBody.Play(type);
		}

		// updatelayer

		float _lastY = float.MinValue;
		void LateUpdate() {
			var y = transform.localPosition.y;
			if (_lastY != y) {
				_curBody.UpdateSortingLayer(y);
				_lastY = y;
			}
		}

		// move

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

		public void StartMove(Vector3 screenPos) {
			if (_onMoveFinish == null) {
				_onMoveFinish = OnMoveFinish;
			}
			var value = agent.SetDestination(mainCamera.ScreenToWorldPoint(screenPos), _onMoveFinish);
			if (value) {
				Play(Anim.Run);
			}
		}

		public void StopMove(bool anim = true) {
			agent.Stop();
			if (anim) {
				Play(Anim.Stand);
			}
		}

		void OnMoveFinish(bool value) {
			Play(Anim.Stand);
		}


	}


}
