using DoChoiXeMay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DoChoiXeMay.Areas.Admin.Data
{
    public class NoteKyThuatData
    {
        Model1 _context;
        public NoteKyThuatData()
        {
            _context = new Model1();
        }
        public List<NoteKythuat> GetListNotebyHD(int hanhdong)
        {
            var model = _context.NoteKythuats.Where(kh => kh.IdHanhDong == hanhdong)
                    .OrderBy(kh => kh.Id)
                    .ToList();
            for (int i = 0; i < model.Count(); i++)
            {
                model[i].SavenhieuFile = (i + 1).ToString();
            }
            model = model
                .OrderByDescending(kh => kh.Id)
                .ToList();
            return model;
        }
        public List<NoteKythuat> Get1ListNotebyHD(int hanhdong)
        {
            var modle = _context.NoteKythuats.Where(kh => kh.IdHanhDong == hanhdong)
                .OrderByDescending(kh => kh.Id)
                .Take(1)
                .ToList();
            return modle;
        }
    }
}