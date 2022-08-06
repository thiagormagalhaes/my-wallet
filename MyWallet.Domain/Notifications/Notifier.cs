namespace MyWallet.Domain.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifcations;

        public Notifier()
        {
            _notifcations = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notifcations.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
            return _notifcations;
        }

        public bool HaveNotification()
        {
            return _notifcations.Any();
        }
    }
}
