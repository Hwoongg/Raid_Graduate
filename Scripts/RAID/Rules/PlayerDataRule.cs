using UnityEngine;

public class PlayerDataRule : MonoBehaviour
{
    [SerializeField] string NickName;
    public string nickName { get { return NickName; } set { NickName = value; } }
};
