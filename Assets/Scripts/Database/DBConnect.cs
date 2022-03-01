/* Amanda Patricia Viray
 * January 2022
 * 
 * DBConnect is a class that contains a public connection string and 
 * database details to connect to the Microsoft SQL Server Database. 
 * 
 * If there is red underline under System.Data.SqlClient,
 * go to NuGet first for the SQL libraries by
 * Right-clicking the project > Manage NuGet Packages for solution... >
 * then download System.Data.SqlClient.
 * 
 * This script is suseptible to change because the project aims to authenticate
 * through the Web API instead straight from the database. This is done in order
 * to have a demo prepare in a short amount of time. 
 * 
 */

using System.Data.SqlClient;
using UnityEngine;
 
public class DBConnect : MonoBehaviour
{
    // You can edit the database details on code or through the Inspector Panel in the Editor
    // Make sure to enable TCP/IP addresses in 'Computer Management' (Search in Windows) by going through
    // Services and Applications > SQL Server Configuration > Protocol for MSSQLSERVER > TCP/IP then Enable all IP addresses
    [Header("Database String for Connection")]

    [Tooltip("Type here the TCP/IP + Port of the database.")]
    public  string dataSource = "127.0.0.1,1433";

    [Tooltip("Type here the database name.")]
    public string DBName = "VirtualPatient_DB";

    [Tooltip("Type here the Username/User ID.")]
    public string UserID = "VPAdmin";

    [Tooltip("Type here the password.")]
    public string password = "123";

    [HideInInspector]
    public string cs; //Connection string 

    void Start()
    {
        cs = $@"Data Source ={dataSource};
            Initial Catalog ={DBName};
            User ID={UserID};
            Password={password};";

        SqlConnection dbConnection = new SqlConnection(cs);

        try
        {
            dbConnection.Open();
            Debug.Log("Connected to database.");
        }
        catch (SqlException _exception)
        {
            Debug.LogWarning(_exception.ToString());

        }
    }
}

/* References
 * Connect to MS SQL Database: https://forum.unity.com/threads/connect-to-ms-sql-database.484855/
 */