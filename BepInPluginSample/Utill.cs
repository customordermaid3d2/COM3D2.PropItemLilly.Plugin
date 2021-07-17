using COM3D2.Lilly.Plugin;
using ShortMenuLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using static SceneEdit;
using static TBody;

namespace BepInPluginSample
{
    class Utill
    {
        static bool isUpdate;

        public static Stopwatch stopwatch = new Stopwatch(); //객체 선언

        public static void MaidChg()
        {

        }

        public static void Count()
        {
            MyLog.LogMessage("texFileIDDic", QuickEdit.texFileIDDic.Count()); // 에딧 진입시 로딩
            MyLog.LogMessage("tex2DDic", QuickEdit.tex2DDic.Count()); // 이게 실시간 로딩
            MyLog.LogMessage("idItemDic", QuickEdit.idItemDic.Count());
            MyLog.LogMessage("AllItemDic", ShortMenuLoader.Main.AllItemDic.Count());
        }

        public static Dictionary<int, List<SMenuItem>> MPNList = new Dictionary<int, List<SMenuItem>>();
        public static string[] namesMPN;
        public static int[] MPNs;
        public static Dictionary<int, Texture[]> iconList = new Dictionary<int, Texture[]>();

        public static void init()
        {
            var list = Enum.GetNames(typeof(SlotID)).ToList();


            Dictionary<MPN, int> list2 = new Dictionary<MPN, int>();

            List<string> names = new List<string>();
            List<int> ints = new List<int>();

            foreach (int item in Enum.GetValues(typeof(MPN)))
            {
                var name=   Enum.GetName(typeof(MPN), item);
                if (list.Contains( name))
                {
                    names.Add(name);
                    ints.Add(item);
                    MPNList.Add(item, new List<SMenuItem>());
                    iconList.Add(item, new Texture[] { });
                }
            }

            namesMPN = names.ToArray();
            MPNs = ints.ToArray();

        }

        public static void QuickEditUpdate()
        {
            MyLog.LogMessage("Lilly", stopwatch.Elapsed);
            stopwatch.Reset();
            stopwatch.Start();

            foreach (var item in Main.AllItemDic.Values)
            {
                /*            
                                if (!m_dicMPNList[(int)__1.m_mpn].Contains(__1))
                                {
                                    m_dicMPNList[(int)__1.m_mpn].Add(__1);
                                }
                */
                foreach (var __1 in item)
                {
                    //if (!QuickEdit.texFileIDDic.ContainsKey(__1.m_nMenuFileRID))
                    //{
                    //    continue;
                    //}
                    if (__1.m_texIcon == null || __1.m_texIcon == Texture2D.whiteTexture)
                    {
                        try
                        {
                            __1.m_texIcon = QuickEdit.GetTexture(__1.m_nMenuFileRID);
                            __1.m_texIconRandomColor = __1.m_texIcon;
                        }
                        catch
                        {
                            __1.m_texIcon = Texture2D.whiteTexture;
                            __1.m_texIconRandomColor = __1.m_texIcon;
                        }
                    }
                }
            }

            foreach (int item in Enum.GetValues(typeof(MPN)))
            {
                //iconList[item] = m_dicMPNList[item].Select(x => x.m_texIcon).ToArray();
                iconList[item] = ShortMenuLoader.Main.AllItemDic[(MPN)item].Select(x => x.m_texIcon).ToArray();
            }

            MyLog.LogMessage("Lilly", stopwatch.Elapsed);

            stopwatch.Reset();
            MyLog.LogMessage("tex2DDic", QuickEdit.tex2DDic.Count()); // 이게 실시간 로딩
        }

        Maid maid;

        public void ClickCallback()
        {
            if (maid.IsBusy)
            {
                return;
            }
        }
    }
}
