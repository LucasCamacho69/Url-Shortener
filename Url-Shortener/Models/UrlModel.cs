using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redirect.Models
{
    public class UrlModel
    {
        public int Id { get; init; }
        public string Url { get; private set; }
        public string ShortCode { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int AccessCount { get; private set; }

        public UrlModel(string url, string shortCode)
        {
            Url = url;
            ShortCode = shortCode;
        }

        public void ChangeUrl(string url)
        {
            Url = url;
        }

        public void Counter()
        {
            AccessCount += 1;
        }

    }
}