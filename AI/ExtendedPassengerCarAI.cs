namespace MoreTransferReasons.AI
{
    public class ExtendedPassengerCarAI : PassengerCarAI, IExtendedVehicleAI
    {
        void IExtendedVehicleAI.ExtendedStartTransfer(ushort vehicleID, ref Vehicle data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            if (material == ExtendedTransferManager.TransferReason.FuelVehicle)
            {
                data.m_custom = (ushort)material;
                SetTarget(vehicleID, ref data, offer.Building);
            }
        }
    }
}
