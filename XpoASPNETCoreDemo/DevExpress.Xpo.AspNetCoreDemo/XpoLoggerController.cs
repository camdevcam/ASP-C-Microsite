using Microsoft.AspNetCore.Mvc;
using DevExpress.Xpo.Logger;

namespace DevExpress.Xpo.Demo
{
    public class XpoLoggerController : Controller
    {
        readonly static LoggerBase logger = new LoggerBase(50000);
        static XpoLoggerController() {
            LogManager.SetTransport(logger);
        }
        [HttpGet]
        public LogMessage[] GetCompleteLog() {
            return logger.GetCompleteLog();
        }
        [HttpGet]
        public LogMessage GetMessage() {
            return logger.GetMessage();
        }
        [HttpGet]
        public LogMessage[] GetMessages(int messagesAmount) {
            return logger.GetMessages(messagesAmount);
        }
    }
}
