using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewZealand.Models.Domain;

namespace NewZealand.Interface
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}