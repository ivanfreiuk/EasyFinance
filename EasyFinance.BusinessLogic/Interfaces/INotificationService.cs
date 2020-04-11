using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Interfaces
{
    public interface INotificationService
    {
        Task<Notification> GetNotificationAsync(int id);

        Task<IEnumerable<Notification>> GetNotificationsAsync();

        Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(int userId);

        Task AddNotificationAsync(Notification notification);

        Task UpdateNotificationAsync(Notification notification);

        Task RemoveNotificationAsync(Notification notification);
    }
}
