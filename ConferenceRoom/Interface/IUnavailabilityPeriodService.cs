using ConferenceRoom.Models;

namespace ConferenceRoom.Interface
{
    public interface IUnavailabilityPeriodService
    {
        Task AddUnavailabilityPeriod(UnavailabilityPeriodViewModel periodViewModel);
        Task<UnavailabilityPeriodViewModel> GetUnavailabilityPeriodById(int id);
        Task<List<UnavailabilityPeriodViewModel>> GetAllUnavailabilityPeriods();
        Task UpdateUnavailabilityPeriod(UnavailabilityPeriodViewModel periodViewModel);
        Task DeleteUnavailabilityPeriod(UnavailabilityPeriodViewModel periodViewModel);
    }
}
