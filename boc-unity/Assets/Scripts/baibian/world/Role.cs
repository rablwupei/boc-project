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
		Body GetBody(BaibianType type, out Vector3 pos) {
			Body ins;
			if (!_navCache.TryGetValue(type, out ins)) {
				var prefab = Resources.Load<Body>(GetBodyPath(type));
				ins = Instantiate(prefab);
				ins.transform.SetParent(transform, false);
				_navCache[type] = ins;
				pos = prefab.transform.localPosition;
				_posCache[type] = pos;
			} else {
				pos = _posCache[type];
			}
			return ins;
		}

		public void ChangeRoleBody(BaibianType type) {
			if (_curBody) {
				_curBody.gameObject.SetActive(false);
				if (agent) {
					agent.Stop();
				}
			}
			Vector3 pos;
			_curBody = GetBody(type, out pos);
			_curBody.transform.localPosition = Vector3.zero;
			transform.localPosition = new Vector3(pos.x, pos.y, pos.y);
			if (_curBody) {
				_curBody.gameObject.SetActive(true);
			}
		}

		public void Play(Anim type) {
			_curBody.Play(type);
		}

	}


}
