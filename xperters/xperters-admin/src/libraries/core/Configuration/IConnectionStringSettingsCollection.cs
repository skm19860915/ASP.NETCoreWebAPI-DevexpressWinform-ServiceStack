using System.Collections;

namespace Xperters.Core.Configuration
{
    public interface IConnectionStringSettingsCollection : ICollection
    {
        /// <summary>
        /// Adds a <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object to the collection.
        /// </summary>
        /// <param name="settings">A <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object to add to the collection.</param>
        void Add(IConnectionStringSettings settings);

        /// <summary>
        /// Removes the specified <see cref="T:Xperters.Configuration.IonnectionStringSettings"/> object from the collection.
        /// </summary>
        /// <param name="settings">A <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        void Remove(IConnectionStringSettings settings);

        /// <summary>
        /// Removes the specified <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object from the collection.
        /// </summary>
        /// <param name="name">The name of a <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        void Remove(string name);

        /// <summary>
        /// Removes the <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object at the specified index in the collection.
        /// </summary>
        /// <param name="index">The index of a <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        void RemoveAt(int index);

        /// <summary>
        /// Removes all the <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> objects from the collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns the collection index of the passed <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object.
        /// </summary>
        /// 
        /// <returns>
        /// The collection index of the specified <see cref="T:Xperters.Core.Configuration.IConnectionStringSettingsCollection"/> object.
        /// </returns>
        /// <param name="settings">A <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        int IndexOf(IConnectionStringSettings settings);

        /// <summary>
        /// Indicates whether the <see cref="T:Xperters.Core.Configuration.IConnectionStringSettingsCollection"/> object is read only.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:Xperters.Core.Configuration.IConnectionStringSettingsCollection"/> object is read only; otherwise, false.
        /// </returns>
        bool IsReadOnly();

        /// <summary>
        /// Gets or sets the connection string at the specified index in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object at the specified index.
        /// </returns>
        /// <param name="index">The index of a <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        IConnectionStringSettings this[int index] { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object with the specified name in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object with the specified name; otherwise, null.
        /// </returns>
        /// <param name="name">The name of a <see cref="T:Xperters.Core.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        IConnectionStringSettings this[string name] { get; }

        /// <summary>
        /// Gets or sets a value that specifies whether the collection has been cleared.
        /// </summary>
        /// 
        /// <returns>
        /// true if the collection has been cleared; otherwise, false. The default is false.
        /// </returns>
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.</exception>
        bool EmitClear { get; set; }
    }

    /*
    public interface IConnectionStringSettingsCollection : ICollection<IConnectionStringSettings>
    {
        /// <summary>
        /// Returns the collection index of the passed <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object.
        /// </summary>
        /// 
        /// <returns>
        /// The collection index of the specified <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object.
        /// </returns>
        /// <param name="item">A <see cref="T:Xperters.Configuration.ConnectionStringSettings"/> object in the collection.</param>
        int IndexOf(IConnectionStringSettings item);

        /// <summary>
        /// Removes the specified <see cref="T:Xperters.Configuration.ConnectionStringSettings"/> object from the collection.
        /// </summary>
        /// <param name="name">The name of a <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        void Remove(string name);

        /// <summary>
        /// Removes the <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object at the specified index in the collection.
        /// </summary>
        /// <param name="index">The index of a <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        void RemoveAt(int index);

        /// <summary>
        /// Gets or sets the connection string at the specified index in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object at the specified index.
        /// </returns>
        /// <param name="index">The index of a <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        IConnectionStringSettings this[int index] { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object with the specified name in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object with the specified name; otherwise, null.
        /// </returns>
        /// <param name="name">The name of a <see cref="T:Xperters.Configuration.IConnectionStringSettings"/> object in the collection.</param>
        IConnectionStringSettings this[string name] { get; }
    }*/
}