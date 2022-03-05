/* Amanda Patricia Viray
 * January 2022
 * 
 * UIController is a class that controls the action of the buttons in the project. 
 * 
 * Functions:
 * - Click (Button, Action)
 * - Switch (string Scene)
 * - (optional) NextLevel()
 * 
 */

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class UIController : MonoBehaviour
{
    UIManager UI;
    LoginForm loginForm;
    RegisterForm registerForm;
    public static UIController instance;
    [HideInInspector]
    [SerializeField] Button 
        // Main Menu Scene
        loginButton, registerButton, register_BackButton, register_ConfirmButton, registered_backButton, 
        
        //Student Menu Scene
        student_settingsButton, closeSettingsButton, logOutButton, StartButton, BackLevelsButton,
        //LevelsButton,

        // Virtual Patient Scene
        sayButton, notesButton, closeNotesButton, helpButton, closeHelpButton, finishButton, returnButton, 
        
        // Feedback Assessment Scene
        NextLevelButton;
    enum UIType { MainMenu, StudentMenu, AdminMenu, VirtualPatient }
    [Header("UI View Controller")]
    [SerializeField] UIType UIView;

    bool showMainMenu = false;
    bool showVirtualPatient = false;
    bool showStudentMenu = false;
    bool showAdminMenu = false;

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(UIController))]
    public class UIControllerEditor : Editor
    {
        private UIController uiC;
        void Awake()
        {
            uiC = (UIController)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
           
            //Serialize

            if (uiC.UIView == UIType.MainMenu)
            {
                uiC.showMainMenu = EditorGUILayout.Foldout(uiC.showMainMenu, "Main Menu Buttons", true);
                
                if (uiC.showMainMenu)
                {

                    uiC.loginButton = (Button)EditorGUILayout.ObjectField("Login Button: ", uiC.loginButton, typeof(Button), true);
                    uiC.registerButton = (Button)EditorGUILayout.ObjectField("Register Screen Button: ", uiC.registerButton, typeof(Button), true);
                    uiC.register_BackButton = (Button)EditorGUILayout.ObjectField("Register - Cancel Button: ", uiC.register_BackButton, typeof(Button), true);
                    uiC.register_ConfirmButton = (Button)EditorGUILayout.ObjectField("Register - Confirm Button: ", uiC.register_ConfirmButton, typeof(Button), true);
                    uiC.registered_backButton = (Button)EditorGUILayout.ObjectField("Register - Return Home Button: ", uiC.registered_backButton, typeof(Button), true);
                }

            } // End Main Menu View

            if (uiC.UIView == UIType.StudentMenu)
            {
                uiC.showStudentMenu = EditorGUILayout.Foldout(uiC.showStudentMenu, "Student Menu Buttons", true);

                if (uiC.showStudentMenu)
                {
                    uiC.student_settingsButton = (Button)EditorGUILayout.ObjectField("Student - Settings Button: ", uiC.student_settingsButton, typeof(Button), true);
                    uiC.closeSettingsButton = (Button)EditorGUILayout.ObjectField("Student - Close Settings Button: ", uiC.closeSettingsButton, typeof(Button), true);
                    uiC.logOutButton = (Button)EditorGUILayout.ObjectField("Log Out Button: ", uiC.logOutButton, typeof(Button), true);
                    //uiC.LevelsButton = (Button)EditorGUILayout.ObjectField("Levels Button: ", uiC.LevelsButton, typeof(Button), true);
                    uiC.BackLevelsButton = (Button)EditorGUILayout.ObjectField("Levels - Back Button: ", uiC.BackLevelsButton, typeof(Button), true);
                }

            } // End Student Menu View


            if (uiC.UIView == UIType.AdminMenu)
            {
                uiC.showAdminMenu = EditorGUILayout.Foldout(uiC.showAdminMenu, "Admin Menu Buttons", true);

                if (uiC.showAdminMenu)
                {
                    EditorGUILayout.HelpBox("Admin Buttons here", MessageType.Info, true);
                }

            } // End Admin Menu View

            if (uiC.UIView == UIType.VirtualPatient)
            {
                uiC.showVirtualPatient = EditorGUILayout.Foldout(uiC.showVirtualPatient, "Virtual Patient Buttons", true);

                if (uiC.showVirtualPatient)
                {
                    uiC.sayButton = (Button)EditorGUILayout.ObjectField("Say Button: ", uiC.sayButton, typeof(Button), true);
                    uiC.notesButton = (Button)EditorGUILayout.ObjectField("Notes Button: ", uiC.notesButton, typeof(Button), true);
                    uiC.closeNotesButton = (Button)EditorGUILayout.ObjectField("Close Notes Button: ", uiC.closeNotesButton, typeof(Button), true);

                    uiC.helpButton = (Button)EditorGUILayout.ObjectField("Help Button: ", uiC.helpButton, typeof(Button), true);
                    uiC.closeHelpButton = (Button)EditorGUILayout.ObjectField("Close Help Button: ", uiC.closeHelpButton, typeof(Button), true);

                    uiC.finishButton = (Button)EditorGUILayout.ObjectField("Finish Button: ", uiC.finishButton, typeof(Button), true);
                    uiC.returnButton = (Button)EditorGUILayout.ObjectField("Return Button: ", uiC.returnButton, typeof(Button), true);
                    uiC.NextLevelButton = (Button)EditorGUILayout.ObjectField("Next Level Button: ", uiC.NextLevelButton, typeof(Button), true);
                }
            } // End Virtual Patient View

            if (GUI.changed)
                EditorUtility.SetDirty(uiC);
        }
    }
#endif
    #endregion

    public virtual void Click(Button button, Action method) => button.onClick.AddListener(() => method());
    public void Switch(string Scene) => SceneManager.LoadScene(Scene);

    [HideInInspector]
    public int nextSceneLoad;
    public void NextLevel() => SceneManager.LoadScene(nextSceneLoad);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    private void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        //nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.GetActiveScene().name == "Authentication") 
        {
            loginForm = GameObject.FindGameObjectWithTag("DBManager").GetComponent<LoginForm>();
            registerForm = GameObject.FindGameObjectWithTag("DBManager").GetComponent<RegisterForm>();

            Click(loginButton, loginForm.FromSql);
            Click(registerButton, UI.RegisterScreen);
            Click(register_BackButton, UI.LoginScreen);
            Click(register_ConfirmButton, registerForm.ToSql);
            Click(registered_backButton, UI.LoginScreen);

        }
        
        if (SceneManager.GetActiveScene().name == "StudentMenu")
        {
            Click(student_settingsButton, UI.SettingsPanel);
            Click(logOutButton, UI.LogOut);
            Click(closeSettingsButton, UI.Close_settings);
            //Click(LevelsButton, UI.LevelsScreen);
            Click(BackLevelsButton, UI.Back_LevelsScreen);
        }
        

        if (currentScene >= 4) 
        {
            Click(notesButton, UI.OpenNotes);
            Click(closeNotesButton, UI.CloseNotes);
            Click(helpButton, UI.HelpStudent);
            Click(closeHelpButton, UI.CloseHelpStudent);
            Click(finishButton, UI.FinishAssessment);
            Click(returnButton, () => Switch("StudentMenu"));

        }

        if (SceneManager.GetActiveScene().name == "FeedbackAssessment")
        {
            Click(NextLevelButton, () => NextLevel());

        }

        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }

    }
}
