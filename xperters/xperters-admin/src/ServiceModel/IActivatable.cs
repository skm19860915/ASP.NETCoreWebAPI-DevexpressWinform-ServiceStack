namespace Xperters.Admin.ServiceModel
{
	public interface IActivatable
	{
		Constants.Enums.ActivationStatus ActivationStatus { get; set; }
		Constants.Enums.ActivationStatus ActivationStatusId { get; set; }
	}
}