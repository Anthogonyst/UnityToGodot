using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLED : MonoBehaviour {

	public int offset = 50;
	public SpriteRenderer led;
	public float waitTimeTillOff = 4f;
	public List<Sprite> ledScreens;
	private bool safety = true;
	private bool screenIsOn = false;
	
	void Start() {
		if (led == null || ledScreens.Count != 13) {
			Debug.Log("Incorrectly instantiated LED screen. " + transform.name);
			safety = false;
		}
	}
	
	public bool displayPianoKey(int n) {
		if (safety) {
			n = (n + offset) % 12;
			led.sprite = ledScreens[n];
			screenIsOn = true;
			
			CancelInvoke();
			Invoke("turnOffLED", waitTimeTillOff);
			return true;
		}
		return false;
	}
	
	public bool turnOffLED() {
		if (safety) {
			led.sprite = ledScreens[12];
			screenIsOn = false;
			return true;
		}
		return false;
	}
	
	public bool isOn() {
		return screenIsOn;
	}
}
