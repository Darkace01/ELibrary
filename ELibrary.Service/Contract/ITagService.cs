﻿using ELibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Service.Contract
{
    public interface ITagService
    {
        Task Add(Tag tag);
        Task Update(Tag tag, string previousName);
        IQueryable<Tag> Get();
        Tag Get(int id);
        Task AddRange(IEnumerable<Tag> tags);
        bool CheckTagName(string tagName, string previousName = "");
        IQueryable<Tag> GetFeatured();
    }
}
