//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using QL_ThuVien.Models;

   
    
    

    public partial class DocGia 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocGia()
        {
            this.CT_PM = new HashSet<CT_PM>();
            this.PhieuMuons = new HashSet<PhieuMuon>();
        }
    
        public string IDDG { get; set; }
        public string TenDG { get; set; }
        [MaxLength(10)]
        public string DienThoai { get; set; }
        [MaxLength(255)]
        public string DiaChi { get; set; }
        public string NameUser { get; set; }
        public string Password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PM> CT_PM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }


        public enum CodeAppUser
        {
            InvalidFullName,
            InvalidPassword,
            Valid
        }

        public interface MVCEntity
        {
            void UpdateDatabase(string id, DocGia kh);

        }

        ThuVienEntities3 data = new ThuVienEntities3();

        public CodeAppUser UpdateDatabase(string id, DocGia kh)
        {
            data.Entry(kh).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
            return CodeAppUser.Valid;
        }

        public partial class Proxy : MVCEntity
        {

            private DocGia DocGia;
            public Proxy(DocGia docgia)
            {
                DocGia = docgia;
            }

            public CodeAppUser UpdateDatabase(string id, DocGia kh)
            {
                //StringComparison comp = StringComparison.OrdinalIgnoreCase;
                string notAllow = "Ho Chi Minh"; 
                string tenDG = DocGia.TenDG.ToString();

                string passwordNotAllow = "abc"; 
                string password = DocGia.Password.ToString();

                if (String.Compare(tenDG,notAllow)==0)
                {
                    
                    return CodeAppUser.InvalidFullName;
                    
                }else if(String.Compare(password, passwordNotAllow) == 0)
                {
                    return CodeAppUser.InvalidPassword;
                }         
                else
                {
                    return CodeAppUser.Valid;
                }
               
            }

            void MVCEntity.UpdateDatabase(string id, DocGia kh)
            {
                throw new NotImplementedException();
            }
        }

    }
}
