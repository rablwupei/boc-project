using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public enum Anim {
		Stand,
		Run,
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
			}
		}

	}


}
