﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Analysis disable once CheckNamespace
public class DisplayScore : MonoBehaviour {

	public Text propText, suspText, stamText, daysText, popUp;
	public bool win;
	public string winMsg;
	private bool UIshown = true; 
	public bool usedSpam = false, mkPropaganda;
	private float usedStam, usedSusp, timeProp, timeSpam;
	public bool work = false;
	public GameObject player;
	public GameObject camScene;
	public int stamina, propaganda, suspicius, days;
	private float timeUI = 0;
	private float timeGame;

	void Start()
	{
		stamina = 100;
		days = 10;
	}
		
	// Update is called once per frame
	void Update () {

		/*
		Dear person who's watching my code, please belive me if i'm saying that i'm shamefull from this
		ugly spaguetti code, this is no me, this is the jam mode alone, and I shouldn't have done that
		but, it's kinda late now...
		*/

		//day cycle


		timeGame += Time.deltaTime;
		if (timeGame > 40) {
			timeGame = 0;
			days -= 1;
			daysText.text = days + " days Left";
			popUp.text = days + " days Left";
			usedSpam = true;
		}


		//check to remove stamina

		usedStam += Time.deltaTime;

		if (usedStam > 3) {
			usedStam = 0;
			stamina -= 1;
			stamText.text = stamina + "%";
			if (stamina == 40 || stamina == 20 || stamina == 10) {
				usedSpam = true;
				popUp.text = "low stamina " + stamina + "%"; 
			}
		}


		//check to add suspicious if not working

		if (!work && !mkPropaganda && timeProp > 8) {
			timeProp = 0;
			suspicius += 1;
			suspText.text = suspicius + "%";
		} 
		else if (mkPropaganda && timeProp > 3) {
			timeProp = 0;
			suspicius += 5;
			suspText.text = suspicius + "%";
			propaganda += 1;
			propText.text = propaganda + "%";
			popUp.text = propaganda + "% of propaganda";
			usedSpam = true;
			if (suspicius > 50) {
				usedSpam = true;
				popUp.text = "Highly suspicious " + suspicius + "%";

				//add a condition to kill him faster if he's too suspicious


				if(Random.Range(0, Mathf.Round(100/suspicius)) == 0)
				{
					suspicius += 10;
					suspText.text = suspicius + "%";
				}

			} 
		}
		else if (work && timeProp > 1 && suspicius > 4) {
			timeProp = 0;
			suspicius -= 5;
			suspText.text = suspicius + "%";
			usedSpam = true;
			popUp.text = suspicius + "% suspicious";
		}
		timeProp += Time.deltaTime;

		//check if spam need reset

		if (usedSpam) {
			if (timeSpam > 3) {
				popUp.text = "";
				usedSpam = false;
				timeSpam = 0;
			}
			timeSpam += Time.deltaTime;
		}


		//check for game ending

		if (stamina < 0 && !win) {
			win = true;
			winMsg = "You died because of low stamina";
		} else if (suspicius > 99 && !win) {
			win = true;
			winMsg = "You died because you were too suspicious";
		} 
		else if (propaganda > 99 && !win) {
			win = true;
			winMsg = "You won, you raised population against B.B.";
		}


		//button to show UI display
		
		if (Input.GetAxis ("Fire2") != 0 && UIshown && timeUI > 2) {
			UIshown = false;
			timeUI = 0;
			camScene.GetComponent<StickyCamera> ().playerFollowed = this.gameObject;
		} 

		else if (Input.GetAxis ("Fire2") != 0 && timeUI > 2) {
			camScene.GetComponent<StickyCamera> ().playerFollowed = player;
			timeUI = 0;
			UIshown = true;
		}

		if(timeUI <= 2)
		timeUI += Time.deltaTime;
		
	}
}
