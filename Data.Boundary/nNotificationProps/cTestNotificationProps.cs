using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Boundary.nNotificationProps
{
    public class cTestNotificationProps : cBaseNotificationProps
    {
        public virtual string SellerName { get; set; }
        public virtual string SelleProfileImage { get; set; }
        public virtual DateTime BookedDate { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual string SellerLessonName { get; set; }
        public virtual long SellerLessonID { get; set; }
        public virtual int SellerLessonMinute { get; set; }
        public virtual int LessonType { get; set; }
        public virtual int CanceledBy { get; set; }

        public virtual string CustomerName { get; set; }
        public virtual string CustomerProfileImage { get; set; }

    }
}
