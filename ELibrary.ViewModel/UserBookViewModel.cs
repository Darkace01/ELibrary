using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.ViewModel;

public class UserBookViewModel
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public ApplicationUserViewModel? User { get; set; }
    public string UserId { get; set; }
    public BookViewModel? Book { get; set; }
    public int BookId { get; set; }
}
