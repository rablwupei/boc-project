using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wuyy {

	public class UIGuocheng : MonoBehaviour {

		public GameObject uiOptionRoot;
		public RectTransform uiOptionPanel;

		public void OpenMenu(Vector3 pos) {
			uiOptionRoot.SetActive(true);
			uiOptionPanel.anchoredPosition = pos;
		}

		public void ButtonClick(int type) {
			Baibian.instance.Change(type);
		}

	}

}
