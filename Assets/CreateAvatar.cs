using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using static UnityEditor.Progress;

public class CreateAvatar : MonoBehaviour
{
    public TMPro.TMP_InputField nameInput;
    public TMPro.TMP_Dropdown avatarClassDropdown;
    public Slider experienceSlider;
    public TMPro.TMP_Dropdown itemDropdown;
    public Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        submitButton.onClick.AddListener(Submit);
    }

    // Update is called once per frame
    void Submit()
    {
        StartCoroutine(Upload());

        //connect to server and send data over
    }

    IEnumerator Upload()
    {
        string name = nameInput.text;
        string avatarClass = avatarClassDropdown.options[avatarClassDropdown.value].text;
        float experience = experienceSlider.value;
        string item = itemDropdown.options[itemDropdown.value].text;

        Debug.Log(name + " " + avatarClass + " " + experience + "  " + item);

        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("avatarClass", avatarClass);
        form.AddField("experience", (int)experience);
        form.AddField("item", item);

        using UnityWebRequest www = UnityWebRequest.Post("http://192.168.56.102/insert.php", form);

        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
