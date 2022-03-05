/* Amanda Patricia Viray
 * January 2022
 * 
 * UIManager is a class that contains functions for managing UI
 * when activated/deactivated. 
 * 
 * Functions:
 * - Hide(GameObject)
 * - Show(GameObject)
 * 
 * LOGIN SCREEN
 * - LoginScreen()
 * - RegisterScreen(), ConfirmRegister()
 * - StudentLogin(), AdminLogin()
 * - LogOut()
 * 
 * STUDENT MENU
 * - SettingsPanel(), Close_settings()
 * - LevelsScreen(), Back_LevelsScreen()
 * - OpenSettings(), CloseSettings()
 * 
 * VIRTUAL PATIENT SCENE
 * - EnablePatientDialog(), DisablePatientDialog()
 * - etc...
 * 
 */

using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    [HideInInspector]
    [SerializeField] GameObject 
        LoginUI, RegisterUI, ConfirmRegisterUI, StudentUI, Student_SettingsUI, Student_LevelsUI, AdminUI,
        
        MainUI, HelpUI, PauseUI, NotesUI, FinishUI, PatientDialogUI;
    enum UIType { MainMenu, StudentMenu, AdminMenu, VirtualPatient }
    [Header("UI View Manager")]
    [SerializeField] UIType UIView;
    bool showMainMenu = false;
    bool showVirtualPatient = false;
    bool showStudentMenu = false;
    bool showAdminMenu = false;

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(UIManager))]
    public class UIManagerrEditor : Editor
    {
        private UIManager uiM;
        void Awake()
        {
            uiM = (UIManager)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //Serialize

            if (uiM.UIView == UIType.MainMenu)
            {
                uiM.showMainMenu = EditorGUILayout.Foldout(uiM.showMainMenu, "Main Menu Screens", false);

                if (uiM.showMainMenu)
                {

                    uiM.LoginUI = (GameObject)EditorGUILayout.ObjectField("Login UI: ", uiM.LoginUI, typeof(GameObject), true);
                    uiM.RegisterUI = (GameObject)EditorGUILayout.ObjectField("Register UI: ", uiM.RegisterUI, typeof(GameObject), true);
                    uiM.ConfirmRegisterUI = (GameObject)EditorGUILayout.ObjectField("Confirmed Register UI: ", uiM.ConfirmRegisterUI, typeof(GameObject), true);
                    
                }

            } // End Main Menu View

            if (uiM.UIView == UIType.StudentMenu)
            {
                uiM.showStudentMenu = EditorGUILayout.Foldout(uiM.showStudentMenu, "Student Screens", false);

                if (uiM.showMainMenu)
                {
                    uiM.StudentUI = (GameObject)EditorGUILayout.ObjectField("Student UI: ", uiM.StudentUI, typeof(GameObject), true);
                    uiM.Student_SettingsUI = (GameObject)EditorGUILayout.ObjectField("Student - Settings UI: ", uiM.Student_SettingsUI, typeof(GameObject), true);
                    uiM.Student_LevelsUI = (GameObject)EditorGUILayout.ObjectField("Student - Levels UI: ", uiM.Student_LevelsUI, typeof(GameObject), true);
                }

            } // End Student View

            if (uiM.UIView == UIType.AdminMenu)
            {
                uiM.showAdminMenu = EditorGUILayout.Foldout(uiM.showAdminMenu, "Admin Screens", false);

                if (uiM.showAdminMenu)
                {
                    EditorGUILayout.HelpBox("Admin Menu shows up here", MessageType.Info, true);
                }

            } // End Student View

            if (uiM.UIView == UIType.VirtualPatient)
            {
                uiM.showVirtualPatient = EditorGUILayout.Foldout(uiM.showVirtualPatient, "Virtual Patient Screens", false);

                if (uiM.showVirtualPatient)
                {
                    uiM.MainUI = (GameObject)EditorGUILayout.ObjectField("Main UI: ", uiM.MainUI, typeof(GameObject), true);
                    uiM.PauseUI = (GameObject)EditorGUILayout.ObjectField("Pause UI: ", uiM.PauseUI, typeof(GameObject), true);

                    uiM.HelpUI = (GameObject)EditorGUILayout.ObjectField("Help UI: ", uiM.HelpUI, typeof(GameObject), true);
                    uiM.NotesUI = (GameObject)EditorGUILayout.ObjectField("Notes UI: ", uiM.NotesUI, typeof(GameObject), true);
                    uiM.FinishUI = (GameObject)EditorGUILayout.ObjectField("Finish UI: ", uiM.FinishUI, typeof(GameObject), true);
                    uiM.PatientDialogUI = (GameObject)EditorGUILayout.ObjectField("Patient Dialog UI:", uiM.PatientDialogUI, typeof(GameObject), true);
                }
            } // End Virtual Patient View

            if (GUI.changed)
                EditorUtility.SetDirty(uiM);
        }
    }
#endif
    #endregion

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

    public virtual void Hide(GameObject UI) => UI.SetActive(false);
    public virtual void Show(GameObject UI) => UI.SetActive(true);


    //Functions to change the login screen UI
    public void LoginScreen() { Show(LoginUI); Hide(RegisterUI); Hide(ConfirmRegisterUI);}
    
    public void RegisterScreen() { Hide(LoginUI); Show(RegisterUI);}

    public void StudentLogin() { SceneManager.LoadScene("StudentMenu"); }
    public void AdminLogin() { SceneManager.LoadScene("AdminMenu"); }
    public void ConfirmRegister(){ Show(ConfirmRegisterUI); }
    public void LogOut() { SceneManager.LoadScene("Authentication"); Hide(Student_SettingsUI); }

    //Student Menu UI
    public void SettingsPanel() { Show(Student_SettingsUI); }
    public void Close_settings() { Hide(Student_SettingsUI); }
    public void LevelsScreen() { Show(Student_LevelsUI); }
    public void Back_LevelsScreen() { Hide(Student_LevelsUI); Show(StudentUI); }

    // Virtual Paitent Menu UI
    public void EnablePatientDialog() { Show(PatientDialogUI); }
    public void DisablePatientDialog() { Hide(PatientDialogUI); }
    public void PauseScreen() { Show(PauseUI); }
    public void ResumeScreen() { Hide(PauseUI); }
    public void HelpStudent() { Show(HelpUI); }
    public void CloseHelpStudent() { Hide(HelpUI); }
    public void OpenNotes() { Show(NotesUI); }
    public void CloseNotes() { Hide(NotesUI); }
    public void FinishAssessment() { Show(FinishUI); }

    //This is to quick start the demo by adding this function into the Start Button in the student menu.
    public void StartVP() { SceneManager.LoadScene("VirtualPatient1");  }


}
