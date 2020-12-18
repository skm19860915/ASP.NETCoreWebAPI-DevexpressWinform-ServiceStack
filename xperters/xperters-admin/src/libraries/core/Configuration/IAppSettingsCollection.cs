using System.Collections;
using System.Collections.Specialized;

namespace Xperters.Core.Configuration
{
    public interface IAppSettingsCollection : ICollection
    {
        /// <summary>
        /// Gets a value indicating whether the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/> contains keys that are not null.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/> contains keys that are not null; otherwise, false.
        /// </returns>
        bool HasKeys();

        /// <summary>
        /// Adds an entry with the specified name and value to the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </summary>
        /// <param name="name">The <see cref="T:System.String"/> key of the entry to add. The key can be null.</param>
        /// <param name="value">The <see cref="T:System.String"/> value of the entry to add. The value can be null.</param>
        /// <exception cref="T:System.NotSupportedException">The collection is read-only. </exception>
        void Add(string name, string value);

        /// <summary>
        /// Gets the values associated with the specified key from the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/> combined into one comma-separated list.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> that contains a comma-separated list of the values associated with the specified key from the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>, if found; otherwise, null.
        /// </returns>
        /// <param name="name">The <see cref="T:System.String"/> key of the entry that contains the values to get. The key can be null.</param>
        string Get(string name);

        /// <summary>
        /// Gets the values associated with the specified key from the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> array that contains the values associated with the specified key from the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>, if found; otherwise, null.
        /// </returns>
        /// <param name="name">The <see cref="T:System.String"/> key of the entry that contains the values to get. The key can be null.</param>
        string[] GetValues(string name);

        /// <summary>
        /// Sets the value of an entry in the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </summary>
        /// <param name="name">The <see cref="T:System.String"/> key of the entry to add the new value to. The key can be null.</param>
        /// <param name="value">The <see cref="T:System.Object"/> that represents the new value to add to the specified entry. The value can be null.</param>
        /// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
        void Set(string name, string value);
        
        /// <summary>
        /// Removes the entries with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase"/> instance.
        /// </summary>
        /// <param name="name">The <see cref="T:System.String"/> key of the entry to remove. The key can be null.</param>
        /// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
        void Remove(string name);

        /// <summary>
        /// Gets the values at the specified index of the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/> combined into one comma-separated list.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> that contains a comma-separated list of the values at the specified index of the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>, if found; otherwise, null.
        /// </returns>
        /// <param name="index">The zero-based index of the entry that contains the values to get from the collection.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is outside the valid range of indexes for the collection.</exception>
        string Get(int index);
        
        /// <summary>
        /// Gets the values at the specified index of the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> array that contains the values at the specified index of the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>, if found; otherwise, null.
        /// </returns>
        /// <param name="index">The zero-based index of the entry that contains the values to get from the collection.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is outside the valid range of indexes for the collection. </exception>
        string[] GetValues(int index);
        
        /// <summary>
        /// Gets the key at the specified index of the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> that contains the key at the specified index of the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>, if found; otherwise, null.
        /// </returns>
        /// <param name="index">The zero-based index of the key to get from the collection.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is outside the valid range of indexes for the collection. </exception>
        string GetKey(int index);

        /// <summary>
        /// Invalidates the cached arrays and removes all entries from the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
        /// <PermissionSet>
        /// <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        /// </PermissionSet>
        void Clear();

        /// <summary>
        /// Gets or sets the entry with the specified key in the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> that contains the comma-separated list of values associated with the specified key, if found; otherwise, null.
        /// </returns>
        /// <param name="name">The <see cref="T:System.String"/> key of the entry to locate.</param>
        /// <exception cref="T:System.NotSupportedException">The collection is read-only and the operation attempts to modify the collection.</exception>
        string this[string name] { get; set; }

        /// <summary>
        /// Gets the entry at the specified index of the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> that contains the comma-separated list of values at the specified index of the collection.
        /// </returns>
        /// <param name="index">The zero-based index of the entry to locate in the collection.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is outside the valid range of indexes for the collection.</exception>
        string this[int index] { get; }

        /// <summary>
        /// Gets all the keys in the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> array that contains all the keys of the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/>.
        /// </returns>
        string[] AllKeys { get; }

        /// <summary>
        /// Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection"/> instance that contains all the keys in the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/> instance.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection"/> instance that contains all the keys in the <see cref="T:Xperters.Core.Configuration.IAppSettingsCollection"/> instance.
        /// </returns>
        NameObjectCollectionBase.KeysCollection Keys { get; }
    }
}