namespace MyWallet.Domain.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifcations;

        public Notifier()
        {
            _notifcations = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notifcations.Add(notification);
        }

        public void NotifyError(string message)
        {
            _notifcations.Add(new Notification(message));
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
