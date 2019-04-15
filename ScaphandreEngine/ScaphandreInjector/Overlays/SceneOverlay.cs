using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScaphandreInjector.Overlays
{

    public class SceneOverlay : MonoBehaviour
    {
        private static string stringRepresentation = "SCENE - F4 + S\n\nPlease wait...";
        private static string searchPattern = "";
        private static string lastSearchPattern = "";
        private static bool lastShowProperties = false;
        public static bool showScene = false;
        public static bool isBuildingRepresentation = false;
        public static bool showFields = false;
        private static Vector2 scrollPos = Vector2.zero;
        private static Coroutine representationCoroutine;

        public class Element {
            public bool match = false;
            public string representation = "";

            public Element()
            {
            }

            public Element(string representation)
            {
                this.representation = representation;
            }

            public Element(bool match, string representation)
            {
                this.match = match;
                this.representation = representation;
            }

            public void AddChild(Element element)
            {
                if (element.match)
                {
                    match = true;
                    representation += element.representation;
                }
            }
        }

        GUIStyle textStyle;

        public void OnEnable()
        {
            textStyle = new GUIStyle()
            {
                alignment = TextAnchor.UpperLeft,
                fontSize = 18,
            };

            textStyle.richText = false;

            textStyle.normal.textColor = new Color(1, 1, 1);
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.F4) && Input.GetKeyDown(KeyCode.S))
            {
                showScene = !showScene;
            }
        }

        void OnGUI()
        {
            if (!showScene) return;
            
            if (searchPattern != lastSearchPattern || showFields != lastShowProperties)
            {
                if(representationCoroutine != null)
                {
                    StopCoroutine(representationCoroutine);
                }
                isBuildingRepresentation = false;
                lastSearchPattern = searchPattern;
                lastShowProperties = showFields;
            }

            if (!isBuildingRepresentation)
            {
                isBuildingRepresentation = true;
                representationCoroutine = StartCoroutine(BuildRepresentation());
            }

            int width = Screen.width;
            int height = Screen.height;

            GUI.Box(new Rect(0, 0, width, height), GUIContent.none);

            scrollPos = GUI.BeginScrollView(new Rect(0, 0, width, height - 31), scrollPos, new Rect(0, 0, width - 10, 30000));
            GUI.Label(new Rect(0, 0, width - 10, 30000), stringRepresentation, textStyle);
            GUI.EndScrollView();

            searchPattern = GUI.TextField(new Rect(0, height - 30, width, 30), searchPattern);
            showFields = GUI.Toggle(new Rect(width - 140, height - 70, 100, 30), showFields, " Show fields");
        }

        IEnumerator BuildRepresentation()
        {
            var scene = SceneManager.GetActiveScene();

            var root = new Element();

            foreach (var trans in FindObjectsOfType<Transform>())
            {
                if (trans.parent == null)
                {
                    root.AddChild(AddElement(trans.gameObject, 1, searchPattern.ToLower()));
                    yield return null;
                }
            }
            stringRepresentation = "SCENE - F4 + S\n\n[" + scene.buildIndex + "] " + scene.name + " (" + scene.path + ")" + root.representation;
        }

        Element AddElement(GameObject obj, int indentation, string searchPattern = null)
        {
            var element = new Element
            {
                match = searchPattern == "" || obj.name.ToLower().Contains(searchPattern),
                representation = "\n" + new string(' ', indentation * 8) + obj.name
            };

            foreach(var component in obj.GetComponents<Component>())
            {
                element.AddChild(AddElement(component, indentation, searchPattern));
            }

            int childCount = obj.transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                element.AddChild(AddElement(obj.transform.GetChild(i).gameObject, indentation + 1, searchPattern));
            }

            return element;
        }

        Element AddElement(Component comp, int indentation, string searchPattern = null)
        {
            var type = comp.GetType();
            var fullName = type.FullName;
            var newLine = "\n" + new String(' ', indentation * 8) + "   ";

            var elementRepresentation = newLine + " - " + fullName;
            var doesMatch = searchPattern == "" || fullName.ToLower().Contains(searchPattern) || showFields && type.GetFields().Any(t => t.Name.ToLower().Contains(searchPattern));

            if (doesMatch && showFields)
            {
                var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                var fieldsLength = fields.Length;
                for (var i = 0; i < fieldsLength; i++)
                {
                    var field = fields[i];
                    var icon = i == fieldsLength - 1 ? "└" : "├";

                    elementRepresentation += newLine + "   " + icon + " " + field.Name + ":  "
                        + string.Join(newLine + "   │" + new string(' ', field.Name.Length * 2 + 4), (field.GetValue(comp) ?? "null").ToString().Replace("*", "\\*").Split('\n'));
                }
            }

            return new Element(
                doesMatch,
                elementRepresentation
            );
        }
    }
}

