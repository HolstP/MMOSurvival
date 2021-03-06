using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour {

	public GameObject playerCamera;
	public GameObject playerUI;
	public GameObject nameTag;
	public MonoBehaviour[] playerControlScripts;

	public Transform nameTagHolder;

	[HideInInspector]
	public PhotonView photonView;

	// Use this for initialization
	void Start () {
		photonView = GetComponent<PhotonView>();
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		//nameTagHolder.rotation = Quaternion.identity;
	}

	void Awake() {
		nameTagHolder = transform.Find ("NameTagHolder");
	}

	private void Initialize() {
		if (photonView.isMine) {
			Instantiate (playerUI);
			nameTag.SetActive (false);
			foreach (MonoBehaviour m in playerControlScripts) {
				m.enabled = true;
			}
		} else {
			playerCamera.SetActive (false);
			playerUI.SetActive (false);
		}

		//nameTagHolder.Find("NameTag").GetComponent<TextMesh> ().text = this.transform.name;
	}
}
