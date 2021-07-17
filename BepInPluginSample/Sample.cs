using BepInEx;
using BepInEx.Configuration;
using COM3D2.Lilly.Plugin;
using COM3D2API;
using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BepInPluginSample
{
    [BepInPlugin(MyAttribute.PLAGIN_FULL_NAME, MyAttribute.PLAGIN_FULL_NAME, MyAttribute.PLAGIN_VERSION)]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    //[BepInPlugin("COM3D2.Sample.Plugin", "COM3D2.Sample.Plugin", "21.6.6")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    [BepInProcess("COM3D2x64.exe")]
    public class Sample : BaseUnityPlugin
    {
        // 단축키 설정파일로 연동
       // private ConfigEntry<BepInEx.Configuration.KeyboardShortcut> ShowCounter;

        public static Sample sample;

        public static Stopwatch stopwatch = new Stopwatch(); //객체 선언

        Harmony harmony;

        public Sample()
        {
            sample = this;
        }

        /// <summary>
        ///  게임 실행시 한번만 실행됨
        /// </summary>
        public void Awake()
        {
            MyLog.LogMessage("Awake");

            // 단축키 기본값 설정
           // ShowCounter = Config.Bind("KeyboardShortcut", "KeyboardShortcut0", new BepInEx.Configuration.KeyboardShortcut(KeyCode.Alpha9, KeyCode.LeftControl));

           // SampleConfig.Install(MyLog.log);

            Utill.init();
        }



        public void OnEnable()
        {
            MyLog.LogMessage("OnEnable");

            SceneManager.sceneLoaded += this.OnSceneLoaded;


            harmony = Harmony.CreateAndPatchAll(typeof(SamplePatch));
        }

        /// <summary>
        /// 게임 실행시 한번만 실행됨
        /// </summary>
        public void Start()
        {
            MyLog.LogMessage("Start");

            SampleGUI.Install(gameObject, Config);

           // SystemShortcutAPI.AddButton(MyAttribute.PLAGIN_FULL_NAME, new Action(delegate () { enabled = !enabled; }), MyAttribute.PLAGIN_NAME, MyUtill.ExtractResource(BepInPluginSample.Properties.Resources.icon));
        }

        public string scene_name = string.Empty;

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            MyLog.LogMessage("OnSceneLoaded", scene.name, scene.buildIndex);
            //  scene.buildIndex 는 쓰지 말자 제발
            scene_name = scene.name;
        }

        public void FixedUpdate()
        {

        }

        public void Update()
        {
            //if (ShowCounter.Value.IsDown())
            //{
            //    MyLog.LogMessage("IsDown", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
            //if (ShowCounter.Value.IsPressed())
            //{
            //    MyLog.LogMessage("IsPressed", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
            //if (ShowCounter.Value.IsUp())
            //{
            //    MyLog.LogMessage("IsUp", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
        }

        public void LateUpdate()
        {

        }

        

        public void OnGUI()
        {
          
        }



        public void OnDisable()
        {
            MyLog.LogMessage("OnDisable");

            SceneManager.sceneLoaded -= this.OnSceneLoaded;

            harmony.UnpatchSelf();// ==harmony.UnpatchAll(harmony.Id);
        }

        public void Pause()
        {
            MyLog.LogMessage("Pause");
        }

        public void Resume()
        {
            MyLog.LogMessage("Resume");
        }

        /// <summary>
        /// 게임 X 버튼 눌렀을때 반응
        /// </summary>
        public void OnApplicationQuit()
        {
            MyLog.LogMessage("OnApplicationQuit");
        }

    }
}
