using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

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
		public AudioSource audioMusic;
		public AudioSource audioSound;

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

			uiHuanying.gameObject.SetActive(true);
			Reset();
		}

		void Reset() {
			uiDuihuakuang.gameObject.SetActive(false);
			uiShouye.gameObject.SetActive(false);
			uiGuocheng.gameObject.SetActive(false);
			Change(BaibianType.none, true);
		}

		BaibianType _baibianType;
		public BaibianType baibianType { get { return _baibianType; } }

		public void Change(int type, bool force = false) {
			Change((BaibianType)type, force);
		}

		public void Change(BaibianType type, bool force = false) {
			if (!_isChanging) {
				StartCoroutine(DoChange(type, force));
			}
		}

		bool _isChanging;

		IEnumerator DoChange(BaibianType type, bool force = false) {
			_isChanging = true;
			var isNone = type == BaibianType.none;
			uiGuocheng.gameObject.SetActive(!isNone);
			if (_baibianType != type || force) {
				_baibianType = type;
				ChangeNav(type);
				yield return ChangeBgfg(type);
				if (isNone) {
					uiShouye.Show();
				}
			}
			_isChanging = false;
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
		const float FADE_TIME = 0.2f;

		public IEnumerator HideOldRoleBody(BaibianType type) {
			if (_roleDidi == null) {
				yield break;
			}

			if (type == BaibianType.none) {
				cameraFollow.follow1 = null;
				cameraFollow.follow2 = null;
				StopSound();
			}

			var tween1 = DOTween.ToAlpha(()=> _roleDidi.color, x=> _roleDidi.color = x, 0, FADE_TIME);
			var tween2 = DOTween.ToAlpha(()=> _roleJiejie.color, x=> _roleJiejie.color = x, 0, FADE_TIME);
			while (tween1.IsPlaying() || tween2.IsPlaying()) {
				yield return null;
			}
			_roleDidi.HideOldRoleBody(type);
			_roleDidi.gameObject.SetActive(false);
			_roleJiejie.HideOldRoleBody(type);
			_roleJiejie.gameObject.SetActive(false);
		}

		public IEnumerator ReadyNewRoleBody(BaibianType type) {
			if (type == BaibianType.none) {
				yield break;
			}

			if (_roleDidi == null) {
				_roleDidi = Role.CreateDidi();
			}
			if (_roleJiejie == null) {
				_roleJiejie = Role.CreateJiejie();
			}

			_roleDidi.agent.map = _curNav;
			_roleDidi.ShowNewRoleBody(type);
			_roleDidi.gameObject.SetActive(true);

			_roleJiejie.ShowNewRoleBody(type);
			_roleJiejie.gameObject.SetActive(true);

			cameraFollow.follow1 = _roleDidi.cameraFollow;
			cameraFollow.follow2 = _roleJiejie.cameraFollow;

			_roleDidi.alpha = 0f;
			_roleJiejie.alpha = 0f;
		}

		public IEnumerator ShowNewRoleBody(BaibianType type) {
			if (type == BaibianType.none) {
				yield break;
			}

			var tween1 = DOTween.ToAlpha(()=> _roleDidi.color, x=> _roleDidi.color = x, 1, FADE_TIME);
			var tween2 = DOTween.ToAlpha(()=> _roleJiejie.color, x=> _roleJiejie.color = x, 1, FADE_TIME);
			while (tween1.IsPlaying() || tween2.IsPlaying()) {
				yield return null;
			}
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
		public IEnumerator ChangeBgfg(BaibianType type) {
			StopSound();
			yield return HideOldRoleBody(type);
			if (_curBgfg) {
				_curBgfg["chuxian"].speed = -1f;
				_curBgfg["chuxian"].normalizedTime = 1f;
				_curBgfg.Play("chuxian");
				while (_curBgfg.isPlaying) {
					yield return null;
				}
				_curBgfg.gameObject.SetActive(false);
			}
			yield return ReadyNewRoleBody(type);
			_curBgfg = GetBgfg(type);
			if (_curBgfg) {
				_curBgfg.gameObject.SetActive(true);
				_curBgfg["chuxian"].normalizedTime = 0f;
				_curBgfg["chuxian"].speed = 1;
				_curBgfg.Play("chuxian");
				while (_curBgfg.isPlaying) {
					yield return null;
				}
				_curBgfg.Play("stand");
			}
			yield return ShowNewRoleBody(type);
			if (type != BaibianType.none) {
				uiDuihuakuang.Show(type);
				foreach (var item in uiGuocheng.menus) {
					if (item.type == type) {
						PlaySound(item.audioClip);
						break;
					}
				}
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
			if (_isChanging) {
				return;
			}
			var eventData = (PointerEventData)data;
			var screenPos = eventData.position;
			var ray = mainCamera.ScreenPointToRay(screenPos);
			var hit = Physics2D.GetRayIntersection(ray);
			if (hit.collider != null) {
				var role = hit.transform.GetComponentInParent<Role>();
				if (role != null) {
					if (role is RoleJiejie) {
						_roleDidi.StopMove(true);
						var viewPos = mainCamera.WorldToViewportPoint(hit.transform.position);
						uiGuocheng.OpenMenu(WorldToCanvas(canvasTrans, viewPos), _baibianType);
					} else {
						_roleDidi.StopMove(false);
						role.Play(Anim.Dianji);
					}
					_pressRole = true;
					_pressEvent = null;
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
			if (_pressEvent != null && !_pressRole) {
				if (Time.realtimeSinceStartup - _pressTime < 0.2f) {
					_roleDidi.StartMove(_pressEvent.position);
				} else {
					_roleDidi.StopMove();
				}
			}
			_pressEvent = null;
		}

		// close auto

		float _lastKeyDown;
		void Update() {
			if (_pressEvent != null) {
				_roleDidi.StartMove(_pressEvent.position);
			}
			if (Input.anyKeyDown) {
				_lastKeyDown = Time.realtimeSinceStartup;
			} else {
				if (Time.realtimeSinceStartup - _lastKeyDown > 3 * 60f &&
					!uiHuanying.gameObject.activeSelf && !_isChanging) {
					Reset();
					uiHuanying.Open();
				}
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

		public void PlaySound(AudioClip clip) {
			audioSound.Stop();
			audioSound.PlayOneShot(clip);
		}

		public void StopSound() {
			if (audioSound) {
				audioSound.Stop();
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
