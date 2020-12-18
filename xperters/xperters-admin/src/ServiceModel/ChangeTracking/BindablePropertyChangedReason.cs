namespace Xperters.Admin.ServiceModel.ChangeTracking
{
	public enum BindablePropertyChangedReason
	{
		/// <summary>
		/// Fires on an object whose property has been set
		/// </summary>
		PropertySet = 0,

		/// <summary>
		/// Fires on an object that has been added to a collection using the AddWithTracking extension method
		/// </summary>
		PropertyOwnerAddedToCollection = 1,

		/// <summary>
		/// Fires on an object that has been removed from a collection using the RemovedWithTracking extension method
		/// </summary>
		PropertyOwnerRemovedFromCollection = 2,

		/// <summary>
		/// Fires on all objects in a collection and all properties on those objects when a List on a Bindable object has been set (bindable.List = somenewlist).

		/// </summary>
		PropertyOwnerCollectionReplaced = 3,

		/// <summary>
		/// Fires on the list property itself when an item gets added via the AddWithTracking extension method
		/// </summary>
		AddedToCollection = 4,

		/// <summary>
		/// Fires on the list property itself when an item gets removed via the RemovedWithTracking extension method
		/// </summary>
		RemovedFromCollection = 5
	}
}