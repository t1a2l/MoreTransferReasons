namespace MoreTransferReasons.AI
{
    public class ExtendedPassengerCarAI : PassengerCarAI, IExtendedVehicleAI
    {
        void IExtendedVehicleAI.ExtendedStartTransfer(ushort vehicleID, ref Vehicle data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            var transferType = data.m_transferType;
            if (data.m_transferType >= 200)
            {
                transferType = (byte)(data.m_transferType - 200);
            }
            if (material == (ExtendedTransferManager.TransferReason)transferType)
            {
                if ((data.m_flags & Vehicle.Flags.WaitingTarget) != 0)
                {
                    SetTarget(vehicleID, ref data, offer.Building);
                }
            }
            if (material == ExtendedTransferManager.TransferReason.FuelVehicle)
            {
                data.m_transferType = (byte)(material + 200);
                SetTarget(vehicleID, ref data, offer.Building);
            }
        }
    }
}
