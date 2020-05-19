using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(InputField))]
public class UIPassword : MonoBehaviour
{
    readonly string PlayerPasswordPrefKey = "PlayerPassword";

    void Start()
    {
        //var defaultPw = string.Empty;
        //var inputField = GetComponent<InputField>();
        //if (Utils.IsValid(inputField))
        //{
        //    if (PlayerPrefs.HasKey(PlayerPasswordPrefKey))
        //    {
        //        defaultPw = PlayerPrefs.GetString(PlayerPasswordPrefKey);
        //        inputField.text = defaultPw;
        //    }
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inputField"></param>
    public void OnSetPlayerPassword(InputField inputField)
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            Dbg.LogE("Player Password is null or empty.");
            return;
        }
        Dbg.Log($"{inputField.text}");
        PlayerPrefs.SetString(PlayerPasswordPrefKey, inputField.text);
        PlayerPrefs.Save();
    }
};
