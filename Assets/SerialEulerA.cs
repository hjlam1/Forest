﻿using System;
using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class SerialEulerA : MonoBehaviour {

	SerialPort stream = new SerialPort("COM3", 115200);
	//SerialPort stream = new SerialPort("\\\\.\\COM18", 115200, Parity.None, 8, StopBits.One);  // My Bluetooth COM port
	public Quaternion direction;


	
	void Start () {
		string[] portList= SerialPort.GetPortNames();
		for (int i = 0; i < portList.Length; i++) {
			Debug.Log (portList[i]);
		}
	}
	
	void Update () {
		try{
			stream.Open();
			string serialInput = stream.ReadLine();
						
			string[] strEul= serialInput.Split (',');
			if (strEul.Length > 5) {
				direction = Quaternion.Euler (new Vector3( -float.Parse(strEul[5]), float.Parse (strEul[3]), float.Parse(strEul[4])));
				this.transform.rotation = direction;
				if (int.Parse(strEul[2]) == 1) {
					this.GetComponent<Rigidbody>().AddForce(this.transform.forward * 30f);

				}
				if (int.Parse(strEul[1]) == 1) {
					this.GetComponent<Rigidbody>().AddForce(this.transform.forward * -30f);
					
				}
			}

			stream.Close ();
		}
		catch(Exception e){
			Debug.Log("Could not open serial port: " + e.Message);
			
		}

	}

	IEnumerator SerialOperation() {

		while(true) {

			yield return null;
				
		}
	}
}

