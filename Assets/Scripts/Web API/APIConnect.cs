/* Amanda Patricia Viray
 * January 2022
 * 
 * APICOnnect is a class that connects the Web API with Unity. 
 * 
 * Functions:
 * - OnClickSubmit()
 * 
 */


using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
//using Newtonsoft.Json;
//using Crosstales.RTVoice.Tool;
//using UnityEngine.UI;
//using Crosstales.RTVoice;
//using TMPro;

public class APIConnect : MonoBehaviour
{
    public string json = "";
    public Text Idtext;
    public InputField inputFieldText;
    public string question;

    private static APIConnect _instance;
    public static APIConnect Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void OnClickSubmit()
    {
        StartCoroutine(getResponse());
    }

    IEnumerator getResponse()
    {
        Debug.Log("Response called");
        Idtext.text = "";

        //string text = String.IsNullOrEmpty(Idtext.text) ? "1" : Idtext.text;
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:63249/api/client?Question=" + inputFieldText.text );
        Debug.Log(www.url);
     

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error Occured");    
        }
        else
        {
            json = www.downloadHandler.text;
            question = inputFieldText.text; //this is student's input
            Idtext.text = json;
            //dial = JsonConvert.DeserializeObject<Dialogue>(json);
            //Debug.Log(dial);

            //if (!String.IsNullOrEmpty(dial.response))
            //{
            //    //speech.CurrentText = dial.response;
            //    //inputFieldText.text = dial.response;
            //    //speech.Speak();
            //    Idtext.text = dial.response;
            //    //FindObjectOfType<ExpressionHandler>().LoadEmotions(dial.response);
            //}
        }
    }

    public IEnumerator postUnityWebRequest(string json)
    {
        ///<summary>
        /// Post using UnityWebRequest class
        /// </summary>
        var jsonString = json; //generated json
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonString.ToCharArray());

        UnityWebRequest unityWebRequest = new UnityWebRequest("https://localhost:44310/Views/Conversation_Input_Output", "POST");
        unityWebRequest.uploadHandler = new UploadHandlerRaw(byteData);
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");
        yield return unityWebRequest.SendWebRequest();

        if (UnityWebRequest.Result.ConnectionError == UnityWebRequest.Result.ProtocolError)
        {
            //Debug.Log(unityWebRequest.error);
        }
        else
        {
            Debug.Log("Form upload complete! Status Code: " + unityWebRequest.responseCode);
        }
    }


}

public class Dialogue
{
    //public int id { get; set; }
    public string question { get; set; }
    public string response { get; set; }
}

public class PatientSetting
{
    /// <summary>
    /// id = 1 --> David
    /// id = 2 --> Zira
    /// id = 3 --> Hazzel
    /// </summary>
    public int id { get; set; }
    public string modelName { get; set; }
    public string voiceName { get; set; }
}