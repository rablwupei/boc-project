using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace wuyy.fk {

	public class Fengkong : MonoBehaviour {

		public static Fengkong instance;

		Fengkong() {
			instance = this;
		}

		void OnDestroy () {
			if (instance == this) {
				instance = null;
			}
		}

		void Start() {
			Screen.fullScreen = true;
			Cursor.visible = true;
			Input.multiTouchEnabled = false;
			AudioListener.volume = PlayerPrefs.GetFloat(Key_volume, 1f);
			DOTween.defaultEaseType = Ease.Linear;

			Next();
		}

		public const string Key_volume = "baibian_volume";

		void Update() {
			if (Input.GetKeyDown(KeyCode.Tab)) {
				Cursor.visible = !Cursor.visible;
			}
		}

		public bool _fastPlay = false;

		public static bool isFast { get { return instance._fastPlay; } }
		public static bool isNormal { get { return !isFast; } }


		public Transform uiRoot;
		public List<UIAbstract> uis;

		int _index;
		UIAbstract _ui;

		public void Next() {
			if (_ui) {
				_ui.gameObject.SetActive(false);
				GameObject.Destroy(_ui.gameObject);
				_ui = null;
			}
			_ui = Instantiate(uis[_index]);
			_ui.transform.SetParent(uiRoot, false);
			_ui.Init();
			_index = ++_index % uis.Count;
		}

		//sounds


		public AudioSource audioMusic;
		public AudioSource audioSound;

		public static void RestartAllSound() {
			var audioMusic = Fengkong.instance.audioMusic;
			if (audioMusic) {
				audioMusic.gameObject.SetActive(true);
			}
			var audioSound = Fengkong.instance.audioSound;
			if (audioSound) {
				audioSound.gameObject.SetActive(true);
			}
		}

		public static void StopAllSound() {
			var audioMusic = Fengkong.instance.audioMusic;
			if (audioMusic) {
				audioMusic.gameObject.SetActive(false);
			}
			var audioSound = Fengkong.instance.audioSound;
			if (audioSound) {
				audioSound.gameObject.SetActive(false);
			}
		}

		public static AudioClip GetClip(string name) {
			return Resources.Load<AudioClip>("Sounds/" + name);
		}

		public static float PlaySound(string name) {
			var clip = GetClip(name);
			return PlaySound(clip);
		}

		public static float PlaySound(AudioClip clip) {
			var audioSound = Fengkong.instance.audioSound;
			if (clip && audioSound) {
				audioSound.Stop();
				audioSound.PlayOneShot(clip);
				return clip.length;
			}
			return 0f;
		}

		public static void StopSound() {
			var audioSound = Fengkong.instance.audioSound;
			if (audioSound) {
				audioSound.Stop();
			}
		}
	}


}
