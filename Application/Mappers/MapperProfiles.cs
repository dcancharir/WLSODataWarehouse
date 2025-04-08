using AutoMapper;
using DWDomain;
using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers;
public class MapperProfiles : Profile{
    public MapperProfiles()
    {
        CreateMap<Associate, DWAssociate>().ReverseMap();
        CreateMap<Brand, DWBrand>().ReverseMap();
        CreateMap<Customer, DWCustomer>().ReverseMap();
        CreateMap<CustomersGroup, DWCustomersGroup>().ReverseMap();
        CreateMap<Game, DWGame>().ReverseMap();
        CreateMap<Groupsx, DWGroupsx>().ReverseMap();
        CreateMap<PaymentMethod, DWPaymentMethod>().ReverseMap();
        CreateMap<Player, DWPlayer>().ReverseMap();
        CreateMap<Processor, DWProcessor>().ReverseMap();
        CreateMap<Provider, DWProvider>().ReverseMap();
        CreateMap<RealGameEvent, DWRealGameEvent>().ReverseMap();
        CreateMap<Store, DWStore>().ReverseMap();
        CreateMap<StoreTx, DWStoreTx>().ReverseMap();
    }
}
