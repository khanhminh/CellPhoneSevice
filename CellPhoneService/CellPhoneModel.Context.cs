﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CellPhoneDbEntities : DbContext
    {
        public CellPhoneDbEntities()
            : base("name=CellPhoneDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BinhLuan> BinhLuans { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }
        public DbSet<DonDatHang> DonDatHangs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<HeDieuHanh> HeDieuHanhs { get; set; }
        public DbSet<HinhAnh> HinhAnhs { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<LienHe> LienHes { get; set; }
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<NguoiNhan> NguoiNhans { get; set; }
        public DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public DbSet<PhuongThucGiaoHang> PhuongThucGiaoHangs { get; set; }
        public DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<ThamSo> ThamSoes { get; set; }
        public DbSet<TrangThaiDonDatHang> TrangThaiDonDatHangs { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<SoSanhGia> SoSanhGias { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<TinTuc> TinTucs { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
