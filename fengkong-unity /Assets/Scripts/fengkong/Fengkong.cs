using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			Next();
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
