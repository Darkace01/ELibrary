﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.ViewModel;

public class ListingViewModel
{
    public List<BookViewModel> Books { get; set; }
    public List<CategoryViewModel> Categories { get; set; }

    public int PageTotal { get; set; }
    public int PageNumber { get; set; }
    public int CurrentPage { get; set; }
}
