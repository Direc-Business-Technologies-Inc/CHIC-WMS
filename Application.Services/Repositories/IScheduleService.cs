using static Application.Models.ViewModels.ScheduleViewModel;

namespace Application.Services.Repositories
{
    public interface IScheduleService
    {
        ScheduleViewModel InitializeReceivingSchedule();

        void FilterSchedule(ScheduleViewModel model, string start, string end, string type);
        ScheduleViewModel InitializeDispatchSchedule();
        ScheduleViewModel InitializeIrradiationSchedule();
		ScheduleViewModel getAllSchedules(string location);
        bool updateEventSchedule(ScheduleEvents eventData);
        Task<bool> updateEventsSchedule(List<ScheduleEvents> eventsData);

		ScheduleViewModel InitializeScheduleDetails(string DocEntry, string ItemCode);
		Task<bool> updateScheduleDetails(string id, string DispatchDate, string IrradDate, string IrradiationStart, string IrradiationEnd, string Remarks);
	}
}