using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrener.Models;

namespace OnlineTrener.Abstract
{
    public interface IVideosRepository
    {
        IEnumerable<Video> Videos { get; }
    }
}
