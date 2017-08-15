using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

namespace FlowMapEditor
{
    public class FlowMapEditor : EditorWindow
    {
        [MenuItem("Tools/Flowhiz")]
        static void Init()
        {
            FlowMapEditor window = (FlowMapEditor)EditorWindow.GetWindow(typeof(FlowMapEditor), false, "Flowhiz");
            window.Show();
        }

        const int MAX_UNDO_OPERATIONS_COUNT = 10;

        private Texture2D flowTexture;
        private Texture2D FlowTexture
        {
            get
            {
                if (flowTexture == null)
                {
                    flowTexture = new Texture2D(256, 256, TextureFormat.ARGB32, false, true);
                    CommandManager.ExecuteCommand(new GenerateTextureCommand(flowTexture, 256, new Color(0.5f, 0.5f, 1.0f, 1.0f)));
                }
                return flowTexture;
            }
        }

        private Camera mainCamera;

        private Brush brush;
        private Brush Brush
        {
            get 
            {
                if (brush == null)
                {
                    brush = new Brush();
                }
                return brush;
            }
        }

        private CommandManager commandManager;
        private CommandManager CommandManager
        {
            get
            {
                if (commandManager == null)
                {
                    commandManager = new CommandManager(MAX_UNDO_OPERATIONS_COUNT);
                }
                return commandManager;
            }
        }

        private bool showBlueChanel = true;
        private bool showRedGreenChanel = true;
        private bool isPreviewMode = false;

        private Material storedMaterial;
        private Material previewMaterial;
        private Material PreviewMaterial
        {
            get
            {
                if (previewMaterial == null)
                {
                    previewMaterial = Resources.Load<Material>("TestWaterMat");
                }
                return previewMaterial;
            }
        }
        private Material editorMaterial;
        private Material EditorMaterial
        {
            get
            {
                if (editorMaterial == null)
                {
                    editorMaterial = Resources.Load<Material>("FlowBrushMat");

                }
                return editorMaterial;
            }
        }

        private MeshCollider meshCollider;
        private GameObject currentObject;
        private Renderer renderer;
        private bool isGeneratingFlowMapMode;
        private int flowTextureSize = 6;
        private Vector2 startFlowDirection = new Vector2();
        private float startOpacity = 1;

        private void OnGUI()
        {
            if (Selection.activeGameObject == null)
            {
                GUILayout.Label("Please select object in hierarchy");
                return;
            }

            if (renderer == null)
            {
                GUILayout.Label("Please select mesh with render component");
                return;
            }

            if (meshCollider == null)
            {
                GUILayout.Label("Please add mesh collider to selected object");
                return;
            }

            if (GUILayout.Button(isGeneratingFlowMapMode ? "Close params for generating flow map" : "Open params for generating flow map"))
            {
                isGeneratingFlowMapMode = !isGeneratingFlowMapMode;
            }

            if (isGeneratingFlowMapMode)
            {
                flowTextureSize = EditorGUILayout.IntSlider("Size : " + (2 << flowTextureSize).ToString(), flowTextureSize, 4, 8);
                EditorGUILayout.LabelField("Select start flow direction", EditorStyles.boldLabel);

                float labelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 30;

                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.BeginHorizontal();
                startFlowDirection = EditorGUILayout.Toggle("NW", startFlowDirection.x == 0 && startFlowDirection.y == 0) ? new Vector2(0, 0) : startFlowDirection;
                startFlowDirection = EditorGUILayout.Toggle("N", startFlowDirection.x == 0.5f && startFlowDirection.y == 0) ? new Vector2(0.5f, 0) : startFlowDirection;
                startFlowDirection = EditorGUILayout.Toggle("NE", startFlowDirection.x == 1.0f && startFlowDirection.y == 0) ? new Vector2(1.0f, 0) : startFlowDirection;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                startFlowDirection = EditorGUILayout.Toggle("W", startFlowDirection.x == 0 && startFlowDirection.y == 0.5f) ? new Vector2(0, 0.5f) : startFlowDirection;
                startFlowDirection = EditorGUILayout.Toggle("0", startFlowDirection.x == 0.5f && startFlowDirection.y == 0.5f) ? new Vector2(0.5f, 0.5f) : startFlowDirection;
                startFlowDirection = EditorGUILayout.Toggle("E", startFlowDirection.x == 1.0f && startFlowDirection.y == 0.5f) ? new Vector2(1.0f, 0.5f) : startFlowDirection;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                startFlowDirection = EditorGUILayout.Toggle("SW", startFlowDirection.x == 0 && startFlowDirection.y == 1.0f) ? new Vector2(0, 1.0f) : startFlowDirection;
                startFlowDirection = EditorGUILayout.Toggle("S", startFlowDirection.x == 0.5f && startFlowDirection.y == 1.0f) ? new Vector2(0.5f, 1.0f) : startFlowDirection;
                startFlowDirection = EditorGUILayout.Toggle("SE", startFlowDirection.x == 1.0f && startFlowDirection.y == 1.0f) ? new Vector2(1.0f, 1.0f) : startFlowDirection;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
                
                EditorGUIUtility.labelWidth = labelWidth;

                startOpacity = EditorGUILayout.Slider("Set opacity", startOpacity, 0, 1);

                if (GUILayout.Button("Generate"))
                {
                    CommandManager.ExecuteCommand(new GenerateTextureCommand(FlowTexture, 2 << flowTextureSize, new Color(startFlowDirection.x, startFlowDirection.y, startOpacity, 1.0f)));
                    isGeneratingFlowMapMode = false;
                }
            }

            if (GUILayout.Button("Load flow map"))
            {
                string path = EditorUtility.OpenFilePanel("Open ", Application.dataPath, "png");
                if (path.Length != 0)
                {
                    WWW www = new WWW("file:///" + path);
                    www.LoadImageIntoTexture(FlowTexture);
                }
            }


            showBlueChanel = EditorGUILayout.Toggle("Show chanel for alpha", showBlueChanel || Brush.drawType == Brush.DrawType.Alpha);
            showRedGreenChanel = EditorGUILayout.Toggle("Show flowmap chanels", showRedGreenChanel || Brush.drawType == Brush.DrawType.FlowMap);

            Brush.drawType = (Brush.DrawType)EditorGUILayout.EnumPopup("Draw type", Brush.drawType);
            Brush.Softness = EditorGUILayout.Slider("Softness ", Brush.Softness, 0, 1);
            Brush.Size = EditorGUILayout.Slider("Size ", Brush.Size, 0.01f, 1);

            if (Brush.drawType == Brush.DrawType.FlowMap)
            {
                Brush.AngleToSmooth = (int)EditorGUILayout.Slider("Smooth angle ", Brush.AngleToSmooth, 5, 180);
            }
            else if (Brush.drawType == Brush.DrawType.Alpha)
            {
                Brush.AlphaIntensivity = EditorGUILayout.Slider("Alpha Intensivity", Brush.AlphaIntensivity, 0, 1);
            }

            renderer.sharedMaterial.SetFloat("_ShowBlue", showBlueChanel ? 1 : 0);
            renderer.sharedMaterial.SetFloat("_ShowRedGreen", showRedGreenChanel ? 1 : 0);
            renderer.sharedMaterial.SetFloat("_BrushSize", Brush.Size);
            renderer.sharedMaterial.SetFloat("_BrushStrange", Brush.Softness);

            if (GUILayout.Button("Clear"))
            {
                CommandManager.ExecuteCommand(new ClearCommand(FlowTexture));
            }

            if (GUILayout.Button("Save"))
            {
                string path = EditorUtility.SaveFilePanel("Save ", Application.dataPath, "flowMap", "png");
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }
                System.IO.File.WriteAllBytes(path, FlowTexture.EncodeToPNG());
            }

            UpdatePreviewMode();

            GUILayout.Space(5);
            GUILayout.BeginVertical("box");
            GUILayout.Label("For undo last action press \"U\"");
            GUILayout.EndHorizontal();
        }

        private void UpdatePreviewMode()
        {
            bool newPreviewMode = EditorGUILayout.Toggle("Enable preview mode", isPreviewMode);
            if (newPreviewMode != isPreviewMode)
            {
                if (newPreviewMode)
                {
                    Shader.EnableKeyword("FLOW_EDITOR_PREVIEW_MODE");
                    renderer.sharedMaterial = PreviewMaterial;
                    PreviewMaterial.SetTexture("FlowMap", flowTexture);
                }
                else
                {
                    Shader.DisableKeyword("FLOW_EDITOR_PREVIEW_MODE");
                    renderer.sharedMaterial = EditorMaterial;
                }
            }

            isPreviewMode = newPreviewMode;

            if (isPreviewMode)
            {
                Shader.SetGlobalFloat("EditorTime", Time.realtimeSinceStartup);
            }
        }

        void OnFocus()
        {
            if (mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }

            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
            SceneView.onSceneGUIDelegate += this.OnSceneGUI;
        }

        private void OnEnable()
        {
            storedMaterial = null;
            Initialize();
        }

        private void OnDisable()
        {
            if (currentObject)
            {
                currentObject.GetComponent<Renderer>().sharedMaterial = storedMaterial;
            }
            Shader.DisableKeyword("FLOW_EDITOR_PREVIEW_MODE");
        }

        private void OnSelectionChange()
        {
            Initialize();
            Repaint();
        }

        private void Initialize()
        { 
            if (currentObject)
            {
                if (storedMaterial)
                {
                    currentObject.GetComponent<Renderer>().sharedMaterial = storedMaterial;
                }

                currentObject = null;
                renderer = null;
                meshCollider = null;
            }

            if (Selection.activeGameObject == null)
            {
                return;
            }


            renderer = Selection.activeGameObject.GetComponent<Renderer>();
            if (renderer == null)
            {
                return;
            }

            meshCollider = Selection.activeGameObject.GetComponent<MeshCollider>();
            if (meshCollider == null)
            {
                return;
            }

            storedMaterial = renderer.sharedMaterial;
            currentObject = Selection.activeGameObject;
            renderer.sharedMaterial = EditorMaterial;
            EditorMaterial.mainTexture = FlowTexture;
        }

        private void OnDestroy()
        {
            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
        }

        private bool isButtonDown;
        
        private void OnSceneGUI(SceneView sceneView)
        {
            Handles.BeginGUI();
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            if (meshCollider == null)
            {
                return;
            }

            RaycastHit hit;
            if (meshCollider.Raycast(Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x, Camera.current.pixelRect.height - Event.current.mousePosition.y, 0)), out hit, 10000))
            {
                Vector3 worldPosition = hit.point;
                Brush.UpdateCurrentUVPosition(hit.textureCoord, new Vector2(worldPosition.x, worldPosition.z), 1.0f / FlowTexture.width);
            }
            else
            {
                Brush.UpdateCurrentUVPosition(new Vector2(-2, -2), new Vector2(), 1.0f / FlowTexture.width);
                isButtonDown = false;
            }

            renderer.sharedMaterial.SetVector("_BrushPosition", Brush.CurrentUVPosition);

            if (Event.current.type == EventType.mouseDown && Event.current.button == 0)
            {
                if (!isButtonDown)
                {
                    CommandManager.ExecuteCommand(new StoreTextureCommand(FlowTexture));
                }

                isButtonDown = true;
            }

            if (Event.current.type == EventType.mouseUp && Event.current.button == 0)
            {
                isButtonDown = false;
            }

            if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.U)
            {
                CommandManager.Undo();
            }
            
            renderer.sharedMaterial = isPreviewMode ? PreviewMaterial : editorMaterial;
            
            if (isButtonDown &&  Brush.IsCanDrawing(1.0f / FlowTexture.width))
            {
                CommandManager.ExecuteCommand(new PaintCommand(Brush, FlowTexture));
            }

            Handles.EndGUI();
            sceneView.Repaint();
        }
    }
}
