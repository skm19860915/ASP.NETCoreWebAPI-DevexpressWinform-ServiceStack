using System.Collections.Specialized;

namespace Xperters.Core.Configuration
{
    public class InMemoryAppSettings : IAppSettingsCollection
    {
        private readonly object _syncRoot = new object();
        private readonly NameValueCollection _appSettings = new NameValueCollection();

        public bool HasKeys()
        {
            return _appSettings.HasKeys();
        }

        public void Add(string name, string value)
        {
            _appSettings.Add(name, value);
        }

        public string Get(string name)
        {
            return _appSettings.Get(name);
        }

        public string[] GetValues(string name)
        {
            return _appSettings.GetValues(name);
        }

        public void Set(string name, string value)
        {
            _appSettings.Set(name, value);
        }

        public void Remove(string name)
        {
            _appSettings.Remove(name);
        }

        public string Get(int index)
        {
            return _appSettings.Get(index);
        }

        public string[] GetValues(int index)
        {
            return _appSettings.GetValues(index);
        }

        public string GetKey(int index)
        {
            return _appSettings.GetKey(index);
        }

        public void Clear()
        {
            _appSettings.Clear();
        }

        public string this[string name]
        {
            get { return _appSettings[name]; }
            set { _appSettings[name] = value; }
        }

        public string this[int index]
        {
            get { return _appSettings[index]; }
        }

        public string[] AllKeys
        {
            get { return _appSettings.AllKeys; }
        }

        public NameObjectCollectionBase.KeysCollection Keys
        {
            get { return _appSettings.Keys; }
        }

        public void CopyTo(System.Array array, int index)
        {
            _appSettings.CopyTo(array, index);
        }

        public int Count
        {
            get { return _appSettings.Count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return _syncRoot; }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _appSettings.GetEnumerator();
        }
    }
}
