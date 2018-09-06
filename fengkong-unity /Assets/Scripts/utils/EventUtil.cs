using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace wuyy {

	public static class EventUtil {

		public static void AddTriggerListener(Transform trans, EventTriggerType eventID, UnityAction<BaseEventData> action) {
			AddTriggerListener(trans.gameObject, eventID, action);
		}

		public static void AddTriggerListener(GameObject obj, EventTriggerType eventID, UnityAction<BaseEventData> action) {
			EventTrigger trigger = obj.GetComponent<EventTrigger>();
			if (trigger == null) {
				trigger = obj.AddComponent<EventTrigger>();
			}
			AddTriggerListener(trigger, eventID, action);
		}

		public static void AddTriggerListener(EventTrigger trigger, EventTriggerType eventID, UnityAction<BaseEventData> action) {
			if (trigger.triggers.Count == 0) {
				trigger.triggers = new List<EventTrigger.Entry>();
			}
			UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(action);
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = eventID;
			entry.callback.AddListener(callback);
			trigger.triggers.Add(entry);
		}

		public static void SetAlpha(this Image image, float alpha) {
			var color = image.color;
			color.a = alpha;
			image.color = color;
		}

		public static void SetAlpha(this Text text, float alpha) {
			var color = text.color;
			color.a = alpha;
			text.color = color;
		}
	}


}
