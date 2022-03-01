/* Amanda Patricia Viray
 * January 2022
 * 
 * RegisterForm is a class that contains functions for registering into VP. 
 * 
 * Functions:
 * - ToSql ()
 * 
 */

using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;

public class RegisterForm : MonoBehaviour
{
    [Header("Register Input Fields")]
    public InputField FirstName;
    public InputField LastName;
    public InputField Username;
    public InputField Email;
    public InputField Password;
    public InputField ConfirmPassword;

    [Header("Register Status Text")]
    public  Text registerStatus;

    private  UIManager UI;

    public void ToSql()
    {
        string cs = GetComponent<DBConnect>().cs; 
        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();


        SqlConnection SqlConn = new SqlConnection(cs);

        if (FirstName.text == "" && LastName.text == "" && Username.text == "" && Email.text == "")
        {
            registerStatus.text = ("Fields cannot be blank.");

        } else if  (Password.text == ConfirmPassword.text && Password.text != "" && ConfirmPassword.text != "")
        {
            registerStatus.text = ("");
            SqlConn.Open();
            SqlCommand cmd = new SqlCommand("INSERT Login values ('" + System.Guid.NewGuid().ToString() + "','" + FirstName.text + "', '" + LastName.text + "', '" + Username.text + "', '" + Email.text + "', '" + ConfirmPassword.text + "', '" + 0 + "')", SqlConn);
            cmd.ExecuteNonQuery();
            SqlConn.Close();

            UI.ConfirmRegister();

        } else
        {
            registerStatus.text = ("Passwords do not match or cannot be empty.");
            
        }
    }
}


