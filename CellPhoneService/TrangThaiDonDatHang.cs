//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CellPhoneService
{
    using System;
    using System.Collections.Generic;
    
    public partial class TrangThaiDonDatHang
    {
        public TrangThaiDonDatHang()
        {
            this.DonDatHangs = new HashSet<DonDatHang>();
        }
    
        public string MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }
    
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
