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

	}


}
