using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DragonHP : MonoBehaviour {

	
	public GameObject redDragonPiece1, redDragonPiece2, redDragonPiece3, redDragonPiece4, redDragonPiece5, redDragonPiece6, redDragonPiece7, redDragonPiece8,redDragonPiece9,redDragonPiece10,redDragonPiece11,redDragonPiece12,redDragonPiece13,redDragonPiece14,redDragonPiece15,redDragonPiece16,redDragonPiece17,redDragonPiece18,redDragonPiece19,redDragonPiece20;
	public GameObject whiteDragonPiece1, whiteDragonPiece2, whiteDragonPiece3, whiteDragonPiece4, whiteDragonPiece5, whiteDragonPiece6, whiteDragonPiece7, whiteDragonPiece8,whiteDragonPiece9,whiteDragonPiece10,whiteDragonPiece11,whiteDragonPiece12,whiteDragonPiece13,whiteDragonPiece14,whiteDragonPiece15,whiteDragonPiece16,whiteDragonPiece17,whiteDragonPiece18,whiteDragonPiece19,whiteDragonPiece20;
	int health;
	// Use this for initialization
	void Start () 
	{
		health = 100;
		whiteDragonPiece1.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece2.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece3.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece4.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece5.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece6.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece7.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece8.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece9.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece10.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece11.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece12.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece13.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece14.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece15.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece16.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece17.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece18.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece19.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
		whiteDragonPiece20.GetComponent<SpriteRenderer>().DOFade(0f, 0.1f);
	}

	// Update is called once per frame
	void Update () 
	{
		Debug.Log (health);
		if (health <100 && redDragonPiece1.activeInHierarchy) {
			whiteDragonPiece1.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece1.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 95 && redDragonPiece2.activeInHierarchy) {
			whiteDragonPiece2.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece2.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 90 && redDragonPiece3.activeInHierarchy) {
			whiteDragonPiece3.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece3.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 85 && redDragonPiece4.activeInHierarchy) {
			whiteDragonPiece4.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece4.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 80 && redDragonPiece5.activeInHierarchy) {
			whiteDragonPiece5.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece5.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 75 && redDragonPiece6.activeInHierarchy) {
			whiteDragonPiece6.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece6.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 70 && redDragonPiece7.activeInHierarchy) {
			whiteDragonPiece7.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece7.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 65 && redDragonPiece8.activeInHierarchy) {
			whiteDragonPiece8.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece8.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 60 && redDragonPiece9.activeInHierarchy) {
			whiteDragonPiece9.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece9.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 55 && redDragonPiece10.activeInHierarchy) {
			whiteDragonPiece10.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece10.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		}if (health < 50 && redDragonPiece11.activeInHierarchy) {
			whiteDragonPiece11.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece11.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 45 && redDragonPiece12.activeInHierarchy) {
			whiteDragonPiece12.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece12.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 40 && redDragonPiece13.activeInHierarchy) {
			whiteDragonPiece13.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece13.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 35 && redDragonPiece14.activeInHierarchy) {
			whiteDragonPiece14.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece14.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 30 && redDragonPiece15.activeInHierarchy) {
			whiteDragonPiece15.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece15.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 25 && redDragonPiece16.activeInHierarchy) {
			whiteDragonPiece16.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece16.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 20 && redDragonPiece17.activeInHierarchy) {
			whiteDragonPiece17.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece17.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 15 && redDragonPiece18.activeInHierarchy) {
			whiteDragonPiece18.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece18.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 10 && redDragonPiece19.activeInHierarchy) {
			whiteDragonPiece19.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece19.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
		} if (health < 5 && redDragonPiece20.activeInHierarchy) {
			whiteDragonPiece20.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
			redDragonPiece20.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
			//game Over
		} 
	}

	public void UpdateHealth (int value) {
		health = value;

	}
}
