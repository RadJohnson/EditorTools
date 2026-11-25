using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace RADJ_Tools
{
    public class FolderStructureSetup : EditorWindow
    {
        private static FolderStructureSetup window;
        Vector2 scrollPos;
        
        // Foldout states
        private bool showProjectSetup = true;
        private bool showCodeSetup = false;
        private bool showArtSetup = false;
        private bool showAudioSetup = false;
        private bool showSceneManagementSetup = false;
        private bool showDocumentationSetup = false;

        //parent directories
        private bool includeCode = false;
        private bool includeArt = false;
        private bool includeAudio = false;
        private bool includeLevelDesign = false; //folders in UI and scene assets
        private bool includeDocumentation = false;


        //Code
        private bool includeScripts = false;
        private bool includeShaders = false;
        private bool includeScriptableObjects = false;
        private bool includeInputSystem = false;
        private bool editorScripts = false;
        
        //Art
        private bool includeModels = false;
        private bool includeMaterials = false;
        private bool includeTextures = false;
        private bool includeAnimation = false;


        //Audio
        private bool includeMusic = false;
        private bool includeSFX = false;


        //UI / Scene management
        private bool includeScenes = false;
        private bool includePrefabs = false;
        private bool includePostProcessing = false;
        private bool includeUiAssets = false;

        //Documentation
        private bool includeDesignDocumentation = false;
        private bool includeConceptArt = false;
        private bool includeTechnicalDocs = false;
        
        
        public static void InitialiseWindow()
        {
            window = EditorWindow.GetWindow<FolderStructureSetup>("Folder Structure Setup");
            window.Show();
        }

        void OnGUI()
        {
            //GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Folder Structure Setup", EditorStyles.boldLabel);

            scrollPos = GUILayout.BeginScrollView(scrollPos,false,true);

            if (GUILayout.Button("Generate folder Structure", GUILayout.Height(30), GUILayout.ExpandWidth(true)))
            {
                CreateFolder();
            }


            showProjectSetup = EditorGUILayout.Foldout(showProjectSetup, "Folder Structure Setup", true);

            if (showProjectSetup)
            {
                //GUILayout.BeginHorizontal();
                includeCode = EditorGUILayout.Toggle("Code", includeCode);
                includeArt = EditorGUILayout.Toggle("Art", includeArt);
                includeAudio = EditorGUILayout.Toggle("Audio", includeAudio);
                includeLevelDesign = EditorGUILayout.Toggle("Level Design", includeLevelDesign);
                includeDocumentation = EditorGUILayout.Toggle("Documentation", includeDocumentation);
                //GUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUI.indentLevel++;
                showCodeSetup = EditorGUILayout.Foldout(showCodeSetup, "Code", true);
                if (includeCode && showCodeSetup)
                {
                    EditorGUI.indentLevel++;
                    includeScripts = EditorGUILayout.Toggle("Scripts", includeScripts);
                    includeShaders = EditorGUILayout.Toggle("Shaders", includeShaders);
                    includeScriptableObjects = EditorGUILayout.Toggle("Scriptable Objects", includeScriptableObjects);
                    includeInputSystem = EditorGUILayout.Toggle("Input System", includeInputSystem);
                    editorScripts = EditorGUILayout.Toggle("Editor Scripts", editorScripts);
                    EditorGUI.indentLevel--;
                }

                showArtSetup = EditorGUILayout.Foldout(showArtSetup, "Art", true);
                if (includeArt && showArtSetup)
                {
                    EditorGUI.indentLevel++;
                    includeModels = EditorGUILayout.Toggle("Models", includeModels);
                    includeMaterials = EditorGUILayout.Toggle("Materials", includeMaterials);
                    includeTextures = EditorGUILayout.Toggle("Textures", includeTextures);
                    includeAnimation = EditorGUILayout.Toggle("Animation", includeAnimation);
                    EditorGUI.indentLevel--;
                }

                showAudioSetup = EditorGUILayout.Foldout(showAudioSetup, "Audio", true);
                if (includeAudio && showAudioSetup)
                {
                    EditorGUI.indentLevel++;
                    includeMusic = EditorGUILayout.Toggle("Music", includeMusic);
                    includeSFX = EditorGUILayout.Toggle("SFX", includeSFX);
                    EditorGUI.indentLevel--;
                }

                showSceneManagementSetup = EditorGUILayout.Foldout(showSceneManagementSetup, "UI / Scene Management", true);
                if (includeLevelDesign && showSceneManagementSetup)
                {
                    EditorGUI.indentLevel++;
                    includeScenes = EditorGUILayout.Toggle("Scenes", includeScenes);
                    includePrefabs = EditorGUILayout.Toggle("Prefabs", includePrefabs);
                    includePostProcessing = EditorGUILayout.Toggle("Post Processing", includePostProcessing);
                    includeUiAssets = EditorGUILayout.Toggle("UI Assets", includeUiAssets);
                    EditorGUI.indentLevel--;
                }

                showDocumentationSetup = EditorGUILayout.Foldout(showDocumentationSetup, "Documentation", true);
                if (includeDocumentation && showDocumentationSetup)
                {
                    EditorGUI.indentLevel++;
                    includeDesignDocumentation =
                        EditorGUILayout.Toggle("Design Documentation", includeDesignDocumentation);
                    includeConceptArt = EditorGUILayout.Toggle("Concept Art", includeConceptArt);
                    includeTechnicalDocs = EditorGUILayout.Toggle("Technical Docs", includeTechnicalDocs);
                    EditorGUI.indentLevel--;
                }

                EditorGUI.indentLevel--;
            }

            
            GUILayout.EndScrollView();
            
            if (window)
            {
                window.Repaint();
            }
        }

   

        void CreateFolder()
        {
            Debug.Log("Generating folder Structure");
            string assetPath = Application.dataPath; //returns the asset directory for the project 
            //Debug.Log(assetPath);
     
            List<string> directoriesToCreate = new List<string>();

            if (includeCode)
            {
                directoriesToCreate.Add("Code");
                if (includeScripts) directoriesToCreate.Add("Code/Scripts");
                if (includeShaders) directoriesToCreate.Add("Code/Shaders");
                if (includeScriptableObjects) directoriesToCreate.Add("Code/ScriptableObjects");
                if (includeInputSystem) directoriesToCreate.Add("Code/InputSystem");
                if (editorScripts) directoriesToCreate.Add("Code/Editor");
            }
            if (includeArt)
            {
                directoriesToCreate.Add("Art");
                if (includeModels) directoriesToCreate.Add("Art/Models");
                if (includeMaterials) directoriesToCreate.Add("Art/Materials");
                if (includeTextures) directoriesToCreate.Add("Art/Textures");
                if (includeAnimation) directoriesToCreate.Add("Art/Animation");
            }

            if (includeAudio)
            {
                directoriesToCreate.Add("Audio");
                if (includeMusic) directoriesToCreate.Add("Audio/Music");
                if (includeSFX) directoriesToCreate.Add("Audio/SFX");
            }

            if (includeLevelDesign)
            {
                directoriesToCreate.Add("LevelDesign");
                if (includeScenes) directoriesToCreate.Add("LevelDesign/Scenes");
                if (includePrefabs) directoriesToCreate.Add("LevelDesign/Prefabs");
                if (includePostProcessing) directoriesToCreate.Add("LevelDesign/PostProcessing");
                if (includeUiAssets) directoriesToCreate.Add("LevelDesign/UI");
            }

            if (includeDocumentation)
            {
                directoriesToCreate.Add("Documentation");
                if (includeDesignDocumentation) directoriesToCreate.Add("Documentation/Design");
                if (includeConceptArt) directoriesToCreate.Add("Documentation/ConceptArt");
                if (includeTechnicalDocs) directoriesToCreate.Add("Documentation/Technical");
            }

            foreach (string dir in directoriesToCreate)
            {
                string fullPath = Path.Combine(assetPath, dir);

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                    Debug.Log("Created: " + fullPath);
                }
            }
            
            AssetDatabase.Refresh();
        }
    }
}