using ConferenceRoom.Data.DBContext;
using ConferenceRoom.Data.Entities;
using ConferenceRoom.Interface;
using ConferenceRoom.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Services
{
    public class UnavailabilityPeriodService : IUnavailabilityPeriodService
    {
        private readonly ApplicationDbContext _context;

        public UnavailabilityPeriodService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddUnavailabilityPeriod(UnavailabilityPeriodViewModel periodViewModel)
        {
            //handle period overlap
            _context.UnavailabilityPeriods.Add(ViewModelToEntity(periodViewModel));
            await _context.SaveChangesAsync();                 
        }

       

        public async Task<List<UnavailabilityPeriodViewModel>> GetAllUnavailabilityPeriods()
        {
            
                var unavailabilityPVm = new List<UnavailabilityPeriodViewModel>();

                var unavailabilities = await _context.UnavailabilityPeriods.ToListAsync();

                foreach (var unavailabilitiesP in unavailabilities)
                {
                    unavailabilityPVm.Add(EntityToViewModel(unavailabilitiesP));
                }

                return unavailabilityPVm;
            

        }

        public async Task<UnavailabilityPeriodViewModel> GetUnavailabilityPeriodById(int id)
        {
           
                var unavailabilityPVm = await _context.UnavailabilityPeriods.FindAsync(id);           
                //cfare ndodh nese id nuk ndodhet ne db
                return EntityToViewModel(unavailabilityPVm);
           
        }

        public async Task UpdateUnavailabilityPeriod(UnavailabilityPeriodViewModel periodViewModel)
        {
              var periodViewModelExist = _context.UnavailabilityPeriods.Any(p => p.Id == periodViewModel.Id);

                if (periodViewModelExist == null)
                {
                    throw new Exception("Unavailability Period does not exist");
                }

                _context.UnavailabilityPeriods.Update(ViewModelToEntity(periodViewModel));
                await _context.SaveChangesAsync();
            
        }


        public async Task DeleteUnavailabilityPeriod(int id)
        {
            
                var unavailability = _context.UnavailabilityPeriods.Where(p => p.Id == id).FirstOrDefault();//Check If room exist and response in web

                if (unavailability == null)
                {
                    throw new Exception("Unavailability Period does not exist");
                }

                _context.UnavailabilityPeriods.Remove(unavailability);
                await _context.SaveChangesAsync();
                

        }





        private UnavailabilityPeriod ViewModelToEntity(UnavailabilityPeriodViewModel periodViewModel)
        {
            var unavailabilityPeriod = new UnavailabilityPeriod()
            {
                Id = periodViewModel.Id,
                StartDate = periodViewModel.StartDate,
                EndDate = periodViewModel.EndDate
            };
            return unavailabilityPeriod;
        }

        private UnavailabilityPeriodViewModel EntityToViewModel(UnavailabilityPeriod entityP)
        {
            var unavailabilityPeriodViewModel = new UnavailabilityPeriodViewModel()
            {
                Id = entityP.Id,
                StartDate = entityP.StartDate,
                EndDate = entityP.EndDate
            };
            return unavailabilityPeriodViewModel;

        }

    }
}
