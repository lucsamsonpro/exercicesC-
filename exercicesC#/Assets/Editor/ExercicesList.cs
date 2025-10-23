using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class ExercicesList : EditorWindow
{
    private Vector2 scrollPos;
    private List<Exercise> exercises;
    private string apiKey = "";
    private bool showApiOptions = false;
    private bool showInstructions = false; // nouvel état pour les instructions

    [MenuItem("Window/Exercices List")]
    public static void ShowWindow()
    {
        GetWindow<ExercicesList>("Exercices List");
    }

    private void OnEnable()
    {
        // Récupérer la clé API sauvegardée
        apiKey = EditorPrefs.GetString("ExercicesList_apiKey", "");
        exercises = new List<Exercise>
        {
            new Exercise { id = "exercice1", title = "Exo 1 : Visibilité alternée", description = "Le script doit afficher/masquer l'objet sur lequel il est placé lors de l'appuie sur Espace.", scriptPath = "Assets/Exercices/Exercice1.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice1", 0) == 1 },
            new Exercise { id = "exercice2", title = "Exo 2 : Couleur aléatoire", description = "Le script doit modifier la couleur de l'objet sur lequel il est placé. La couleur change aléatoirement chaque seconde.", scriptPath = "Assets/Exercices/Exercice2.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice2", 0) == 1 },
            new Exercise { id = "exercice3", title = "Exo 3 : Transformation", description = "Le script doit modifier la forme (primitive) de l'objet. Si on appuie sur Espace, la forme de l'objet change aléatoirement.", scriptPath = "Assets/Exercices/Exercice3.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice3", 0) == 1 },
            new Exercise { id = "exercice4", title = "Exo 4 : Déplacement", description = "Le script doit permettre de déplacer un objet grâce aux flèches du clavier et Espace pour faire sauter l'objet.", scriptPath = "Assets/Exercices/Exercice4.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice4", 0) == 1 },
            new Exercise { id = "exercice5", title = "Exo 5 : Trou noir", description = "Le script doit transformer l'objet en trou noir : Il doit aspirer tous les objets qui se trouvent à moins de 3 unités.", scriptPath = "Assets/Exercices/Exercice5.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice5", 0) == 1 },
            new Exercise { id = "exercice6", title = "Exo 6 : Mon voisin", description = "Le script doit cibler/trouver l'objet le plus proche de l'objet qui possède ce script. Il faut colorer l'objet le plus proche en rouge.", scriptPath = "Assets/Exercices/Exercice6.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice6", 0) == 1 },
            new Exercise { id = "exercice7", title = "Exo 7 : Rotation", description = "Le script doit permettre de faire tourner un objet sur lui même avec la souris. Si l'utilisateur clique et bouge la souris, l'objet tourne en adéquation.", scriptPath = "Assets/Exercices/Exercice7.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice7", 0) == 1 },
            new Exercise { id = "exercice8", title = "Exo 8 : Changement de perspective", description = "Le script doit permettre de changer de perspective de caméra (passer de perspective à orthographique et inversement). Utiliser Espace pour changer la perspective.", scriptPath = "Assets/Exercices/Exercice8.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice8", 0) == 1 },
            new Exercise { id = "exercice9", title = "Exo 9 : Écrire dans un fichier", description = "Le script doit permettre de créer un fichier texte contenant HELLO WORLD. Le fichier dois automatiquement être ouvert au lancement.", scriptPath = "Assets/Exercices/Exercice9.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice9", 0) == 1 },
            new Exercise { id = "exercice10", title = "Exo 10 : Balle rebondissante", description = "Le script doit permettre de créer une balle rebondissante. Le script doit être placé sur un objet vide. L'intégralité de la scène doit être généré par script.", scriptPath = "Assets/Exercices/Exercice10.cs", image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image as Texture2D, completed = EditorPrefs.GetInt("ExercicesList_exercice10", 0) == 1 }
        };
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 24,
            alignment = TextAnchor.MiddleCenter,
            padding = new RectOffset(10, 10, 10, 10)
        };
        GUILayout.Label("Liste des exercices", titleStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        
        // Ajout du menu déroulant "Instructions"
        showInstructions = EditorGUILayout.Foldout(showInstructions, "Instructions", true);
        if (showInstructions)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Chaque exercice correspond à un script à compléter. Il faut modifier les scripts sans supprimer l'énoncé. Pour chaque exercice il faudra créer un objet sur la scène et attacher le script de l'exercice sur cer objet. Parfois il faudra ajouter d'autres objets pour gérer les interactions. Vous devez au maximum faire les choses par script et pas via l'éditeur, en effet, votre solution sera vérifiée par IA et l'IA n'est capable que de regarder votre code, pas votre scène. Faites donc en sorte de tout faire par script (exemple : si vous avez besoin d'un rigidbody, créez le par script et pas via l'éditeur).", EditorStyles.wordWrappedLabel);
            EditorGUILayout.EndVertical();
        }
        
        showApiOptions = EditorGUILayout.Foldout(showApiOptions, "Options API", true);
        if (showApiOptions)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Clé API Groq :", GUILayout.Width(85));
            EditorGUI.BeginChangeCheck();
            apiKey = EditorGUILayout.TextField(apiKey);
            if (EditorGUI.EndChangeCheck())
            {
                // Sauvegarder la nouvelle clé API
                EditorPrefs.SetString("ExercicesList_apiKey", apiKey);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Obtenez votre clé API Groq :", GUILayout.Width(220));
            if (GUILayout.Button("console.groq.com", EditorStyles.linkLabel))
            {
                Application.OpenURL("https://console.groq.com/");
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Regarder le tutoriel vidéo", EditorStyles.linkLabel))
            {
                Application.OpenURL("https://www.youtube.com/watch?v=1zcrjkr2juE");
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        int total = exercises != null ? exercises.Count : 0;
        int completedCount = exercises != null ? exercises.FindAll(e => e.completed).Count : 0;
        int remainingCount = total - completedCount;
        EditorGUILayout.BeginHorizontal("box");
        GUILayout.Label("Terminé : " + completedCount, GUILayout.ExpandWidth(true));
        GUILayout.Label("Restant : " + remainingCount, GUILayout.ExpandWidth(true));
        GUILayout.Label("Total : " + total, GUILayout.ExpandWidth(true));
        EditorGUILayout.EndHorizontal();

        // Ajout du lien "Apprendre Unity et C#"
        if (GUILayout.Button("Apprendre Unity et C#", EditorStyles.linkLabel))
        {
            Application.OpenURL("https://www.udemy.com/course/formation-unity-par-la-pratique-le-cours-ultime-tout-en-1-unity/?referralCode=F158E1E111F89AEEAB52");
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        foreach (var ex in exercises)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(ex.image, GUILayout.Width(50), GUILayout.Height(50));
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(ex.title, EditorStyles.boldLabel);
            Texture2D statusIcon = ex.completed ? EditorGUIUtility.IconContent("TestPassed").image as Texture2D : EditorGUIUtility.IconContent("TestFailed").image as Texture2D;
            GUILayout.Label(statusIcon, GUILayout.Width(20), GUILayout.Height(20));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField(ex.description, EditorStyles.wordWrappedLabel);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Ouvrir l'exercice"))
            {
                OpenScript(ex.scriptPath);
            }
            if (GUILayout.Button("Vérifier le script"))
            {
                CheckScript(ex.scriptPath);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            GUILayout.Space(10);
        }

        GUILayout.Space(20);
        Rect bonhommeRect = GUILayoutUtility.GetRect(200, 210, GUILayout.ExpandWidth(false));
        bonhommeRect.x = (position.width - 200) / 2;

        EditorGUI.DrawRect(new Rect(bonhommeRect.x, bonhommeRect.y, 200, 200), new Color(0.8f, 0.8f, 0.8f));

        Vector2 leftEyeCenter = new Vector2(bonhommeRect.x + 60, bonhommeRect.y + 80);
        Vector2 rightEyeCenter = new Vector2(bonhommeRect.x + 140, bonhommeRect.y + 80);
        float eyeRadius = 20;

        Handles.BeginGUI();
        Handles.color = Color.white;
        Handles.DrawSolidDisc(leftEyeCenter, Vector3.forward, eyeRadius);
        Handles.DrawSolidDisc(rightEyeCenter, Vector3.forward, eyeRadius);

        Vector2 mousePos = Event.current.mousePosition;
        float pupilMaxOffset = 5f;
        Vector2 leftDir = (mousePos - leftEyeCenter).normalized * pupilMaxOffset;
        Vector2 rightDir = (mousePos - rightEyeCenter).normalized * pupilMaxOffset;

        Handles.color = Color.black;
        Handles.DrawSolidDisc(leftEyeCenter + leftDir, Vector3.forward, 8);
        Handles.DrawSolidDisc(rightEyeCenter + rightDir, Vector3.forward, 8);

        int totalEx = exercises != null ? exercises.Count : 0;
        int completedEx = exercises != null ? exercises.FindAll(e => e.completed).Count : 0;
        float smileRatio = totalEx > 0 ? (float)completedEx / totalEx : 0f;
        Vector2 smileStart = new Vector2(bonhommeRect.x + 50, bonhommeRect.y + 130);
        Vector2 smileEnd = new Vector2(bonhommeRect.x + 150, bonhommeRect.y + 130);
        Vector2 controlOffset = new Vector2(0, 20 * smileRatio);
        Vector2 smileCP1 = smileStart + controlOffset;
        Vector2 smileCP2 = smileEnd + controlOffset;
        Handles.DrawBezier(smileStart, smileEnd, smileCP1, smileCP2, Color.black, null, 2);

        Handles.EndGUI();

        EditorGUILayout.EndScrollView();
        
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Mettre à jour | Page officielle", EditorStyles.linkLabel))
        {
            Application.OpenURL("https://unity3d-dev.com/exercices-de-codage-c-pour-unity-plugin/");
        }
        GUILayout.FlexibleSpace();
        GUILayout.Label("1.0.0");
        EditorGUILayout.EndHorizontal();
    }

    private void OpenScript(string scriptPath)
    {
        var scriptAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
        if (scriptAsset != null)
        {
            AssetDatabase.OpenAsset(scriptAsset);
        }
        else
        {
            Debug.LogError("Script not found at: " + scriptPath);
        }
    }

    private async void CheckScript(string scriptPath)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            EditorUtility.DisplayDialog("Clé API manquante", "Veuillez ajouter votre clé API Groq dans les options.", "OK");
            return;
        }
        var scriptAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
        if (scriptAsset != null)
        {
            string absolutePath = Application.dataPath.Replace("Assets", "") + scriptPath;
            string scriptContent = File.ReadAllText(absolutePath);
            Debug.Log("Contenu du script (" + Path.GetFileName(scriptPath) + ") :\n" + scriptContent);
            
            // Déléguer l'appel à la méthode CheckScriptAsync du helper
            string result = await Helpers.ScriptChecker.CheckScriptAsync(apiKey, scriptContent);
            if (result == null) return;

            Debug.Log(result);
            string proposedSolutionCheck = result.Split(' ')[0];
            
            if (proposedSolutionCheck.Equals("CORRECT", System.StringComparison.OrdinalIgnoreCase))
            {
                foreach (var ex in exercises)
                {
                    if (ex.scriptPath == scriptPath)
                    {
                        ex.completed = true;
                        EditorPrefs.SetInt("ExercicesList_" + ex.id, 1);
                        EditorUtility.DisplayDialog("Exercice valide", "L'exercice a été validé.", "OK");
                        break;
                    }
                }
                Repaint();
            }
            else if (proposedSolutionCheck.Equals("INCORRECT", System.StringComparison.OrdinalIgnoreCase))
            {
                EditorUtility.DisplayDialog("Exercice invalide", "L'exercice est incorrect. Veuillez corriger le script : " + result, "OK");
            }
        }
        else
        {
            Debug.LogError("Script non trouvé à : " + scriptPath);
        }
    }

    private class Exercise
    {
        public string id;
        public string title;
        public string description;
        public string scriptPath;
        public Texture2D image;
        public bool completed;
    }
}
