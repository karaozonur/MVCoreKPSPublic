using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace MVCoreKPSPublic.Models.Notification
{
    public class NotificationService: INotificationService
    {
        private readonly string _notificationListKey = "temp-notify-list";
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public NotificationService(IHttpContextAccessor contextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            _contextAccessor = contextAccessor;
        }

        protected virtual void PrepareTempData(NotifyType type, string message, bool encode=true)
        {
            var notifies = Getnotifies();
            notifies.Add(new NotifyData
            { 
                Message= message,   
                Type=type,
                Encode=encode   
            });
            TempData[_notificationListKey]=JsonConvert.SerializeObject(notifies);

        }
        private ITempDataDictionary TempData => _tempDataDictionaryFactory.GetTempData(_contextAccessor.HttpContext);
        public virtual IList<NotifyData> Getnotifies()
        {
            return TempData.ContainsKey(_notificationListKey)
              ? JsonConvert.DeserializeObject<IList<NotifyData>>(TempData[_notificationListKey].ToString() ?? string.Empty)
              : new List<NotifyData>();
        }

        public virtual void Notification(NotifyType type,string message,bool encode=true)
        {
            PrepareTempData(type, message, encode);
        }
        public virtual void WarningNotification(string message, bool encode = true)
        {
            PrepareTempData(NotifyType.Warning, message, encode);
        }
        public virtual void SuccessNotification(string message, bool encode = true)
        {
            PrepareTempData(NotifyType.Success, message, encode);
        }
        public virtual void ErrorNotification(string message, bool encode = true)
        {
            PrepareTempData(NotifyType.Error, message, encode);
        }
        public virtual void ErrorNotification(Exception exception)
        {

            if (exception == null)
                return;
            ErrorNotification(exception.Message);
        }

    }
}
