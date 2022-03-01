/* Amanda Patricia Viray
 * January 2022
 * 
 * LoginForm is a class that contains a function for logging into VP. 
 * 
 * This script is suseptible to change because the project aims to authenticate
 * through the Web API instead straight from the database. This is done in order
 * to have a demo prepare in a short amount of time. 
 * 
 * Functions:
 * - FromSql ()
 * 
 */

using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;

public class LoginForm : MonoBehaviour
{
    [Header("Login Input Fields")]
    public InputField Username;
    public InputField Password;

    [Header("Login Status Text")]
    public Text LoginStatus;

    private  UIManager UI;
    public static string StudentName, StudentUsername, AdminName, AdminUsername;

    public void FromSql()
    {
        string cs = GetComponent<DBConnect>().cs;
        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>(); //Unity component to get UI Manager gameobject

        SqlConnection SqlConn = new SqlConnection(cs);
        SqlConn.Open();

        SqlCommand login = new SqlCommand
            ("SELECT FirstName,Username,Password,Admin from Login where Username='" + Username.text + "'and Password='" + Password.text + "'", SqlConn);
        
        using (SqlDataReader reader = login.ExecuteReader()) //System.Data.CommandBehavior.CloseConnection
        {
            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    //Debug.Log(reader["Username"].ToString());
                    //Debug.Log(reader["Password"].ToString());
                    //Debug.Log(reader["Admin"].ToString());
                    //Debug.Log(reader["FirstName"].ToString());

                    if (reader["Admin"].ToString().Contains("True"))
                    {
                        UI.AdminLogin();
                        AdminName = reader["FirstName"].ToString();
                        AdminUsername = reader["Username"].ToString();
                        LoginStatus.text = ("");
                    } else
                    {
                        UI.StudentLogin();

                        StudentName = reader["FirstName"].ToString();
                        StudentUsername = reader["Username"].ToString();
                        LoginStatus.text = ("");
                    }
                }
            } else
            {
                LoginStatus.text = ("Invalid Login, please check username and password");
            }
            
            SqlConn.Close();
        }
    }
}

/* References
 * (reader != null && reader.HasRows) -> https://www.codeproject.com/Questions/520074/InvalidplusattemptplustopluscallplusMetaDatapluswh
 * using (SqlDataReader reader = login.ExecuteReader()) -> https://stackoverflow.com/questions/20469899/c-sharp-invalid-attempt-to-call-read-when-reader-is-closed
 *
 * Notes
 * - used SqlDataReader rather than SqlDataAdapter (which is used by most login form SQL C# tutorials)
 * because its easier to know if the user is Admin or not unlike DataAdapter wherein we have to create 
 * a table and read through  * each one including assigning the table with an ID column. 
 */
