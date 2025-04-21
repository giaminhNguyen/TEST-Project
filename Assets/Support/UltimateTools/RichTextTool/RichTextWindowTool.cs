#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UltimateHelper
{
    public class RichTextWindowTool : EditorWindow
    {
        private string                text     = "Hello $0{W}orld $0{!}";
        private List<RichTextElement> elements = new List<RichTextElement>() { new(){color = Color.green,size = 15, useSize = true,fontStyle = FontStyle.BoldAndItalic} };
        private Vector2               scrollPosition;

        [MenuItem("UltimateTools/Rich Text Window Tool",false,3)]
        public static void ShowWindow()
        {
            GetWindow<RichTextWindowTool>();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            DrawLeftPanel();
            GUILayout.Space(25);
            DrawRightPanel();
            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
        }
        
        private void DrawRightPanel()
        {
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField("Text:", EditorStyles.boldLabel);
            text = EditorGUILayout.TextArea(text, GUILayout.Height(50), GUILayout.ExpandWidth(true)); // Use TextArea instead of TextField
            EditorGUILayout.Space();
            var previewText = GetRichText();
            EditorGUILayout.LabelField("Code:", EditorStyles.boldLabel);
            GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
            EditorGUI.SelectableLabel(EditorGUILayout.GetControlRect(GUILayout.Height(100)), previewText, EditorStyles.textArea);
            GUI.backgroundColor = Color.white;
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("Preview:", EditorStyles.boldLabel);
            var centeredStyle = new GUIStyle(GUI.skin.textArea) { alignment = TextAnchor.MiddleCenter, richText = true };
            // GUILayout.Label(previewText, centeredStyle);
            EditorGUI.SelectableLabel(EditorGUILayout.GetControlRect(GUILayout.MinHeight(80),GUILayout.Height(80),GUILayout.ExpandHeight(true)), previewText, centeredStyle);
            EditorGUILayout.EndVertical();
        }
        
        private void DrawLeftPanel()
        {
            EditorGUILayout.BeginVertical( GUILayout.Width(350));
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            for (var i = 0; i < elements.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("-", GUILayout.Width(30),GUILayout.Height(100)) && i != 0)
                {
                    elements.RemoveAt(i);
                }
                EditorGUILayout.BeginVertical(GUILayout.Height(100)); // Help box around index
                EditorGUILayout.LabelField(i.ToString(), new GUIStyle(GUI.skin.label) { fontSize = 17, 
                        fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter }, GUILayout.Width(30),GUILayout.Height(100)); // Larger, bold, centered index
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical("box"); // Help box around each element
                elements[i].color = EditorGUILayout.ColorField("Color", elements[i].color);
                elements[i].fontStyle = (FontStyle)EditorGUILayout.EnumPopup("Font Style", elements[i].fontStyle);
                elements[i].useSize = EditorGUILayout.Toggle("Use Size", elements[i].useSize);
                EditorGUI.BeginDisabledGroup(!elements[i].useSize);
                elements[i].size = EditorGUILayout.IntField("Size", elements[i].size);
                EditorGUI.EndDisabledGroup();
                elements[i].underline = EditorGUILayout.Toggle("Underline", elements[i].underline);
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); // Separator between elements
            }
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("+",GUILayout.Height(30)))
            {
                elements.Add(new RichTextElement());
            }
            
            EditorGUILayout.EndVertical();
        }

        private string GetRichText()
        {
            string          richText = text;
            string          pattern  = @"\$(\d+)\{";
            MatchCollection matches  = Regex.Matches(richText, pattern);

            for (var i = matches.Count - 1; i >= 0; i--)
            {
                var indexStyleRich = int.Parse(matches[i].Groups[1].Value);
                if(indexStyleRich < 0 || indexStyleRich >= elements.Count) continue;
                int endIndex       = richText.IndexOf('}', matches[i].Index);
                if(endIndex < 0) continue;
                string innerText     = richText.Substring(matches[i].Index + matches[i].Length, endIndex - (matches[i].Index + matches[i].Length));
                string formattedText = ApplyRichText(elements[indexStyleRich], innerText);
                richText = richText.Substring(0, matches[i].Index) + formattedText + richText.Substring(endIndex + 1);
                // Cập nhật lại chỉ số i
                i = matches.Count - 1 - (matches.Count - 1 - i);
            }
            return richText;
        }
        
        private string ApplyRichText(RichTextElement element, string innerText)
        {
            string richText = "";
            if (element.useSize)
            {
                richText += "<size=" + element.size + ">";
            }
            richText += "<color=" + ColorToHex(element.color) + ">";
            switch (element.fontStyle)
            {
                case FontStyle.Bold:
                    richText += "<b>";
                    break;
                case FontStyle.Italic:
                    richText += "<i>";
                    break;
                case FontStyle.BoldAndItalic:
                    richText += "<b><i>";
                    break;
            }
            if (element.underline)
            {
                richText += "<u>";
            }
            richText += innerText;
            if (element.underline)
            {
                richText += "</u>";
            }
            switch (element.fontStyle)
            {
                case FontStyle.BoldAndItalic:
                    richText += "</i></b>";
                    break;
                case FontStyle.Italic:
                    richText += "</i>";
                    break;
                case FontStyle.Bold:
                    richText += "</b>";
                    break;
            }
            richText += "</color>";
            if (element.useSize)
            {
                richText += "</size>";
            }
            return richText;
        }

        private string ColorToHex(Color color)
        {
            return "#" + ((int)(color.r * 255)).ToString("X2") + ((int)(color.g * 255)).ToString("X2") + ((int)(color.b * 255)).ToString("X2");
        }
    }

    [Serializable]
    public class RichTextElement
    {
        public Color color = Color.white;
        public FontStyle fontStyle = FontStyle.Normal;
        public bool useSize = false;
        public int size = 12;
        public bool underline = false;
    }
}
#endif