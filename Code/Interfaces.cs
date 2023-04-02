
namespace MoreTransferReasons
{
	public interface IExtendedVehicleAI
	{
		void StartTransfer(ushort vehicleID, ref Vehicle data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer);
	}

	public interface IExtendedBuildingAI
	{
		void StartTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer);


		void GetMaterialAmount(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, out int amount, out int max);

		void ModifyMaterialBuffer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ref int amountDelta);
	}

	public interface IExtendedCitizenAI
	{
		void StartTransfer(uint citizenID, ref Citizen data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer);
	}

}
