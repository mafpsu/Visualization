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
    
    public partial class coord
    {
        public int id { get; set; }
        public long trip_id { get; set; }
        public System.DateTime recorded { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public Nullable<double> altitude { get; set; }
        public Nullable<double> speed { get; set; }
        public Nullable<double> hAccuracy { get; set; }
        public Nullable<double> vAccuracy { get; set; }
    }
}
