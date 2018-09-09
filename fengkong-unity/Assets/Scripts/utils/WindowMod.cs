﻿using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UnityEngine;
using System.Xml.Serialization;

public class WindowMod : MonoBehaviour
{
	public Rect screenPosition;

	#if UNITY_STANDALONE && !UNITY_EDITOR
	[DllImport("user32.dll")]
	static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
	[DllImport("user32.dll")]
	static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
	[DllImport("user32.dll")]
	static extern IntPtr GetActiveWindow();
	const uint SWP_SHOWWINDOW = 0x0040;
	const int GWL_STYLE = -16;
	const int WS_BORDER = 1;
	private int i = 0;

	void Start()
	{
		SetWindowLong(GetActiveWindow(), GWL_STYLE, WS_BORDER);
		SetWindowPos(GetActiveWindow(), -1, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
	}

	void Update()
	{
		i++;
		if(i<5)
		{
			SetWindowLong(GetActiveWindow(), GWL_STYLE, WS_BORDER);
			SetWindowPos(GetActiveWindow(), -1, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
		}
	}
	#endif
}