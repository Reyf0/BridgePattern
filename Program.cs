namespace BridgePattern
{
    public interface INotificationSender
    {
        void Send(string message, NotificationFormat format);
    }

    public enum NotificationFormat
    {
        Text,
        HTML
    }

    public class EmailNotificationSender : INotificationSender
    {
        public void Send(string message, NotificationFormat format)
        {
            if (format == NotificationFormat.HTML)
            {
                Console.WriteLine("Отправка Email в формате HTML: " + message);
            }
            else
            {
                Console.WriteLine("Отправка Email в текстовом формате: " + message);
            }
        }
    }

    public class SmsNotificationSender : INotificationSender
    {
        public void Send(string message, NotificationFormat format)
        {
            // Логика отправки SMS (обычно только текст)
            Console.WriteLine("Отправка SMS: " + message);
        }
    }

	public class PushNotificationSender : INotificationSender
	{
		public void Send(string message, NotificationFormat format)
		{
			// Логика отправки Push-уведомления
			if (format == NotificationFormat.HTML)
			{
				Console.WriteLine("Отправка Push-уведомления в формате HTML: " + message);
			}
			else
			{
				Console.WriteLine("Отправка Push-уведомления в текстовом формате: " + message);
			}
		}
	}



	public abstract class Notification
    {
        protected INotificationSender _notificationSender;

        protected Notification(INotificationSender notificationSender)
        {
            _notificationSender = notificationSender;
        }

        public abstract void SendNotification(string message, NotificationFormat format);
    }

    public class OrderNotification : Notification
    {
        public OrderNotification(INotificationSender notificationSender) : base(notificationSender) { }

        public override void SendNotification(string message, NotificationFormat format)
        {
            Console.WriteLine("Уведомление о заказе:");
            _notificationSender.Send(message, format);
        }
    }

    public class ReminderNotification : Notification
    {
        public ReminderNotification(INotificationSender notificationSender) : base(notificationSender) { }

        public override void SendNotification(string message, NotificationFormat format)
        {
            Console.WriteLine("Уведомление-напоминание:");
            _notificationSender.Send(message, format);
        }
    }

	public class AlertNotification : Notification
	{
		public AlertNotification(INotificationSender notificationSender) : base(notificationSender) { }

		public override void SendNotification(string message, NotificationFormat format)
		{
			Console.WriteLine("Push-уведомление:");
			_notificationSender.Send(message, format);
		}
	}


	internal class Program
    {

        public static void Main()
        {
			INotificationSender emailSender = new EmailNotificationSender();
			INotificationSender smsSender = new SmsNotificationSender();
			INotificationSender pushSender = new PushNotificationSender();

			Notification orderNotification = new OrderNotification(emailSender);
			Notification reminderNotification = new ReminderNotification(smsSender);
			Notification alertNotification = new AlertNotification(pushSender);

			orderNotification.SendNotification("Ваш заказ был успешно обработан.", NotificationFormat.HTML);			
			reminderNotification.SendNotification("Напоминание о встрече в 15:00.", NotificationFormat.Text);
			alertNotification.SendNotification("Внимание! Высокая загрузка сервера.", NotificationFormat.Text);

		}
    }
}