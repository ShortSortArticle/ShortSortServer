//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class SimilarWordTable
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string similarWord { get; set; }
        public double Percent { get; set; }
    }
}
