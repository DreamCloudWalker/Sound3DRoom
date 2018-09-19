using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;
using System.Runtime.InteropServices;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Log: HelloARController Start");
		// asynCheckOmsSupported();
		Thread thread = new Thread(asynCheckOmsSupported);
        thread.Start();
	}

	private void asynCheckOmsSupported() {
        // AndroidJNI.AttachCurrentThread();
        Debug.Log("Log: HelloARController Start before checkOms");

        IntPtr javaVMHandle = IntPtr.Zero;
        // IntPtr activityHandle = IntPtr.Zero;
        NativeApi.ArCoreUnity_getJniInfo(ref javaVMHandle);

        int ret = checkOmsSupported("com.google.ar.core.examples.unity.helloar", "123456789", javaVMHandle);
        Debug.Log("Log: HelloARController checkOmsSupported ret = " + ret);
    }

	private int checkOmsSupported(string packageName, string appId, IntPtr jvm) 
	{
		byte[] tempstr = new byte[packageName.Length+1];
		tempstr[packageName.Length] = 0;
		byte[] tempstr1 = System.Text.Encoding.Default.GetBytes(packageName);
		Array.Copy(tempstr1, tempstr, packageName.Length);

		byte[] tmpStr2 = new byte[appId.Length + 1];
		tmpStr2[appId.Length] = 0;
		byte[] tmpStr3 = System.Text.Encoding.Default.GetBytes(appId);
		Array.Copy(tmpStr3, tmpStr2, appId.Length);

		return NativeApi.ArCoreUnity_checkOmsSupported(tempstr, tmpStr2, jvm);
	}

	private struct NativeApi {
		[DllImport("arworks")]
        public static extern void ArCoreUnity_getJniInfo(ref IntPtr jvm);

		[DllImport("arworks")]
		public static extern int ArCoreUnity_checkOmsSupported(byte[] packageName, byte[] appId, IntPtr jvm);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
