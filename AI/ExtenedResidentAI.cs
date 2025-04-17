namespace MoreTransferReasons.AI
{
    public class ExtenedResidentAI : ResidentAI, IExtendedCitizenAI
    {
        public void ExtendedStartTransfer(uint citizenID, ref Citizen data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            if (data.m_flags == Citizen.Flags.None || data.Dead || data.Sick)
            {
                return;
            }
            ushort source_building = 0;
            switch(data.CurrentLocation)
            {
                case Citizen.Location.Home:
                    source_building = data.m_homeBuilding; 
                    break;

                case Citizen.Location.Work:
                    source_building = data.m_workBuilding;
                    break;

                case Citizen.Location.Visit:
                    source_building = data.m_visitBuilding;
                    break;
            }
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.MealsLow:
                case ExtendedTransferManager.TransferReason.MealsMedium:
                case ExtendedTransferManager.TransferReason.MealsHigh:
                    data.m_flags &= ~Citizen.Flags.Evacuating;
                    if (StartMoving(citizenID, ref data, source_building, offer.Building))
                    {
                        data.SetVisitplace(citizenID, offer.Building, 0u);
                    }
                    break;
                case ExtendedTransferManager.TransferReason.CarBuy:
                case ExtendedTransferManager.TransferReason.CarSell:
                case ExtendedTransferManager.TransferReason.FuelVehicle:
                case ExtendedTransferManager.TransferReason.FuelElectricVehicle:
                    data.m_flags &= ~Citizen.Flags.Evacuating;
                    StartMoving(citizenID, ref data, source_building, offer.Building);
                    break;
            }
        }

    }
}
