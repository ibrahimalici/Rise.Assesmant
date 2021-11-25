using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Messages
{
    public class ReportResultMessage
    {
        public Guid ReportId { get; set; }
        public bool ResultState { get; set; }
        public string ResultMessage { get; set; }
    }
}
