using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(InputField))]
public class UIUsername : MonoBehaviour
{
    readonly string PlayerNamePrefKey = "PlayerName";

    void Start()
    {
        //var defaultName = string.Empty;
        //var inputField = GetComponent<InputField>();
        //if (Utils.IsValid(inputField))
        //{
        //    if (PlayerPrefs.HasKey(PlayerNamePrefKey))
        //    {
        //        defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
        //        inputField.text = defaultName;
        //    }
        //}
        //PhotonNetwork.NickName = defaultName;
    }

    /// <summary>
    /// Sets the name of the player, and save it in the PlayerPrefs for future session.
    /// </summary>
    /// <param name="name"></param>
    public void OnSetPlayerName(InputField inputField)
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            Dbg.LogE("Player Name is empty.");
            return;
        }
        Dbg.Log($"{inputField.text}");
        PhotonNetwork.NickName = inputField.text;

        PlayerPrefs.SetString(PlayerNamePrefKey, inputField.text);
        PlayerPrefs.Save();
    }

    bool CheckIsExisting(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return true;
        }
        return false;
    }
};
