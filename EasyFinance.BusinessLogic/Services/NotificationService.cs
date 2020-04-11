using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Services
{
    public class NotificationService: INotificationService
    {
        public async Task<Notification> GetNotificationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveNotificationAsync(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
