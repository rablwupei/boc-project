using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace wuyy {

	[RequireComponent(typeof(Slider))]
	public class AudioVolume : MonoBehaviour {

		Slider _slider;
		Slider slider {
			get {
				if (!_slider) {
					_slider = GetComponent<Slider>();
				}
				return _slider;
			}
		}

		void OnEnable() {
			slider.value = AudioListener.volume;
		}

		public void ValueChange(float value) {
			AudioListener.volume = value;
			PlayerPrefs.SetFloat(Baibian.Key_volume, value);
		}

	}


}
