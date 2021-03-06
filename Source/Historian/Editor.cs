/**
* This file is part of Historian.
* 
* Historian is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
* 
* Historian is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
* 
* You should have received a copy of the GNU General Public License
* along with Historian. If not, see <http://www.gnu.org/licenses/>.
**/
using UnityEngine;

namespace KSEA.Historian
{
    public class Editor
    {
        bool isOpen = false;
        static LauncherButton appLauncherButton = new LauncherButton();
        ToolbarButton toolbarButton = null;
        Rect position;
        int windowId;
        Texture nextButtonTexture = null;
        Texture previousButtonTexture = null;
        bool enableLauncherButton = true;
        bool enableToolberButton = true;
        

        public Editor(Configuration configuration)
        {
            //m_LauncherButton = new LauncherButton();
            toolbarButton = new ToolbarButton();

            var windowHeight = 580f;
            var windowWidth = 900f;
            //if (GameSettings.KERBIN_TIME)
            //    windowHeight += 100; // add extra height for Kerbin month/day name fields

            position = new Rect(0.5f * Screen.width - windowWidth / 2, 0.5f * Screen.height - windowHeight / 2, windowWidth, windowHeight);
            windowId = (new System.Random()).Next(876543210, 987654321); // 9 digit random number

            nextButtonTexture = GameDatabase.Instance.GetTexture("KSEA/Historian/Historian_Button_Next", false);
            previousButtonTexture = GameDatabase.Instance.GetTexture("KSEA/Historian/Historian_Button_Previous", false);

            enableLauncherButton = configuration.EnableLauncherButton;
            enableToolberButton = configuration.EnableToolbarButton;

            if (enableLauncherButton)
            {
                appLauncherButton.Click += Button_Click;

                if (!appLauncherButton.IsRegistered)
                    appLauncherButton.Register();
            }

            if (ToolbarManager.ToolbarAvailable && enableToolberButton)
            {
                toolbarButton.OnTrue += Toggle;
                toolbarButton.OnFalse += Toggle;
                toolbarButton.OnAlternateClick += Button_OnAlternateClick;

                toolbarButton.Register();
            }
        }

        public void Draw()
        {
            if (isOpen)
            {
                position = GUI.Window(windowId, position, OnWindowGUI, $"Historian: v {Historian.Instance.AssemblyFileVersion}", HighLogic.Skin.window);

                if (enableLauncherButton)
                {
                    appLauncherButton.Update();
                }

                if (enableToolberButton && ToolbarManager.ToolbarAvailable)
                {
                    toolbarButton.Update();
                }
            }
        }

        void OnWindowGUI(int id)
        {
            GUI.skin = HighLogic.Skin;
            var historian = Historian.Instance;
            var configuration = historian.GetConfiguration();

            // column one
            using (var columnOne = new GUILayout.AreaScope(new Rect(15, 20, 380, 550)))
            {
                using (var col = new GUILayout.VerticalScope())
                {
                    GUILayout.Space(20);
                    historian.Suppressed = GUILayout.Toggle(historian.Suppressed, "Suppressed");
                    historian.AlwaysActive = GUILayout.Toggle(historian.AlwaysActive, "Always Active");
                    historian.AutoHideUI = GUILayout.Toggle(historian.AutoHideUI, "Auto hide UI");
                    configuration.AutoHideUI = historian.AutoHideUI;

                    configuration.PersistentConfigurationWindow = GUILayout.Toggle(configuration.PersistentConfigurationWindow, "Always Display Configuration Window");
                    enableLauncherButton = GUILayout.Toggle(enableLauncherButton, "Use Stock Launcher");
                    enableToolberButton = GUILayout.Toggle(enableToolberButton, "Use Blizzy's Toolbar");
                    GUILayout.Space(10);

                    using (var layout = new GUILayout.HorizontalScope())
                    {
                        var rightClickOptionsCount = 4;
                        //GUILayout.Space(40);
                        GUILayout.Label("Right click action");
                        GUILayout.Space(10);
                        if (GUILayout.Button(previousButtonTexture, GUILayout.Width(20), GUILayout.Height(GUI.skin.label.lineHeight)))
                        {
                            configuration.RightClickAction = (RightClickAction)Mathf.Clamp((int)configuration.RightClickAction - 1, 0, rightClickOptionsCount - 1);
                        }
                        else if (GUILayout.Button(nextButtonTexture, GUILayout.Width(20), GUILayout.Height(GUI.skin.label.lineHeight)))
                        {
                            configuration.RightClickAction = (RightClickAction)Mathf.Clamp((int)configuration.RightClickAction + 1, 0, rightClickOptionsCount - 1);
                        }
                        GUILayout.Space(5);
                        GUILayout.Label(configuration.RightClickAction.ToString(), GUI.skin.textArea, GUILayout.ExpandWidth(true));
                    }

                    ManageButtons();

                    GUILayout.Space(10);
                    using (var layout = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Layout");
                        GUILayout.Space(10);
                        var layouts = historian.GetLayoutNames();
                        if (GUILayout.Button(previousButtonTexture, GUILayout.Width(20), GUILayout.Height(GUI.skin.label.lineHeight)))
                        {
                            historian.CurrentLayoutIndex = Mathf.Clamp(historian.CurrentLayoutIndex - 1, 0, layouts.Length - 1);
                        }
                        else if (GUILayout.Button(nextButtonTexture, GUILayout.Width(20), GUILayout.Height(GUI.skin.label.lineHeight)))
                        {
                            historian.CurrentLayoutIndex = Mathf.Clamp(historian.CurrentLayoutIndex + 1, 0, layouts.Length - 1);
                        }
                        GUILayout.Space(5);
                        GUILayout.Label(historian.GetCurrentLayoutName(), GUI.skin.textArea, GUILayout.ExpandWidth(true));
                    }

                    GUILayout.Space(10);
                    using (var customHead = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Custom Text:");
                        GUILayout.FlexibleSpace();
                        configuration.PersistentCustomText = GUILayout.Toggle(configuration.PersistentCustomText, "Persistent", GUILayout.Width(120));
                    }
                    configuration.CustomText = GUILayout.TextArea(configuration.CustomText, GUI.skin.textArea, GUILayout.Height(60));

                    GUILayout.Space(10);
                    using (var spaceCentre = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Default Space Center Name:");
                        GUILayout.FlexibleSpace();
                        configuration.DefaultSpaceCenterName = GUILayout.TextField(configuration.DefaultSpaceCenterName, GUI.skin.textArea, GUILayout.Width(150));
                    }


                    GUILayout.Space(10);
                    GUILayout.Label($"Time to remember action key press: {configuration.TimeToRememberLastAction} ms");
                    configuration.TimeToRememberLastAction = (int)GUILayout.HorizontalSlider(configuration.TimeToRememberLastAction, 250, 10000, GUILayout.ExpandWidth(true));

                }
            }

            // column two
            using (var columnTwo = new GUILayout.AreaScope(new Rect(410, 20, 220, 500)))
            {
                using (var col = new GUILayout.VerticalScope())
                {
                    GUILayout.Space(20);
                    GUILayout.Label("Kerbin calendar day names:");
                    for (int i = 0; i < configuration.KerbinDayNames.Length; i++)
                    {
                        using (var item = new GUILayout.HorizontalScope())
                        {
                            GUILayout.Label($"{i + 1}:");
                            GUILayout.FlexibleSpace();
                            configuration.KerbinDayNames[i] = GUILayout.TextField(configuration.KerbinDayNames[i], GUI.skin.textArea, GUILayout.Width(190f));
                        }
                    }

                    GUILayout.Space(50);
                    GUILayout.Label("Default empty crew slot labels:");
                    GUILayout.Space(10);
                    using (var noCrewLabel = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Crewed:");
                        GUILayout.FlexibleSpace();
                        configuration.DefaultNoCrewLabel = GUILayout.TextField(configuration.DefaultNoCrewLabel, GUI.skin.textArea, GUILayout.Width(120));
                    }
                    using (var noCrewLabel = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Uncrewed:");
                        GUILayout.FlexibleSpace();
                        configuration.DefaultUnmannedLabel = GUILayout.TextField(configuration.DefaultUnmannedLabel, GUI.skin.textArea, GUILayout.Width(120));
                    }

                }
            }

            // column three
            using (var columnThree = new GUILayout.AreaScope(new Rect(660, 20, 220, 480)))
            {
                using (var col = new GUILayout.VerticalScope())
                {
                    GUILayout.Space(20);
                    GUILayout.Label("Kerbin calendar month names:");
                    for (int i = 0; i < configuration.KerbinMonthNames.Length; i++)
                    {
                        using (var item = new GUILayout.HorizontalScope())
                        {
                            GUILayout.Label($"{i + 1}:");
                            GUILayout.FlexibleSpace();
                            configuration.KerbinMonthNames[i] = GUILayout.TextField(configuration.KerbinMonthNames[i], GUI.skin.textArea, GUILayout.Width(190f));
                        }
                    }
                }
            }

            // bottom bar
            using (var buttonBar = new GUILayout.AreaScope(new Rect(5, 525, 890, 30)))
            {
                using (var layout = new GUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Load", GUILayout.Width(100.0f)))
                    {
                        historian.Reload();
                    }
                    if (GUILayout.Button("Save", GUILayout.Width(100.0f)))
                    {
                        configuration.Layout = historian.GetCurrentLayoutName();
                        configuration.EnableLauncherButton = enableLauncherButton;
                        configuration.EnableToolbarButton = enableToolberButton;

                        historian.SetConfiguration(configuration);
                        if (!configuration.PersistentConfigurationWindow) Toggle();
                    }
                    GUILayout.Space(20);
                    // GUILayout.FlexibleSpace();
                }
            }

            GUI.DragWindow();
        }

        void ManageButtons()
        {
            if (enableLauncherButton && !appLauncherButton.IsRegistered)
            {
                appLauncherButton.Click += Button_Click;
                appLauncherButton.Register();
            }
            else if (!enableLauncherButton && appLauncherButton.IsRegistered)
            {
                appLauncherButton.Click -= Button_Click;
                appLauncherButton.Unregister();
            }

            if (enableToolberButton && ToolbarManager.ToolbarAvailable && !toolbarButton.IsRegistered)
            {
                toolbarButton.OnTrue += Button_Click;
                toolbarButton.OnFalse += Button_Click;

                toolbarButton.SetState(isOpen);

                toolbarButton.Register();
            }
            else if (!enableToolberButton && ToolbarManager.ToolbarAvailable && toolbarButton.IsRegistered)
            {
                toolbarButton.OnTrue -= Button_Click;
                toolbarButton.OnFalse -= Button_Click;

                toolbarButton.SetState(isOpen);

                toolbarButton.Unregister();
            }
        }

        internal void RemoveButton() => appLauncherButton.Unregister();

        void Button_Click() => Toggle();

        void Toggle() => isOpen = !isOpen;

        void Button_OnAlternateClick()
        {
            switch (Historian.Instance.GetConfiguration().RightClickAction)
            {
                case RightClickAction.None:
                    break;
                case RightClickAction.Suppress:
                    Historian.Instance.Suppressed = !Historian.Instance.Suppressed;
                    break;
                case RightClickAction.AlwaysActive:
                    Historian.Instance.AlwaysActive = !Historian.Instance.AlwaysActive;
                    break;
                case RightClickAction.AutoHideUI:
                    Historian.Instance.AutoHideUI = !Historian.Instance.AutoHideUI;
                    break;
                default:
                    break;
            }
            
        } 
    }
}