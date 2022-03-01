
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScreenUI : MonoBehaviour
{
    enum PFType { Student, Admin }
    [Header("UI View Controller")]
    [SerializeField] PFType profileType;

    bool showStudent = false;
    bool showAdmin = false;

    [HideInInspector]
    [SerializeField] Text StudentNameText, StudentUsernameText, AdminNameText, AdminUsernameText;

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(ScreenUI))]
    public class ScreenUIEditor : Editor
    {
        private ScreenUI screenUI;
        void Awake()
        {
            screenUI = (ScreenUI)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //Serialize

            if (screenUI.profileType == PFType.Student)
            {
                screenUI.showStudent = EditorGUILayout.Foldout(screenUI.showStudent, "Student Text", false);

                if (screenUI.showStudent)
                {
                    screenUI.StudentNameText = (Text)EditorGUILayout.ObjectField("Student Name: ", screenUI.StudentNameText, typeof(Text), true);
                    screenUI.StudentUsernameText = (Text)EditorGUILayout.ObjectField("Student Username: ", screenUI.StudentUsernameText, typeof(Text), true);
                }

            } // End Main Menu View

            if (screenUI.profileType == PFType.Admin)
            {
                screenUI.showAdmin = EditorGUILayout.Foldout(screenUI.showAdmin, "Admin Text", false);

                if (screenUI.showAdmin)
                {
                    screenUI.AdminNameText = (Text)EditorGUILayout.ObjectField("Admin Name: ", screenUI.AdminNameText, typeof(Text), true);
                    screenUI.AdminUsernameText = (Text)EditorGUILayout.ObjectField("Admin Username: ", screenUI.AdminUsernameText, typeof(Text), true);
                }

            } // End Student Menu View

            if (GUI.changed)
                EditorUtility.SetDirty(screenUI);
        }
    }
#endif
    #endregion
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "AdminMenu")
        {
            AdminNameText.text = ("Welcome, " + LoginForm.AdminName + "!");
            AdminUsernameText.text = LoginForm.AdminUsername;
        }

        if (SceneManager.GetActiveScene().name == "StudentMenu")
        {
            StudentNameText.text = ("Welcome, " + LoginForm.StudentName + "!");
            StudentUsernameText.text = LoginForm.StudentUsername;
        }

    }
}
