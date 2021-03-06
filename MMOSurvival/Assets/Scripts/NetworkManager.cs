using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	public GameObject player;
    public string posX;
    public string posY;
    public string posZ;
	public Vector3 spawnPoint;

    //public PlayerLevel playerlevel;

    public string myCharacterID;
    public string[] characterid;

    public string GetCharacterIDURL = "http://www.varygames.com/spacemmo/getcharacterid.php";

    public string[] characterpos;

    // Use this for initialization
    void Start () {

        StartCoroutine(GetCharacterID());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator GetCharacterID()
    {
        Debug.Log("Getting CharacterID From " + PhotonNetwork.player.NickName);
        WWWForm form = new WWWForm();
        form.AddField("charactername", PhotonNetwork.player.NickName);
        WWW characterID = new WWW(GetCharacterIDURL, form);
        yield return characterID;

        string userIDString = characterID.text;
        characterid = userIDString.Split(';');
        myCharacterID = (GetCharacterID(characterid[0], "Character_ID"));
        Debug.Log("CharacterID fetched CharacterID is: " + myCharacterID);

        StartCoroutine(GetCharacterPos());
    }

    string GetCharacterID(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    private IEnumerator GetCharacterPos()
    {
        WWWForm form = new WWWForm();
        form.AddField("character_id", myCharacterID);
        WWW characterPos = new WWW("http://www.varygames.com/spacemmo/getcharacterpos.php", form);
        yield return characterPos;
        if (characterPos.text == "No character found.")
        {
            Debug.Log("Fuck");
        } else
        {
            string characterPosString = characterPos.text;
            characterpos = characterPosString.Split(';');
            posX = (GetCharacterPos(characterpos[0], "posX "));
            posY = (GetCharacterPos(characterpos[0], "posY "));
            posZ = (GetCharacterPos(characterpos[0], "posZ "));
            Debug.Log("Users pos succesfully retrived.");

            float x = float.Parse(posX);
            float y = float.Parse(posY);
            float z = float.Parse(posZ);
            spawnPoint = new Vector3(x, y, z);

            PhotonNetwork.Instantiate(player.name, spawnPoint, Quaternion.Euler(0, 0, 0), 0);

            yield return new WaitForSeconds(1);

            //playerlevel = GameObject.Find(PhotonNetwork.player.NickName).GetComponent<PlayerLevel>();

            //playerlevel.level = Convert.ToInt32((GetCharacterPos(characterpos[0], "level ")));
            //playerlevel.currentExperience = Convert.ToInt32((GetCharacterPos(characterpos[0], "xp ")));
        }
    }

    string GetCharacterPos(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }
}
