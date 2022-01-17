using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QL_ThuVien.Models;

namespace QL_ThuVien.Models
{
    public interface Iterator
    {
        DocGia First();
        DocGia Next();

        bool IsDone { get; } 
        DocGia CurrentItem { get; }

    }
    public class DocGiaIterator : Iterator
    {
        List<DocGia> _docgia;
        int current = 0;
        int step = 1;

        public DocGiaIterator(List<DocGia> ListDocGia)
        {
            _docgia = ListDocGia;
        }

        public bool IsDone
        {
            get { return current >= _docgia.Count; }
        }


        public DocGia CurrentItem => _docgia[current];

        public DocGia First()
        {
            current = 0;
            return _docgia[current];
        }

        public DocGia Next()
        {
            current += step;
            if (!IsDone)
            {
                return _docgia[current];
            }
            else
            {
                return null;
            }
        }
    }
}