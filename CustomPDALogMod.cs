using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;
using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using BepInEx;

[BepInPlugin("tdcdev.custompdalogmod", "Custom PDA Logs Mod", "1.0.0")]
[BepInProcess("Subnautica.exe")]
public class CustomPDALogsMod : BaseUnityPlugin
{
    private string logsPath;

    void Awake()
    {
        logsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "logs");
        if (!Directory.Exists(logsPath))
            Directory.CreateDirectory(logsPath);

        string[] jsonFiles = Directory.GetFiles(logsPath, "*.json");
        Logger.LogInfo($"[CustomPDALogsMod] {jsonFiles.Length} JSON dosyasý bulundu.");

        foreach (var file in jsonFiles)
        {
            try
            {
                string jsonText = File.ReadAllText(file);
                PDALog logData = JsonConvert.DeserializeObject<PDALog>(jsonText);

                if (logData != null)
                    AddLogToPDA(logData);
            }
            catch (Exception ex)
            {
                Logger.LogError($"[CustomPDALogsMod] JSON iþlenirken hata: {file}\n{ex}");
            }
        }
    }

    private void AddLogToPDA(PDALog log)
    {
        // Buraya PDA log ekleme iþlemi yazýlacak
    }

    public class PDALog
    {
        public string id { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string modelPath { get; set; }
        public bool unlockOnStart { get; set; } = false;
    }
}
