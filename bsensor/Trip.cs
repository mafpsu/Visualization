//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bsensor
{
    using System;
    using System.Collections.Generic;
    
    public partial class trip
    {
        public long id { get; set; }
        public Nullable<long> user_id { get; set; }
        public int client_trip_id { get; set; }
        public System.DateTime start { get; set; }
        public System.DateTime stop { get; set; }
        public Nullable<long> n_coord { get; set; }
    }
}
