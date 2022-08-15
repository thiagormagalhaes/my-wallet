namespace MyWallet.Domain.Notifications
{
    public interface INotifier
    {
        bool HaveNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
        void NotifyError(string message);
    }
}
