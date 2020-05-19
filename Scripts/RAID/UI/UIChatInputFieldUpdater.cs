using UnityEngine;
using UnityEngine.UI;

public class UIChatInputFieldUpdater : MonoBehaviour
{
    ChatRule ChatRuleCache;
    InputField InputFieldCache;

    void Start()
    {
        InputFieldCache = GetComponent<InputField>();
        ChatRuleCache = GameObject.Find("Chat Rules").GetComponent<ChatRule>();
        if (Utils.IsValid(ChatRuleCache) && Utils.IsValid(InputFieldCache))
        {
            InputFieldCache.onEndEdit.AddListener(ChatRuleCache.SendChatMessage);
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    InputFieldCache.end
        //}
    }
}
