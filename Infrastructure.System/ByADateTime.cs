using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.System
{
    /// <summary>
    /// Used for getting DateTime.Now(), time is changeable for unit testing
    /// </summary>
    public static class ByADateTime
    {
        /// <summary> Normally this is a pass-through to DateTime.Now, but it can be overridden with SetDateTime( .. ) for testing or debugging.
        /// </summary>
        private static Func<DateTime> _Now = () => DateTime.Now;

        public static DateTime Now
        {
            get
            {
                return _Now();
            }
        }

        /// <summary> Set time to return when SystemTime.Now() is called.
        /// </summary>
        public static void SetDateTime(DateTime dateTimeNow)
        {
            _Now = () => dateTimeNow;
        }

        /// <summary> Resets SystemTime.Now() to return DateTime.Now.
        /// </summary>
        public static void ResetDateTime()
        {
            _Now = () => DateTime.Now;
        }


    }
}
