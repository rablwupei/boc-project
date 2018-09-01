using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace wuyy {

	public class UIDuihuakuang : MonoBehaviour {

		public Text text;

		public void BgClick() {
			gameObject.SetActive(false);
		}

		public void Show(MenuItemType type) {
			gameObject.SetActive(true);
		}

	}


}
