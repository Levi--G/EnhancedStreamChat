﻿//using EnhancedTwitchChat.Bot;
using IPA.Old;
using StreamCore.SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using StreamCore.Config;
using StreamCore;
using System.Threading.Tasks;
using System.Threading;

namespace EnhancedStreamChat.Config
{
    public class OldConfigOptions
    {
        public string TwitchChannel = "";
    }

    public class OldBlacklistOption
    {
        public string SongBlacklist;
    }

    public class SemiOldConfigOptions
    {
        public string TwitchChannelName = "";
        public string TwitchUsername = "";
        public string TwitchOAuthToken = "";
        public bool SongRequestBot = false;
        public bool PersistentRequestQueue = true;
    }
    
    public class ChatConfig
    {
        private string FilePath = Path.Combine(Globals.DataPath, $"{Plugin.ModuleName.Replace(" ", "")}.ini");


        public string FontName = "Segoe UI";
        //public int BombBitValue;
        //public int TwitchBitBalance;

        public float ChatScale = 1.1f;
        public float ChatWidth = 160;
        public float LineSpacing = 2.0f;
        public int MaxChatLines = 30;
        
        public float PositionX = 0;
        public float PositionY = 2.6f;
        public float PositionZ = 2.3f;

        public float RotationX = -30;
        public float RotationY = 0;
        public float RotationZ = 0;

        public float TextColorR = 1;
        public float TextColorG = 1;
        public float TextColorB = 1;
        public float TextColorA = 1;

        public float BackgroundColorR = 0;
        public float BackgroundColorG = 0;
        public float BackgroundColorB = 0;
        public float BackgroundColorA = 0.6f;
        public float BackgroundPadding = 4;

        public bool AnimatedEmotes = true;
        public bool ClearChatEnabled = true;
        public bool ClearTimedOutMessages = true;
        public bool DrawShadows = false;
        public bool LockChatPosition = false;
        public bool ReverseChatOrder = false;
        public bool ShowBTTVEmotes = true;
        public bool ShowFFZEmotes = true;

        public bool FilterCommandMessages = false;
        public bool FilterBroadcasterMessages = false;
        public bool FilterSelfMessages = false;

      
        public event Action<ChatConfig> ConfigChangedEvent;

        private readonly FileSystemWatcher _configWatcher;
        private bool _saving;

        private static ChatConfig _instance = null;
        public static ChatConfig Instance {
            get
            {
                if (_instance == null)
                    _instance = new ChatConfig();
                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        
        public Color TextColor
        {
            get
            {
                return new Color(TextColorR, TextColorG, TextColorB, TextColorA);
            }
            set
            {
                TextColorR = value.r;
                TextColorG = value.g;
                TextColorB = value.b;
                TextColorA = value.a;
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return new Color(BackgroundColorR, BackgroundColorG, BackgroundColorB, BackgroundColorA);
            }
            set
            {
                BackgroundColorR = value.r;
                BackgroundColorG = value.g;
                BackgroundColorB = value.b;
                BackgroundColorA = value.a;
            }
        }

        public Vector3 ChatPosition
        {
            get
            {
                return new Vector3(PositionX, PositionY, PositionZ);
            }
            set
            {
                PositionX = value.x;
                PositionY = value.y;
                PositionZ = value.z;
            }
        }

        public Vector3 ChatRotation
        {
            get { return new Vector3(RotationX, RotationY, RotationZ); }
            set
            {
                RotationX = value.x;
                RotationY = value.y;
                RotationZ = value.z;
            }
        }

        public ChatConfig()
        {
            Instance = this;
            _configWatcher = new FileSystemWatcher();
            Task.Run(() =>
            {
                while (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                    Thread.Sleep(100);

                Plugin.Log("FilePath exists! Continuing initialization!");

                string oldFilePath = Path.Combine(Environment.CurrentDirectory, "UserData", "EnhancedTwitchChat.ini");
                string newerFilePath = Path.Combine(Globals.DataPath, "EnhancedTwitchChat.ini");
                if (File.Exists(newerFilePath))
                {
                    // Append the data to the blacklist, if any blacklist info exists, then dispose of the old config file.
                    AppendToBlacklist(newerFilePath);
                    if (!File.Exists(FilePath))
                        File.Move(newerFilePath, FilePath);
                    else
                        File.Delete(newerFilePath);
                }
                else if (File.Exists(oldFilePath))
                {
                    // Append the data to the blacklist, if any blacklist info exists, then dispose of the old config file.
                    AppendToBlacklist(oldFilePath);
                    if (!File.Exists(FilePath))
                        File.Move(oldFilePath, FilePath);
                    else
                        File.Delete(oldFilePath);
                }

                if (File.Exists(FilePath))
                {
                    Load();

                    var text = File.ReadAllText(FilePath);
                    if (text.Contains("TwitchUsername="))
                    {
                        SemiOldConfigOptions semiOldConfigInfo = new SemiOldConfigOptions();
                        ObjectSerializer.Load(semiOldConfigInfo, FilePath);

                        TwitchLoginConfig.Instance.TwitchChannelName = semiOldConfigInfo.TwitchChannelName;
                        TwitchLoginConfig.Instance.TwitchUsername = semiOldConfigInfo.TwitchUsername;
                        TwitchLoginConfig.Instance.TwitchOAuthToken = semiOldConfigInfo.TwitchOAuthToken;
                        TwitchLoginConfig.Instance.Save(true);
                    }
                }
                Save();

                _configWatcher.Path = Path.GetDirectoryName(FilePath);
                _configWatcher.NotifyFilter = NotifyFilters.LastWrite;
                _configWatcher.Filter = $"{Plugin.ModuleName.Replace(" ", "")}.ini";
                _configWatcher.EnableRaisingEvents = true;

                _configWatcher.Changed += ConfigWatcherOnChanged;
            });
        }

        ~ChatConfig()
        {
            _configWatcher.Changed -= ConfigWatcherOnChanged;
        }

        public void Load()
        {
            ObjectSerializer.Load(this, FilePath);

            CorrectConfigSettings();
        }

        public void Save(bool callback = false)
        {
            if (!callback)
                _saving = true;

            ObjectSerializer.Save(this, FilePath);
        }

        private void AppendToBlacklist(string path)
        {
            string text = File.ReadAllText(path);

            if (text.Contains("SongBlacklist="))
            {
                var oldConfig = new OldBlacklistOption();
                ObjectSerializer.Load(oldConfig, path);

                if (oldConfig.SongBlacklist.Length > 0)
                    File.AppendAllText(Path.Combine(Globals.DataPath, "SongBlacklistMigration.list"), oldConfig.SongBlacklist + ",");
            }
        }
        
        private void CorrectConfigSettings()
        {
            if (BackgroundPadding < 0)
                BackgroundPadding = 0;
            if (MaxChatLines < 1)
                MaxChatLines = 1;
        }

        private void ConfigWatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            if (_saving)
            {
                _saving = false;
                return;
            }

            Load();
            ConfigChangedEvent?.Invoke(this);
        }
    }
}