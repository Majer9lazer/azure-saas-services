//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConnectToDatabase.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class database_firewall_rules
    {
        public int id { get; set; }
        public string name { get; set; }
        public string start_ip_address { get; set; }
        public string end_ip_address { get; set; }
        public System.DateTime create_date { get; set; }
        public System.DateTime modify_date { get; set; }
    }
}
