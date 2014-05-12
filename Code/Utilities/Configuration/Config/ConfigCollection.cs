using System;
using System.Collections;

namespace Nini.Config
{
    #region ConfigEventHandler class

    /// <include file='ConfigEventArgs.xml' path='//Delegate[@name="ConfigEventHandler"]/docs/*' />
    public delegate void ConfigEventHandler(object sender, ConfigEventArgs e);

    /// <include file='ConfigEventArgs.xml' path='//Class[@name="ConfigEventArgs"]/docs/*' />
    public class ConfigEventArgs : EventArgs
    {
        private readonly IConfig config;

        /// <include file='ConfigEventArgs.xml' path='//Constructor[@name="ConstructorIConfig"]/docs/*' />
        public ConfigEventArgs(IConfig config)
        {
            this.config = config;
        }

        /// <include file='ConfigEventArgs.xml' path='//Property[@name="Config"]/docs/*' />
        public IConfig Config
        {
            get { return config; }
        }
    }

    #endregion ConfigEventHandler class

    /// <include file='ConfigCollection.xml' path='//Class[@name="ConfigCollection"]/docs/*' />
    public class ConfigCollection : ICollection, IEnumerable, IList
    {
        #region Private variables

        private readonly ArrayList configList = new ArrayList();
        private readonly ConfigSourceBase owner;

        #endregion Private variables

        #region Constructors

        /// <include file='ConfigCollection.xml' path='//Constructor[@name="Constructor"]/docs/*' />
        public ConfigCollection(ConfigSourceBase owner)
        {
            this.owner = owner;
        }

        #endregion Constructors

        #region Public properties

        /// <include file='ConfigCollection.xml' path='//Property[@name="ItemIndex"]/docs/*' />
        public IConfig this[int index]
        {
            get { return (IConfig)configList[index]; }
        }

        /// <include file='ConfigCollection.xml' path='//Property[@name="ItemName"]/docs/*' />
        public IConfig this[string configName]
        {
            get
            {
                IConfig result = null;

                foreach (IConfig config in configList)
                {
                    if (config.Name == configName)
                    {
                        result = config;
                        break;
                    }
                }

                return result;
            }
        }

        /// <include file='ConfigCollection.xml' path='//Property[@name="Count"]/docs/*' />
        public int Count
        {
            get { return configList.Count; }
        }

        /// <include file='ConfigCollection.xml' path='//Property[@name="IsSynchronized"]/docs/*' />
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <include file='ConfigCollection.xml' path='//Property[@name="SyncRoot"]/docs/*' />
        public object SyncRoot
        {
            get { return this; }
        }

        /// <include file='ConfigCollection.xml' path='//Property[@name="ItemIndex"]/docs/*' />
        object IList.this[int index]
        {
            get { return configList[index]; }
            set { }
        }

        /// <include file='ConfigCollection.xml' path='//Property[@name="IsFixedSize"]/docs/*' />
        public bool IsFixedSize
        {
            get { return false; }
        }

        /// <include file='ConfigCollection.xml' path='//Property[@name="IsReadOnly"]/docs/*' />
        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion Public properties

        #region Public methods

        /// <include file='ConfigCollection.xml' path='//Method[@name="GetEnumerator"]/docs/*' />
        public IEnumerator GetEnumerator()
        {
            return configList.GetEnumerator();
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="CopyTo"]/docs/*' />
        public void CopyTo(Array array, int index)
        {
            configList.CopyTo(array, index);
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="Add"]/docs/*' />
        int IList.Add(object config)
        {
            var newConfig = config as IConfig;

            if (newConfig == null)
            {
                throw new Exception("Must be an IConfig");
            }
            Add(newConfig);
            return IndexOf(newConfig);
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="Remove"]/docs/*' />
        public void Remove(object config)
        {
            configList.Remove(config);
            OnConfigRemoved(new ConfigEventArgs((IConfig)config));
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="RemoveAt"]/docs/*' />
        public void RemoveAt(int index)
        {
            var config = (IConfig)configList[index];
            configList.RemoveAt(index);
            OnConfigRemoved(new ConfigEventArgs(config));
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="Clear"]/docs/*' />
        public void Clear()
        {
            configList.Clear();
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="Contains"]/docs/*' />
        public bool Contains(object config)
        {
            return configList.Contains(config);
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="IndexOf"]/docs/*' />
        public int IndexOf(object config)
        {
            return configList.IndexOf(config);
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="Insert"]/docs/*' />
        public void Insert(int index, object config)
        {
            configList.Insert(index, config);
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="Add"]/docs/*' />
        public void Add(IConfig config)
        {
            if (configList.Contains(config))
            {
                throw new ArgumentException("IConfig already exists");
            }
            var existingConfig = this[config.Name];

            if (existingConfig != null)
            {
                // Set all new keys
                var keys = config.GetKeys();
                for (var i = 0; i < keys.Length; i++)
                {
                    existingConfig.Set(keys[i], config.Get(keys[i]));
                }
            }
            else
            {
                configList.Add(config);
                OnConfigAdded(new ConfigEventArgs(config));
            }
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="AddName"]/docs/*' />
        public IConfig Add(string name)
        {
            ConfigBase result = null;

            if (this[name] == null)
            {
                result = new ConfigBase(name, owner);
                configList.Add(result);
                OnConfigAdded(new ConfigEventArgs(result));
            }
            else
            {
                throw new ArgumentException("An IConfig of that name already exists");
            }

            return result;
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="Remove"]/docs/*' />
        public void Remove(IConfig config)
        {
            configList.Remove(config);
            OnConfigRemoved(new ConfigEventArgs(config));
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="CopyToStrong"]/docs/*' />
        public void CopyTo(IConfig[] array, int index)
        {
            ((ICollection)configList).CopyTo(array, index);
        }

        #endregion Public methods

        #region Public events

        /// <include file='ConfigCollection.xml' path='//Event[@name="ConfigAdded"]/docs/*' />
        public event ConfigEventHandler ConfigAdded;

        /// <include file='ConfigCollection.xml' path='//Event[@name="ConfigRemoved"]/docs/*' />
        public event ConfigEventHandler ConfigRemoved;

        #endregion Public events

        #region Protected methods

        /// <include file='ConfigCollection.xml' path='//Method[@name="OnConfigAdded"]/docs/*' />
        protected void OnConfigAdded(ConfigEventArgs e)
        {
            if (ConfigAdded != null)
            {
                ConfigAdded(this, e);
            }
        }

        /// <include file='ConfigCollection.xml' path='//Method[@name="OnConfigRemoved"]/docs/*' />
        protected void OnConfigRemoved(ConfigEventArgs e)
        {
            if (ConfigRemoved != null)
            {
                ConfigRemoved(this, e);
            }
        }

        #endregion Protected methods

        #region Private methods

        #endregion Private methods
    }
}