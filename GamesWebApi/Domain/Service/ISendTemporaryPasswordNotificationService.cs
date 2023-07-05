using Domain.DTO;

namespace Domain.Service;

public interface ISendTemporaryPasswordNotificationService
{
    void SendNotification(ForgotPasswordEmailReceiverDto dto);
}