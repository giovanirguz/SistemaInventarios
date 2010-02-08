using System;
using AutoMapper;
using SistemaInventarios.Model;
using SistemaInventarios.UI.Models;

namespace SistemaInventarios.UI
{
    public class InventariosProfile: Profile
    {
        public const string VIEW_MODEL = "Inventarios";

        protected override string ProfileName
        {
            get { return VIEW_MODEL; }
        }

        protected override void Configure()
        {
            base.Configure();
            AllowNullDestinationValues = false;

            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateClienteMaps();
        }

        private void CreateClienteMaps()
        {
            Mapper.CreateMap<Cliente, ClienteForm>();
        }

    }
}