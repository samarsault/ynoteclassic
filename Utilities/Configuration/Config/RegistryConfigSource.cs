#region Copyright

//
// Nini Configuration Project.
// Copyright (C) 2006 Brent R. Matzelle.  All rights reserved.
//
// This software is published under the terms of the MIT X11 license, a copy of
// which has been included with this distribution in the LICENSE.txt file.
//

#endregion Copyright

using Microsoft.Win32;
using System;

namespace Nini.Config
{
    #region RegistryRecurse enumeration

    /// <include file='RegistryConfigSource.xml' path='//Enum[@name="RegistryRecurse"]/docs/*' />
    public enum RegistryRecurse
    {
        /// <include file='RegistryConfigSource.xml' path='//Enum[@name="RegistryRecurse"]/Value[@name="None"]/docs/*' />
        None,

        /// <include file='RegistryConfigSource.xml' path='//Enum[@name="RegistryRecurse"]/Value[@name="Flattened"]/docs/*' />
        Flattened,

        /// <include file='RegistryConfigSource.xml' path='//Enum[@name="RegistryRecurse"]/Value[@name="Namespacing"]/docs/*' />
        Namespacing
    }

    #endregion RegistryRecurse enumeration

    /// <include file='RegistryConfigSource.xml' path='//Class[@name="RegistryConfigSource"]/docs/*' />
    public class RegistryConfigSource : ConfigSourceBase
    {
        #region Private variables

        private RegistryKey defaultKey = null;

        #endregion Private variables

        #region Public properties

        /// <include file='RegistryConfigSource.xml' path='//Property[@name="DefaultKey"]/docs/*' />
        public RegistryKey DefaultKey
        {
            get { return defaultKey; }
            set { defaultKey = value; }
        }

        #endregion Public properties

        #region Constructors

        #endregion Constructors

        #region Public methods

        /// <include file='RegistryConfigSource.xml' path='//Method[@name="AddConfig"]/docs/*' />
        public override IConfig AddConfig(string name)
        {
            if (DefaultKey == null)
            {
                throw new ApplicationException("You must set DefaultKey");
            }

            return AddConfig(name, DefaultKey);
        }

        /// <include file='RegistryConfigSource.xml' path='//Method[@name="AddConfigKey"]/docs/*' />
        public IConfig AddConfig(string name, RegistryKey key)
        {
            var result = new RegistryConfig(name, this);
            result.Key = key;
            result.ParentKey = true;

            Configs.Add(result);

            return result;
        }

        /// <include file='RegistryConfigSource.xml' path='//Method[@name="AddMapping"]/docs/*' />
        public void AddMapping(RegistryKey registryKey, string path)
        {
            var key = registryKey.OpenSubKey(path, true);

            if (key == null)
            {
                throw new ArgumentException("The specified key does not exist");
            }

            LoadKeyValues(key, ShortKeyName(key));
        }

        /// <include file='RegistryConfigSource.xml' path='//Method[@name="AddMappingRecurse"]/docs/*' />
        public void AddMapping(RegistryKey registryKey,
                                string path,
                                RegistryRecurse recurse)
        {
            var key = registryKey.OpenSubKey(path, true);

            if (key == null)
            {
                throw new ArgumentException("The specified key does not exist");
            }

            if (recurse == RegistryRecurse.Namespacing)
            {
                LoadKeyValues(key, path);
            }
            else
            {
                LoadKeyValues(key, ShortKeyName(key));
            }

            var subKeys = key.GetSubKeyNames();
            for (var i = 0; i < subKeys.Length; i++)
            {
                switch (recurse)
                {
                    case RegistryRecurse.None:
                        // no recursion
                        break;

                    case RegistryRecurse.Namespacing:
                        AddMapping(registryKey, path + "\\" + subKeys[i], recurse);
                        break;

                    case RegistryRecurse.Flattened:
                        AddMapping(key, subKeys[i], recurse);
                        break;
                }
            }
        }

        /// <include file='IConfigSource.xml' path='//Method[@name="Save"]/docs/*' />
        public override void Save()
        {
            MergeConfigsIntoDocument();

            for (var i = 0; i < Configs.Count; i++)
            {
                // New merged configs are not RegistryConfigs
                if (Configs[i] is RegistryConfig)
                {
                    var config = (RegistryConfig)Configs[i];
                    var keys = config.GetKeys();

                    for (var j = 0; j < keys.Length; j++)
                    {
                        config.Key.SetValue(keys[j], config.Get(keys[j]));
                    }
                }
            }
        }

        /// <include file='IConfigSource.xml' path='//Method[@name="Reload"]/docs/*' />
        public override void Reload()
        {
            ReloadKeys();
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Loads all values from the registry key.
        /// </summary>
        private void LoadKeyValues(RegistryKey key, string keyName)
        {
            var config = new RegistryConfig(keyName, this);
            config.Key = key;

            var values = key.GetValueNames();
            foreach (var value in values)
            {
                config.Add(value, key.GetValue(value).ToString());
            }
            Configs.Add(config);
        }

        /// <summary>
        /// Merges all of the configs from the config collection into the
        /// registry.
        /// </summary>
        private void MergeConfigsIntoDocument()
        {
            foreach (IConfig config in Configs)
            {
                if (config is RegistryConfig)
                {
                    var registryConfig = (RegistryConfig)config;

                    if (registryConfig.ParentKey)
                    {
                        registryConfig.Key =
                            registryConfig.Key.CreateSubKey(registryConfig.Name);
                    }
                    RemoveKeys(registryConfig);

                    var keys = config.GetKeys();
                    for (var i = 0; i < keys.Length; i++)
                    {
                        registryConfig.Key.SetValue(keys[i], config.Get(keys[i]));
                    }
                    registryConfig.Key.Flush();
                }
            }
        }

        /// <summary>
        /// Reloads all keys.
        /// </summary>
        private void ReloadKeys()
        {
            var keys = new RegistryKey[Configs.Count];

            for (var i = 0; i < keys.Length; i++)
            {
                keys[i] = ((RegistryConfig)Configs[i]).Key;
            }

            Configs.Clear();
            for (var i = 0; i < keys.Length; i++)
            {
                LoadKeyValues(keys[i], ShortKeyName(keys[i]));
            }
        }

        /// <summary>
        /// Removes all keys not present in the current config.
        /// </summary>
        private void RemoveKeys(RegistryConfig config)
        {
            foreach (var valueName in config.Key.GetValueNames())
            {
                if (!config.Contains(valueName))
                {
                    config.Key.DeleteValue(valueName);
                }
            }
        }

        /// <summary>
        /// Returns the key name without the fully qualified path.
        /// e.g. no HKEY_LOCAL_MACHINE\\MyKey, just MyKey
        /// </summary>
        private string ShortKeyName(RegistryKey key)
        {
            var index = key.Name.LastIndexOf("\\");

            return (index == -1) ? key.Name : key.Name.Substring(index + 1);
        }

        #region RegistryConfig class

        /// <summary>
        /// Registry Config class.
        /// </summary>
        private class RegistryConfig : ConfigBase
        {
            #region Private variables

            private RegistryKey key = null;
            private bool parentKey = false;

            #endregion Private variables

            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            public RegistryConfig(string name, IConfigSource source)
                : base(name, source)
            {
            }

            #endregion Constructor

            #region Public properties

            /// <summary>
            /// Gets or sets whether the key is a parent key.
            /// </summary>
            public bool ParentKey
            {
                get { return parentKey; }
                set { parentKey = value; }
            }

            /// <summary>
            /// Registry key for the Config.
            /// </summary>
            public RegistryKey Key
            {
                get { return key; }
                set { key = value; }
            }

            #endregion Public properties
        }

        #endregion RegistryConfig class

        #endregion Private methods
    }
}