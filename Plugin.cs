using System;
using System.IO;
using System.Reflection;
using BepInEx;
using lemonFrame;
using Photon.Voice;
using Unity.Mathematics;
using UnityEngine;
using Utilla;

namespace lemonFrame
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {


        public bool active;
        bool inRoom;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            AssetObj.SetActive(true);

            active = true;

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            AssetObj.SetActive(false);

            active = false;

            HarmonyPatches.RemoveHarmonyPatches();
        }
        public GameObject AssetObj;
        void OnGameInitialized(object sender, EventArgs e)
        {
            var assetBundle = LoadAssetBundle("lemonFrame.lemon");
            GameObject Obj = assetBundle.LoadAsset<GameObject>("AssetObject");

            AssetObj = Instantiate(Obj);
            AssetObj.transform.position = new Vector3(-68.8294f, 12.382f, - 84.1987f);
            AssetObj.transform.rotation = Quaternion.Euler(270.0198f, 138.9622f, 0f);
            AssetObj.layer = 8;
        }

        AssetBundle LoadAssetBundle(string path)
        {
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
                AssetBundle bundle = AssetBundle.LoadFromStream(stream);
                stream.Close();
                Debug.Log("[" + PluginInfo.GUID + "] Success loading asset bundle");
                return bundle;
            }
            catch (Exception e)
            {
                Debug.Log("[" + PluginInfo.Name + "] Error loading asset bundle: " + e.Message + " " + path);
                throw;
            }
        }
    }
}