using BepInEx;
using BepInEx.Configuration;
using COM3D2.Lilly.Plugin;
using COM3D2API;
using ShortMenuLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BepInPluginSample
{
    class SampleGUI : MonoBehaviour
    {
        public static SampleGUI instance;

        private static ConfigFile config;

        private static ConfigEntry<BepInEx.Configuration.KeyboardShortcut> ShowCounter;

        private static int windowId = new System.Random().Next();
        private static int windowId2 = new System.Random().Next();

        private static Vector2 scrollPosition;
        private int selectedItembak;
        public static int selectedMPN;
        public static string selectedName;

        // 위치 저장용 테스트 json
        public static MyWindowRect myWindowRect;
        public static MyWindowRect myWindowRect2;
        public static MyWindowRect myWindowRect3;

        public static bool IsOpen
        {
            get => myWindowRect.IsOpen;
            set => myWindowRect.IsOpen = value;
        }

        public static bool IsOpen2
        {
            get => myWindowRect2.IsOpen;
            set => myWindowRect2.IsOpen = value;
        }

        public static bool IsOpen3
        {
            get => myWindowRect3.IsOpen;
            set => myWindowRect3.IsOpen = value;
        }

        // GUI ON OFF 설정파일로 저장
        private static ConfigEntry<bool> IsGUIOn;

        public static bool isGUIOn
        {
            get => IsGUIOn.Value;
            set => IsGUIOn.Value = value;
        }

        // GUI ON OFF 설정파일로 저장
        private static ConfigEntry<bool> IsGUIOn2;
        private static ConfigEntry<bool> IsGUIOn3;

        private int selectedItem;
        private int selectedMPNBak;
        private Vector2 scrollPosition2;
        private Vector2 scrollPosition3;
        private int seletedMaidbak;
        private int seletedMaid;

        public static bool isGUIOn2
        {
            get => IsGUIOn2.Value;
            set => IsGUIOn2.Value = value;
        }

        public static bool isGUIOn3
        {
            get => IsGUIOn3.Value;
            set => IsGUIOn3.Value = value;
        }

        internal static SampleGUI Install(GameObject parent, ConfigFile config)
        {
            SampleGUI.config = config;
            instance = parent.GetComponent<SampleGUI>();
            if (instance == null)
            {
                instance = parent.AddComponent<SampleGUI>();
                MyLog.LogMessage("GameObjectMgr.Install", instance.name);
            }
            return instance;
        }

        public void Awake()
        {
            myWindowRect = new MyWindowRect(config, MyAttribute.PLAGIN_FULL_NAME + "-list", wo: 200f);
            myWindowRect2 = new MyWindowRect(config, MyAttribute.PLAGIN_FULL_NAME + "-item", wo: 600f+10+20);// side , slide
            myWindowRect3 = new MyWindowRect(config, MyAttribute.PLAGIN_FULL_NAME + "-maid", wo: 200f);
            IsGUIOn = config.Bind("GUI", "isGUIOn", false);
            IsGUIOn2 = config.Bind("GUI", "isGUIOn2", false);
            IsGUIOn3 = config.Bind("GUI", "isGUIOn3", false);
            ShowCounter = config.Bind("GUI", "isGUIOnKey", new BepInEx.Configuration.KeyboardShortcut(KeyCode.Alpha9, KeyCode.LeftControl));
            SystemShortcutAPI.AddButton(MyAttribute.PLAGIN_FULL_NAME, new Action(delegate () { SampleGUI.isGUIOn2 = SampleGUI.isGUIOn = !SampleGUI.isGUIOn; }), MyAttribute.PLAGIN_NAME + " : " + ShowCounter.Value.ToString(), MyUtill.ExtractResource(BepInPluginSample.Properties.Resources.icon));
        }

        public void OnEnable()
        {
            MyLog.LogMessage("OnEnable");

            SampleGUI.myWindowRect.load();
            SampleGUI.myWindowRect2.load();
            SampleGUI.myWindowRect3.load();
            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }

        public void Start()
        {
            MyLog.LogMessage("Start");
            itemStyle = new GUIStyle(GUI.skin.button);
            itemStyle.fixedWidth=80;
            itemStyle.fixedHeight=80;
        }

        static GUIStyle itemStyle;

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SampleGUI.myWindowRect.save();
            SampleGUI.myWindowRect2.save();
            SampleGUI.myWindowRect3.save();
        }

        private void Update()
        {
            //if (ShowCounter.Value.IsDown())
            //{
            //    MyLog.LogMessage("IsDown", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
            //if (ShowCounter.Value.IsPressed())
            //{
            //    MyLog.LogMessage("IsPressed", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
            if (ShowCounter.Value.IsUp())
            {
                isGUIOn = !isGUIOn;
                MyLog.LogMessage("IsUp", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            }
        }

        public void OnGUI()
        {
            if (!isGUIOn)
                return;

            //GUI.skin.window = ;

            //myWindowRect.WindowRect = GUILayout.Window(windowId, myWindowRect.WindowRect, WindowFunction, MyAttribute.PLAGIN_NAME + " " + ShowCounter.Value.ToString(), GUI.skin.box);
            //GUI.skin.box.
            myWindowRect.WindowRect = GUILayout.Window(windowId, myWindowRect.WindowRect, WindowFunction, "", GUI.skin.box);

            myWindowRect2.WindowRect = GUILayout.Window(windowId + 1, myWindowRect2.WindowRect, WindowFunction2, "", GUI.skin.box);

            myWindowRect3.WindowRect = GUILayout.Window(windowId + 2, myWindowRect3.WindowRect, WindowFunction3, "", GUI.skin.box);

        }

        public void WindowFunction(int id)
        {
            TopMenu(MyAttribute.PLAGIN_NAME, IsGUIOn, myWindowRect);

            if (!IsOpen)
            {

            }
            else
            {
                if (SceneEdit.Instance == null)
                {
                    GUILayout.Label("SceneEdit null");
                }

                scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                //GUILayout.BeginHorizontal();
                //GUILayout.EndHorizontal();

                if (GUILayout.Button("count"))
                {
                    Utill.Count();
                }
                /*
                if (GUILayout.Button("SceneEdit"))
                {
                    Utill.GetComponentSceneEdit();
                }
                */
                if (GUILayout.Button("update"))
                {
                    Utill.QuickEditUpdate();
                }

                if (GUILayout.Button("open"))
                {
                    Utill.QuickEditUpdate();
                }
                                
                GUI.enabled = SamplePatch.maids[seletedMaid] != null;
                GUILayout.Label("MPN List");
                selectedMPNBak = GUILayout.SelectionGrid(selectedMPN, Utill.namesMPN, 1);

                GUILayout.EndScrollView();

                if (GUI.changed)
                {
                    if (selectedMPNBak != selectedMPN)
                    {
                        selectedName = Utill.namesMPN[selectedMPNBak];
                        GUI.BringWindowToFront(windowId + 1);
                        MyLog.LogMessage("OnGUI.changed", selectedMPNBak);
                        selectedMPN = selectedMPNBak;
                        int c = Utill.iconList[Utill.MPNs[selectedMPN]].Length / 10;
                        if (c*10== Utill.iconList[Utill.MPNs[selectedMPN]].Length)
                        {
                            h=c *  60;
                        }
                        else
                        {
                            h=(c+1) *  60;
                        }
                    }
                }

            }
            GUI.enabled = true;
            GUI.DragWindow(); // 창 드레그 가능하게 해줌. 마지막에만 넣어야함
        }

        static int w= 600, h;

        public void WindowFunction2(int id)
        {
            TopMenu(MyAttribute.PLAGIN_NAME + " " + selectedName, IsGUIOn2, myWindowRect2);

            if (!IsOpen2 || Utill.iconList[Utill.MPNs[selectedMPN]].Length == 0)
            {

            }
            else
            {
                scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);

                selectedItembak = GUILayout.SelectionGrid(selectedItem, Utill.iconList[Utill.MPNs[selectedMPN]], 10,  GUILayout.Width(w), GUILayout.Height(h));

                GUILayout.EndScrollView();

                if (GUI.changed)
                {
                    if (selectedItembak!= selectedItem)
                    {
                        MyLog.LogMessage("OnGUI.changed2", selectedItembak);
                        selectedItem = selectedItembak;


                    }

                }
            }
            GUI.enabled = true;
            GUI.DragWindow(); // 창 드레그 가능하게 해줌. 마지막에만 넣어야함
        }


        public void WindowFunction3(int id)
        {
            TopMenu(MyAttribute.PLAGIN_NAME, IsGUIOn3, myWindowRect3);

            if (!IsOpen3)
            {

            }
            else
            {
                scrollPosition3 = GUILayout.BeginScrollView(scrollPosition3);

                seletedMaidbak = GUILayout.SelectionGrid(seletedMaid, SamplePatch.maidNames, 1);

                GUILayout.EndScrollView();

                if (GUI.changed)
                {
                    MyLog.LogMessage("OnGUI.changed3", seletedMaidbak);
                    seletedMaid = seletedMaidbak;
                }
            }
            GUI.enabled = true;
            GUI.DragWindow(); // 창 드레그 가능하게 해줌. 마지막에만 넣어야함
        }


        private static void TopMenu(string title, ConfigEntry<bool> isOff, MyWindowRect isOpen)
        {
            GUI.enabled = true;
            GUILayout.BeginHorizontal();
            GUILayout.Label(title, GUILayout.Height(20));
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("P", GUILayout.Width(20), GUILayout.Height(20))) { isGUIOn = !isGUIOn; }//IsOpen = !IsOpen; 
            if (GUILayout.Button("I", GUILayout.Width(20), GUILayout.Height(20))) { isGUIOn2 = !isGUIOn2; }//IsOpen = !IsOpen; 
            if (GUILayout.Button("M", GUILayout.Width(20), GUILayout.Height(20))) { isGUIOn3 = !isGUIOn3; }//IsOpen = !IsOpen; 
            if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(20))) { isOpen.IsOpen = !isOpen.IsOpen; }// isGUIOn = false; 
            if (GUILayout.Button("x", GUILayout.Width(20), GUILayout.Height(20))) { isOff.Value = false; }// isGUIOn = false; 
            GUILayout.EndHorizontal();
        }

        public void OnDisable()
        {
            SampleGUI.myWindowRect.save();
            SampleGUI.myWindowRect2.save();
            SampleGUI.myWindowRect3.save();
            SceneManager.sceneLoaded -= this.OnSceneLoaded;
        }




    }
}
