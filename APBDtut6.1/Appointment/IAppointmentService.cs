namespace APBDtut6._1.Appointment;
using APBDtut6._1.DTO;
using APBDtut6._1.Models;
public interface IAppointmentService
{
    Task<IEnumerable<AppointmentListDto>> GetAppointmentsAsync(string? status, string? patientLastName);
    Task<ServiceResult<AppointmentDetailsDto>> GetAppointmentAsync(int idAppointment);
    Task<ServiceResult<int>> CreateAppointmentAsync(CreateAppointmentRequestDto request);
    Task<ServiceResult> UpdateAppointmentAsync(int idAppointment, UpdateAppointmentRequestDto request);
    Task<ServiceResult> DeleteAppointmentAsync(int idAppointment);
}