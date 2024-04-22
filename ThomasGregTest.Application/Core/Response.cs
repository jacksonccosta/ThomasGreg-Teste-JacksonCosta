using Flunt.Notifications;

namespace ThomasGregTest.Application;

public class Response
{
    public Response()
    {
    }

    private IList<Notification> _messages { get; } = new List<Notification>();
    public IReadOnlyCollection<Notification> Messages => [.. _messages];
    public bool HasMessages => _messages.Any();
    public object Value { get; private set; }
    public Response(object @object) : this()
    {
        AddValue(@object);
    }
    public void AddValue(object @object)
    {
        Value = @object;
    }
    public void AddNotification(Notification notification)
    {
        _messages.Add(notification);
    }
    public void AddNotifications(IEnumerable<Notification> notifications)
    {
        foreach (var notification in notifications)
        {
            AddNotification(notification);
        }
    }
    public override string ToString()
    {
        return string.Join(" - ", Messages.Select(x => x.Message));
    }
}
