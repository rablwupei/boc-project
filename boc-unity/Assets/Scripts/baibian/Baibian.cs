using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace wuyy {

	public enum BaibianType {
		tongnian,
		haiwai,
		chuguo,
		chengjia,
		zinv,
		wannian,
	}

	public class Baibian : MonoBehaviour {

		public static Baibian instance;

		public Canvas canvas;
		public RectTransform canvasTrans;
		public CameraFollow cameraFollow;
		public Transform bgfg;
		public Transform roles;
		public Transform nav;

		Baibian() {
			instance = this;
		}

		void OnDestroy() {
			if (instance == this) {
				instance = null;
			}
		}

		void Start() {
			Change(BaibianType.tongnian, true);
		}

		BaibianType _baibianType;

		public void Change(int type) {
			Change((BaibianType)type);
		}

		public void Change(BaibianType type, bool force = false) {
			if (_baibianType != type || force) {
				_baibianType = type;
				ChangeNav(type);
				ChangeBgfg(type);
				ChangeRoleBody(type);
			}
		}

		//camera

		Camera _camera;
		public Camera mainCamera {
			get {
				if (_camera == null) {
					_camera = cameraFollow.GetComponent<Camera>();
				}
				return _camera;
			}
		}


		//rolebody

		RoleDidi _roleDidi;
		RoleJiejie _roleJiejie;

		public void ChangeRoleBody(BaibianType type) {
			if (_roleDidi == null) {
				_roleDidi = Role.CreateDidi();
			}
			_roleDidi.agent.map = _curNav;
			_roleDidi.ChangeRoleBody(type);
			if (_roleJiejie == null) {
				_roleJiejie = Role.CreateJiejie();
			}
			_roleJiejie.ChangeRoleBody(type);
			cameraFollow.follow1 = _roleDidi.cameraFollow;
			cameraFollow.follow2 = _roleJiejie.cameraFollow;
		}


		//nav
		Dictionary<BaibianType, PolyNav2D> _navCache = new Dictionary<BaibianType, PolyNav2D>(DictionaryBaibianType.Default);
		PolyNav2D GetNav(BaibianType biabian) {
			PolyNav2D ins;
			if (!_navCache.TryGetValue(biabian, out ins)) {
				ins = Instantiate(Resources.Load<PolyNav2D>("baibian/nav/nav_" + biabian.ToString()));
				ins.transform.SetParent(nav, false);
				_navCache[biabian] = ins;
			}
			return ins;
		}
		PolyNav2D _curNav;
		public void ChangeNav(BaibianType type) {
			if (_curNav) {
				_curNav.gameObject.SetActive(false);
			}
			_curNav = GetNav(type);
			if (_curNav) {
				_curNav.gameObject.SetActive(true);
			}
		}

		//Bgfg

		Dictionary<BaibianType, Animation> _bgfgCache = new Dictionary<BaibianType, Animation>(DictionaryBaibianType.Default);
		Animation GetBgfg(BaibianType biabian) {
			Animation anim;
			if (!_bgfgCache.TryGetValue(biabian, out anim)) {
				anim = Instantiate(Resources.Load<Animation>("baibian/world/bgfg_" + biabian.ToString()));
				anim.transform.SetParent(bgfg, false);
				_bgfgCache[biabian] = anim;
			}
			return anim;
		}

		Animation _curBgfg;
		public void ChangeBgfg(BaibianType type) {
			if (_curBgfg) {
				_curBgfg.gameObject.SetActive(false);
			}
			_curBgfg = GetBgfg(type);
			if (_curBgfg) {
				_curBgfg.gameObject.SetActive(true);
				_curBgfg.Play("chuxian");
				_curBgfg.PlayQueued("stand");
			}
		}

		// touch

		public static Vector2 WorldToCanvas(RectTransform canvas_rect, Vector3 viewport_position) {
			return new Vector2((viewport_position.x * canvas_rect.sizeDelta.x) - (canvas_rect.sizeDelta.x * 0.5f),
				(viewport_position.y * canvas_rect.sizeDelta.y) - (canvas_rect.sizeDelta.y * 0.5f));
		}

		PointerEventData _pressEvent;
		bool _pressRole;
		float _pressTime;

		public void UITouchPress(BaseEventData data) {
			var eventData = (PointerEventData)data;
			var screenPos = eventData.position;
			var ray = mainCamera.ScreenPointToRay(screenPos);
			var hit = Physics2D.GetRayIntersection(ray);
			if (hit.collider != null) {
				uiOptionRoot.SetActive(true);
				var viewPos = mainCamera.WorldToViewportPoint(hit.transform.position);
				uiOptionPanel.anchoredPosition = WorldToCanvas(canvasTrans, viewPos);
				_pressRole = true;
				_pressEvent = null;
				_roleDidi.StopMove();
			} else {
				_pressRole = false;
				_pressEvent = eventData;
				_pressTime = Time.realtimeSinceStartup;
				_roleDidi.StartMove(_pressEvent.position);
			}
		}

		public void UITouchUp(BaseEventData data) {
			if (!_pressRole) {
				if (Time.realtimeSinceStartup - _pressTime < 0.2f) {
					_roleDidi.StartMove(_pressEvent.position);
				} else {
					_roleDidi.StopMove();
				}
			}
			_pressEvent = null;
		}

		void Update() {
			if (_pressEvent != null) {
				_roleDidi.StartMove(_pressEvent.position);
			}
		}

		// UI

		public GameObject uiOptionRoot;
		public RectTransform uiOptionPanel;

	}

	class DictionaryBaibianType : IEqualityComparer<BaibianType> {
		public static DictionaryBaibianType Default = new DictionaryBaibianType();
		public bool Equals (BaibianType x, BaibianType y) {
			return x == y;
		}
		public int GetHashCode (BaibianType obj) {
			return ((int)obj).GetHashCode();
		}
	}

}
