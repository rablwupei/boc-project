using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public enum Anim {
		Stand,
		Run,
		Dianji,
	}

	public class Body : MonoBehaviour {

		public Transform cameraFollow;

		Animation _anim;
		Animation anim {
			get {
				if (!_anim) {
					_anim = GetComponentInChildren<Animation>();
				}
				return _anim;
			}
		}

		public void Play(Anim type) {
			switch (type) {
			case Anim.Run:
				anim.Play("run");
				break;
			case Anim.Stand:
				anim.Play("stand");
				break;
			case Anim.Dianji:
				anim.Play("dianji");
				anim.PlayQueued("stand");
				break;
			}
		}

		SpriteRenderer[] _renders;
		int[] _rendersSort;
		bool _cache;

		void Cache() {
			_renders = GetComponentsInChildren<SpriteRenderer>(true);
			_rendersSort = new int[_renders.Length];
			for (int i = 0; i < _renders.Length; i++) {
				_rendersSort[i] = _renders[i].sortingOrder;
			}
			_cache = true;
		}

		public void UpdateSortingLayer(float y) {
			if (!_cache) Cache();
			for (int i = 0; i < _renders.Length; i++) {
				_renders[i].sortingOrder = _rendersSort[i] + (int)(-y * 1000);
			}
		}

		public float alpha {
			set {
				if (!_cache) Cache();
				Color color;
				for (int i = 0; i < _renders.Length; i++) {
					color = _renders[i].color;
					color.a = value;
					_renders[i].color = color;
				}
			}
			get {
				if (!_cache) Cache();
				Color color;
				for (int i = 0; i < _renders.Length; i++) {
					color = _renders[i].color;
					return color.a;
				}
				throw new System.Exception();
			}
		}

		public Color color {
			set {
				if (!_cache) Cache();
				for (int i = 0; i < _renders.Length; i++) {
					_renders[i].color = value;
				}
			}
			get {
				if (!_cache) Cache();
				for (int i = 0; i < _renders.Length; i++) {
					return _renders[i].color;
				}
				throw new System.Exception();
			}
		}
	}


}
