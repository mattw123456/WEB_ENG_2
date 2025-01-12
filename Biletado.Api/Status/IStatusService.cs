namespace Biletado.Api.Status
{
    public interface IStatusService
    {
        public StatusInformation GebeStatusInformationenZurueck();
        public StatusInformationHealth GebeHealthInformationenZurueck();
    }


}
