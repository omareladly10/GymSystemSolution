using AutoMapper;
using GymSystem.BLL.ViewModels.SessionViewModels;
using GymSystem.DAL.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Utilities
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            MapSession();
        }


        private void MapSession()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest=>dest.CategoryName ,opt=> opt.MapFrom(src=>src.Category.CategoryName))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest=>dest.AvailableSlots,opt=>opt.Ignore()).ReverseMap();

        }
    }
}
