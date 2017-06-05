using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : Score {

	public void SaveScore () {
		PlayerPrefs.SetFloat ("Highscore", scorez);
	}

	public void LoadScore () {
		PlayerPrefs.GetFloat ("Highscore");
	}
}
