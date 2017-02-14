using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// Dialogue script based on TutorialDirector from Bit.Spike
/// </summary>
public class DialogueManager : MonoBehaviour {

	public static DialogueManager instance;

	public TextAsset introduction;
	public TextAsset keyboardIntroduction;

	public RawImage moveBit;

	public RawImage scrollMapKB;
	public RawImage firstPersonKB;
	public RawImage moveBitKB;

	public RawImage targets;
	public RawImage levelEnd;

	public Text spikeLabel;

	private float nextText;

	private string targetText;

	private StringReader lineReader;

	public Text instructions;

	private string current;

	void clearText() {
		instructions.text = "";
	}

	public void advanceScroll() {
		if (targetText.Equals ("<ScrollMap>")) {
			nextText = Time.time;
			nextLine ();
		}
	}

	public void advancePointer() {
		if (targetText.Equals ("<MoveBit>")) {
			nextText = Time.time;
			nextLine ();
		}
	}

	public void advanceSwitch() {
		if (targetText.Equals ("<FirstPerson>")) {
			nextText = Time.time;
			nextLine ();
		}
	}

	public void advanceTargets() {
		if (targetText.Equals ("<Targets>")) {
			nextText = Time.time;
			nextLine ();
		}
	}

	void nextLine() {
		current = lineReader.ReadLine ();
		if (current != null) {
			clearText ();
			if (current.Equals("<ScrollMap>")) {
				instructions.enabled = false;
				spikeLabel.enabled = false;
				
			} else if (current.Equals("<MoveBit>")) {
				instructions.enabled = false;
				spikeLabel.enabled = false;
				
			} else if (current.Equals("<FirstPerson>")) {
				instructions.enabled = false;
				spikeLabel.enabled = false;
				
			} else if (current.Equals("<Targets>")) {
				instructions.enabled = false;
				spikeLabel.enabled = false;
				targets.enabled = true;
			} else if (current.Equals("<LevelEnd>")) {
				instructions.enabled = false;
				spikeLabel.enabled = false;
				levelEnd.enabled = true;
			} else {
				instructions.enabled = true;
				spikeLabel.enabled = true;
			}
			targetText = current;
		}
	}

	// Use this for initialization
	void Start () {
		instance = this;
		nextText = Time.time;
		lineReader = new StringReader (keyboardIntroduction.text);
		//	lineReader = new StringReader (introduction.text);
		current = lineReader.ReadLine ();
		clearText ();
		targetText = current;
	}
	
	// Update is called once per frame
	void Update () {
		//if (Director.isGameMode ()) {
		//	nextText = Time.time + 0.05f;
		//} else {
			if (instructions.enabled && current != null) {
				if (Time.time > nextText) {
					if (targetText != null) {
						if (!instructions.text.Equals (targetText)) {
							instructions.text = targetText.Substring (0, instructions.text.Length + 1);
							nextText = nextText + 0.05f;
						} else {
							if (current.Equals ("")) {
								nextLine ();
								nextText = Time.time;
							} else {
								current = "";
								nextText = nextText + 2f;
							}
						}
					}

				}
			}
		//}
	}
}
