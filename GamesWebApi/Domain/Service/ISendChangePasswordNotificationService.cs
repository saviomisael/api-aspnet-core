using Domain.DTO;

namespace Domain.Service;

public interface ISendChangePasswordNotificationService
{
    void SendNotification(EmailReceiverDto dto);
}