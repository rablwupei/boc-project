using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace wuyy {

	public class UIShouye : MonoBehaviour {

		public Text day;
		public Text time;

		public void ButtonClick(int type) {
			gameObject.SetActive(false);
			Baibian.instance.Change(type, true);
		}

		float next;

		void Update() {
			if (Time.realtimeSinceStartup > next) {
				next = Time.realtimeSinceStartup + 1;
				var now = DateTime.Now;
				day.text = now.ToString("yyyy-MM-dd");
				time.text = now.Hour <= 12 ? "上午" : "下午" + now.ToString("hh:mm");
			}

		}

	}

}
