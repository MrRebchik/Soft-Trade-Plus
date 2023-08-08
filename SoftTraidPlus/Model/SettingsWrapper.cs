using System;
using System.IO;
using Newtonsoft.Json;
using SoftTradePlus.Model.Data;

namespace SoftTradePlus.Model
{
    internal class SettingsWrapper : IDisposable
    {
        private SettingsMemento _memento;
        private bool _initialized;
        private string _settingsFilePath;

        public SettingsWrapper()
        {
            _memento = new SettingsMemento();
        }
        
        public bool IsAuthorized
        {
            get
            {
                EnsureInitialized();
                return _memento.IsAuthorized;
            }
            set
            {
                EnsureInitialized();
                _memento.IsAuthorized = value;
            }
        }
        public Manager? CurrentUser 
        {
            get
            {
                EnsureInitialized();
                return _memento.CurrentUser;
            }
            set
            {
                EnsureInitialized();
                _memento.CurrentUser = value;
            }
        }
        public void Save()
        {
            var serializedMemento = JsonConvert.SerializeObject(_memento);
            File.WriteAllText(_settingsFilePath, serializedMemento);
        }
        public void Dispose()
        {
            EnsureInitialized();
            Save();
        }

        public void Initialize()
        {
            if (_initialized)
                throw new InvalidOperationException("SettingsWrapper is already initialized");

            _initialized = true;

            var localApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            const string company = "SoftTradePlus";
            const string settingsFolderName = "Settings";

            var settingsPath = Path.Combine(localApplicationDataPath, company, settingsFolderName);
            _settingsFilePath = Path.Combine(settingsPath, "SettingsMemento.json");

            Directory.CreateDirectory(settingsPath);

            if (!File.Exists(_settingsFilePath))
                return;

            var serializedMemento = File.ReadAllText(_settingsFilePath);

            _memento = JsonConvert.DeserializeObject<SettingsMemento>(serializedMemento);
        }
        private void EnsureInitialized()
        {
            if (!_initialized)
                throw new InvalidOperationException("SettingsWrapper is not initialized");
        }
    }
}
