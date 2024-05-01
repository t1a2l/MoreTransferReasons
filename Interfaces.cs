namespace MoreTransferReasons.Utils
{
    public interface IExtendedVehicleAI
    {
        void ExtendedStartTransfer(ushort vehicleID, ref Vehicle data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer);
    }

    public interface IExtendedBuildingAI
    {
        void ExtendedStartTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer);

        void ExtendedGetMaterialAmount(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, out int amount, out int max);

        void ExtendedModifyMaterialBuffer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ref int amountDelta);
    }

    public interface IExtendedCitizenAI
    {
        void ExtendedStartTransfer(uint citizenID, ref Citizen data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer);
    }

}
