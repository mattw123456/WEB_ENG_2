namespace Biletado.Domain.Status
{
    public interface IStatusService
    {
        public StatusInformation GebeStatusInformationenZurueck();
        public StatusInformationHealth GebeHealthInformationenZurueck();
    }
}
