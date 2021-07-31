using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace Service.Helpers
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(
            IEnumerable<string> to, string subject, string content)
        {
            To = new();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        }
    }
}
