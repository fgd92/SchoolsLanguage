//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolsLanguage.ModelDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class VisitInfo
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> DateLastVisit { get; set; }
        public Nullable<int> CountVisit { get; set; }
        public Nullable<int> ClientID { get; set; }
    
        public virtual Client Client { get; set; }
    }
}
