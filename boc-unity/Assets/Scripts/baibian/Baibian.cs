using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace wuyy {

	public enum BaibianType {
		tongnian,
		liuxue,
		chengjia,
		caifu,
		wannian,
		none,
	}

	public class Baibian : MonoBehaviour {

		public static Baibian instance;

		public Canvas canvas;
		public RectTransform canvasTrans;
		public CameraFollow cameraFollow;
		public Transform bgfg;
		public Transform roles;
		public Transform nav;
		public List<AudioSource> audios;

		public UIHuanying uiHuanying;
		public UIGuocheng uiGuocheng;
		public UIShouye uiShouye;
		public UIDuihuakuang uiDuihuakuang;
		public UIWeb prefabUIWeb;

		GameObject _bgShouye;
		public GameObject bgShouye {
			get {
				if (!_bgShouye) {
					var prefab = Resources.Load<GameObject>("baibian/world/bgfg/bgfg_shouye");
					_bgShouye = Instantiate(prefab, bgfg);
				}
				return _bgShouye;
			}
		}

		Baibian() {
			instance = this;
		}

		void OnDestroy() {
			if (instance == this) {
				instance = null;
			}
		}

		public const string Key_volume = "baibian_volume";

		void Start() {
			AudioListener.volume = PlayerPrefs.GetFloat(Key_volume, 1f);

			uiDuihuakuang.gameObject.SetActive(false);
			uiShouye.gameObject.SetActive(false);
			uiGuocheng.gameObject.SetActive(false);
			uiHuanying.gameObject.SetActive(true);
		}

		BaibianType _baibianType;

		public void Change(int type, bool force = false) {
			Change((BaibianType)type, force);
		}

		public void Change(BaibianType type, bool force = false) {
			var isNone = type == BaibianType.none;
			uiGuocheng.gameObject.SetActive(!isNone);
			uiShouye.gameObject.SetActive(isNone);
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
			if (type == BaibianType.none) {
				if (_roleDidi != null) {
					_roleDidi.gameObject.SetActive(false);
				}
				if (_roleJiejie != null) {
					_roleJiejie.gameObject.SetActive(false);
				}
				cameraFollow.follow1 = null;
				cameraFollow.follow2 = null;
				return;
			}
			if (_roleDidi == null) {
				_roleDidi = Role.CreateDidi();
			}
			_roleDidi.agent.map = _curNav;
			_roleDidi.ChangeRoleBody(type);
			_roleDidi.gameObject.SetActive(true);
			if (_roleJiejie == null) {
				_roleJiejie = Role.CreateJiejie();
			}
			_roleJiejie.ChangeRoleBody(type);
			_roleJiejie.gameObject.SetActive(true);
			cameraFollow.follow1 = _roleDidi.cameraFollow;
			cameraFollow.follow2 = _roleJiejie.cameraFollow;
		}


		//nav
		Dictionary<BaibianType, PolyNav2D> _navCache = new Dictionary<BaibianType, PolyNav2D>(DictionaryBaibianType.Default);
		PolyNav2D GetNav(BaibianType type) {
			if (type == BaibianType.none) {
				return null;
			}
			PolyNav2D ins;
			if (!_navCache.TryGetValue(type, out ins)) {
				ins = Instantiate(Resources.Load<PolyNav2D>("baibian/nav/nav_" + type.ToString()));
				ins.transform.SetParent(nav, false);
				_navCache[type] = ins;
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
		Animation GetBgfg(BaibianType type) {
			if (type == BaibianType.none) {
				return null;
			}
			Animation anim;
			if (!_bgfgCache.TryGetValue(type, out anim)) {
				anim = Instantiate(Resources.Load<Animation>("baibian/world/bgfg/bgfg_" + type.ToString()));
				anim.transform.SetParent(bgfg, false);
				_bgfgCache[type] = anim;
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
				var role = hit.transform.GetComponentInParent<Role>();
				if (role != null) {
					if (role is RoleJiejie) {
						var viewPos = mainCamera.WorldToViewportPoint(hit.transform.position);
						uiGuocheng.OpenMenu(WorldToCanvas(canvasTrans, viewPos), _baibianType);
					} else {
						//PlayAnim
					}
					_pressRole = true;
					_pressEvent = null;
					_roleDidi.StopMove();
				}
			} else {
				if (_baibianType != BaibianType.wannian) {
					_pressRole = false;
					_pressEvent = eventData;
					_pressTime = Time.realtimeSinceStartup;
					_roleDidi.StartMove(_pressEvent.position);
				}
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

		//sound

		void RestartAllSound() {
			foreach (var item in audios) {
				item.gameObject.SetActive(true);
			}
		}

		void StopAllSound() {
			foreach (var item in audios) {
				item.gameObject.SetActive(false);
			}
		}

		//web

		public void OpenUrl(WebType type) {
			if (UIWeb.Show(type, RestartAllSound)) {
				StopAllSound();
			}
		}


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
