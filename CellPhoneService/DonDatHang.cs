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
    
    public partial class DonDatHang
    {
        public DonDatHang()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }
    
        public int MaDDH { get; set; }
        public string MaTK { get; set; }
        public int MaNguoiNhan { get; set; }
        public System.DateTime NgayLap { get; set; }
        public int MaPTGH { get; set; }
        public int MaPTTT { get; set; }
        public string MaTrangThai { get; set; }
        public double DonGia { get; set; }
    
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual NguoiNhan NguoiNhan { get; set; }
        public virtual PhuongThucGiaoHang PhuongThucGiaoHang { get; set; }
        public virtual PhuongThucThanhToan PhuongThucThanhToan { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual TrangThaiDonDatHang TrangThaiDonDatHang { get; set; }
    }
}
